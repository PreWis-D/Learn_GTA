using DG.Tweening;
using StarterAssets;
using UnityEngine;
using UnityEngine.Playables;

public class Player : MonoBehaviour
{
    [SerializeField] private InteractableDetector _detector;
    [SerializeField] private PlayerAnimator _playerAnimator;
    [SerializeField] private PlayableDirector _playableDirector;
    [SerializeField] private PlayableAsset[] _playableAssets;
    [SerializeField] private float _animateDuration = 0.5f;

    private StarterAssetsInputs _starterAssetsInputs;
    private Sequence _sequence;

    private int _playableDoorAsset = 0;
    private int _playerIndex = 0;
    private int _doorIndex = 1;

    public IInteractable Interactable => _detector.Interactable;
    public ThirdPersonController ThirdPersonController {  get; private set; }

    private void Awake()
    {
        ThirdPersonController = GetComponent<ThirdPersonController>();
        _starterAssetsInputs = GetComponent<StarterAssetsInputs>();

        _starterAssetsInputs.InteractableButtonClicked += TryUseInteractable;

        _playerAnimator.Init(this);
        ThirdPersonController.Init(this);
        ThirdPersonController.SetActiveControllState(ActiveControllState.None);
    }

    private void Start()
    {
        ThirdPersonController.SetActiveControllState(ActiveControllState.Camera);
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
                _playableDirector.playableAsset = _playableAssets[_playableDoorAsset];
                SetSourceAnimator(_playerIndex, _playerAnimator.Animator);
                SetSourceAnimator(_doorIndex, door.Animator);
                _sequence.Kill();
                _sequence = DOTween.Sequence();
                _sequence.Append(transform.DOMove(door.CharacterPoint.position, _animateDuration).SetEase(Ease.Linear));
                _sequence.Append(transform.DOLocalRotate(door.CharacterPoint.localEulerAngles, _animateDuration).SetEase(Ease.Linear));
                _sequence.OnComplete(() => _playableDirector.Play());
                break;
        }

        _detector.Interactable.Execute();
    }

    private void Update()
    {
        _detector.Update();
    }

    public void OnRespawnFinished()
    {
        ThirdPersonController.SetActiveControllState(ActiveControllState.All);
        _detector.ChangeActiveState(true);
    }

    public void OnTimelineEnded()
    {
        ThirdPersonController.SetActiveControllState(ActiveControllState.All);
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