using Cysharp.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ViewPanel : BasePanel
{
    [Space(10)]
    [Header("Present")]
    [SerializeField] private Transform _presentTransform;
    [SerializeField] private Image _presentIcon;
    [SerializeField] private TMP_Text _targetCountText;
    [SerializeField] private float _durationAnimationPresent = 0.5f;

    [Space(10)]
    [Header("Description")]
    [SerializeField] private Transform _descriptionTransform;
    [SerializeField] private TMP_Text _descriptionText;
    [SerializeField] private float _delayHideDescription = 3f;
    [SerializeField] private float _durationAnimationDescription = 0.5f;

    [Space(10)]
    [Header("Interaction description")]
    [SerializeField] private Transform _interactionDescriptionTransform;
    [SerializeField] private TMP_Text _interactionDescriptionText;
    [SerializeField] private float _durationAnimationInteractionDescription = 0.25f;

    private Player _player;
    private IInteractable _interactable;

    private Tween _tweenPresent;
    private Tween _tweenDescription;
    private Tween _tweenInteractable;

    public void Init(Player player)
    {
        _player = player;

        _presentTransform.localScale = Vector3.zero;
        _descriptionTransform.localScale = Vector3.zero;
        _interactionDescriptionTransform.localScale = Vector3.zero;
        _presentTransform.gameObject.SetActive(false);
        _descriptionTransform.gameObject.SetActive(false);
        _interactionDescriptionTransform.gameObject.SetActive(false);
    }

    public void SetPresent(QuestViewConfig questViewConfig)
    {
        _presentIcon.sprite = questViewConfig.PresentSprite;

        _targetCountText.gameObject.SetActive(questViewConfig.TargetCount > 0);
    }

    public void SetCountText(int currentValue, int targetValue)
    {
        _targetCountText.text = $"{currentValue}/{targetValue}";
    }

    public void SetDescription(QuestViewConfig questViewConfig)
    {
        _descriptionText.text = questViewConfig.Description;
    }

    public void ShowPresentView()
    {
        _presentTransform.gameObject.SetActive(true);
        _tweenPresent.Kill();
        _tweenPresent = _presentTransform.DOScale(Vector3.one, _durationAnimationPresent).SetEase(Ease.OutBack);
    }

    public void HidePresentView()
    {
        _tweenPresent.Kill();
        _tweenPresent = _presentTransform.DOScale(Vector3.zero, _durationAnimationPresent).SetEase(Ease.InBack);
        _tweenPresent.OnComplete(() => _presentTransform.gameObject.SetActive(false));
    }

    public void ShowDescriptionView()
    {
        _descriptionTransform.gameObject.SetActive(true);
        _tweenDescription.Kill();
        _tweenDescription = _descriptionTransform.DOScale(Vector3.one, _durationAnimationDescription).SetEase(Ease.OutBack);
        _tweenDescription.OnComplete(() => DelayHideDescription().Forget());
    }

    private async UniTask DelayHideDescription()
    {
        await UniTask.Delay((int)(_delayHideDescription * 1000));

        HideDescriptionView();
    }

    private void HideDescriptionView()
    {
        _tweenDescription.Kill();
        _tweenDescription = _descriptionTransform.DOScale(Vector3.zero, _durationAnimationDescription).SetEase(Ease.InBack);
        _tweenDescription.OnComplete(() => _descriptionTransform.gameObject.SetActive(false));
    }

    private void Update()
    {
        if (_player)
            CheckInteractable();
    }

    private void CheckInteractable()
    {
        if (_player.Interactable != null && (_interactable == null || _interactable != _player.Interactable))
        {
            _interactionDescriptionTransform.gameObject.SetActive(true);
            _interactable = _player.Interactable;
            _interactionDescriptionText.text = $"E: {_interactable.InteractionDescription}";
            _tweenInteractable.Kill();
            _tweenInteractable = _interactionDescriptionTransform.DOScale(Vector3.one, _durationAnimationInteractionDescription).SetEase(Ease.OutBack);
        }
        else if (_player.Interactable == null && _interactable != null)
        {
            _interactable = null;
            _tweenInteractable.Kill();
            _tweenInteractable = _interactionDescriptionTransform.DOScale(Vector3.zero, _durationAnimationInteractionDescription).SetEase(Ease.InBack);
            _tweenInteractable.OnComplete(() =>
            {
                _interactionDescriptionTransform.gameObject.SetActive(false);
            });
        }
    }

    private void OnDestroy()
    {
        _tweenPresent.Kill();
        _tweenDescription.Kill();
    }
}