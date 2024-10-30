using Reflex.Attributes;
using System;
using UnityEngine;

public class QuestTakeObjects : MonoBehaviour, IQuestPart
{
    [SerializeField] private QuestViewConfig _questViewConfig;
    [SerializeField] private Collectable[] _collectables;

    private int _currentTakeCollectables;

    private UIController _uiController;

    public event Action QuestCompleted;

    [Inject]
    private void Construct(UIController uIController)
    {
        _uiController = uIController;
    }

    public void Init()
    {
        for (int i = 0; i < _collectables.Length; i++)
        {
            _collectables[i].PlayerReached += OnPlayerReached;
            _collectables[i].gameObject.SetActive(false);
        }
    }

    private void OnPlayerReached(Collectable collectable)
    {
        _collectables[_currentTakeCollectables].gameObject.SetActive(false);
        _currentTakeCollectables++;
        _uiController.ShowCountText(_currentTakeCollectables, _questViewConfig.TargetCount);

        if (_currentTakeCollectables >= _collectables.Length)
            OnCompleted();
        else
            _collectables[_currentTakeCollectables].gameObject.SetActive(true);
    }

    public void Enter()
    {
        _collectables[_currentTakeCollectables].gameObject.SetActive(true);
        _uiController.ShowQuestView(_questViewConfig);
        _uiController.ShowCountText(_currentTakeCollectables, _questViewConfig.TargetCount);
    }

    public void OnCompleted()
    {
        QuestCompleted?.Invoke();
    }

    private void OnDestroy()
    {
        for (int i = 0; i < _collectables.Length; i++)
            _collectables[i].PlayerReached -= OnPlayerReached;
    }
}