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

    private Tween _tweenPresent;
    private Tween _tweenDescription;

    public void Init()
    {
        _presentTransform.localEulerAngles = Vector3.zero;
        _descriptionTransform.localEulerAngles = Vector3.zero;
        _presentTransform.gameObject.SetActive(false);
        _descriptionTransform.gameObject.SetActive(false);
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

    private void OnDestroy()
    {
        _tweenPresent.Kill();
        _tweenDescription.Kill();
    }
}