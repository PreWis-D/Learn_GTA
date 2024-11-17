using DG.Tweening;
using StarterAssets;
using System;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.Playables;

public class Player : MonoBehaviour
{
    [SerializeField] private InteractableDetector _detector;
    [SerializeField] private PlayerAnimator _playerAnimator;
    [SerializeField] private PlayableDirector _playableDirector;
    [SerializeField] private RigidbodyModel _model;
    [SerializeField] private PlayableAsset[] _playableAssets;
    [SerializeField] private float _animateDuration = 0.5f;

    private StarterAssetsInputs _starterAssetsInputs;
    private Sequence _sequence;

    private int _playableDoorAsset = 0;
    private int _playerIndex = 0;
    private int _doorIndex = 1;

    public IInteractable Interactable => _detector.Interactable;
    public ThirdPersonController ThirdPersonController { get; private set; }
    public WeaponBase CurrentWeapon {  get; private set; }

    private void Awake()
    {
        ThirdPersonController = GetComponent<ThirdPersonController>();
        _starterAssetsInputs = GetComponent<StarterAssetsInputs>();

        _starterAssetsInputs.InteractableButtonClicked += TryUseInteractable;

        _playerAnimator.Init(this);
        ThirdPersonController.Init(this);
        ThirdPersonController.SetActiveControllState(ActiveControllState.All);
    }

    private void TryUseInteractable()
    {
        if (_detector.Interactable == null) return;

        switch (_detector.Interactable)
        {
            case QuestNPC questNPC:
                ThirdPersonController.SetActiveControllState(ActiveControllState.None);
                break;
            case Door door:
                ThirdPersonController.SetActiveControllState(ActiveControllState.Camera);
                if (CurrentWeapon)
                {
                    CurrentWeapon.transform.SetParent(_model.GetBodyPart(CurrentWeapon.StorageBodyPartType).transform);
                    CurrentWeapon.transform.SetLocalPositionAndRotation(CurrentWeapon.StorageBodyPartTargetPosition, Quaternion.Euler(CurrentWeapon.StorageBodyPartTargetRotation));
                }
                _playableDirector.playableAsset = _playableAssets[_playableDoorAsset];
                SetSourceAnimator(_playerIndex, _playerAnimator.Animator);
                SetSourceAnimator(_doorIndex, door.Animator);
                _sequence.Kill();
                _sequence = DOTween.Sequence();
                _sequence.Append(transform.DOMove(door.CharacterPoint.position, _animateDuration).SetEase(Ease.Linear));
                _sequence.Append(transform.DOLocalRotate(door.CharacterPoint.localEulerAngles, _animateDuration).SetEase(Ease.Linear));
                _sequence.OnComplete(() => _playableDirector.Play());
                break;
            case WeaponBase weaponBase:
                ThirdPersonController.SetActiveControllState(ActiveControllState.Camera);
                _sequence.Kill();
                _sequence = DOTween.Sequence();
                var direction = weaponBase.transform.position - transform.position;
                _sequence.Append(transform.DOLocalRotate(direction, _animateDuration).SetEase(Ease.Linear));
                _sequence.OnComplete(() =>
                {
                    if (CurrentWeapon)
                    {
                        CurrentWeapon.transform.SetParent(_model.GetBodyPart(CurrentWeapon.StorageBodyPartType).transform);
                        CurrentWeapon.transform.SetLocalPositionAndRotation(CurrentWeapon.StorageBodyPartTargetPosition, Quaternion.Euler(CurrentWeapon.StorageBodyPartTargetRotation));
                    }
                    weaponBase.transform.SetParent(_model.GetBodyPart(weaponBase.HandTargetType).transform);
                    weaponBase.transform.SetLocalPositionAndRotation(weaponBase.HandTargetPosition, Quaternion.Euler(weaponBase.HandTargetRotation));
                    CurrentWeapon = weaponBase;
                    ThirdPersonController.SetActiveControllState(ActiveControllState.All);
                });
                break;
        }

        _detector.Interactable.Execute();
    }

    private void Update()
    {
        _detector.Update();

        if (CurrentWeapon != null)
            _playerAnimator.SetAim(_starterAssetsInputs.aim);
    }

    public void OnRespawnFinished()
    {
        ThirdPersonController.SetActiveControllState(ActiveControllState.All);
        _detector.ChangeActiveState(true);
    }

    public void OnTimelineEnded()
    {
        ThirdPersonController.SetActiveControllState(ActiveControllState.All);

        if (CurrentWeapon)
        {
            CurrentWeapon.transform.SetParent(_model.GetBodyPart(CurrentWeapon.HandTargetType).transform);
            CurrentWeapon.transform.SetLocalPositionAndRotation(CurrentWeapon.HandTargetPosition, Quaternion.Euler(CurrentWeapon.HandTargetRotation));
        }
    }

    private void SetSourceAnimator(int index, Animator animator)
    {
        var outputs = _playableDirector.playableAsset.outputs;

        int counter = 0;
        foreach (var output in outputs)
        {
            if (output.outputTargetType == typeof(Animator)
                && counter == index)
            {
                _playableDirector.ClearGenericBinding(output.sourceObject);
                _playableDirector.SetGenericBinding(output.sourceObject, animator);
            }
            counter++;
        }
    }

    private void OnDestroy()
    {
        _starterAssetsInputs.InteractableButtonClicked -= TryUseInteractable;
    }
}