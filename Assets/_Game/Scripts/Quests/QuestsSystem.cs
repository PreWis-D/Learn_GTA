using Reflex.Attributes;
using UnityEngine;

public class QuestsSystem : MonoBehaviour
{
    private IQuestPart[] _questParts;
    private int _currentQuestPart;

    private UIController _uiController;

    [Inject]
    private void Construct(UIController uIController)
    {
        _uiController = uIController;
    }

    public void Awake()
    {
        _questParts = GetComponentsInChildren<IQuestPart>();

        for (int i = 0; i < _questParts.Length; i++)
        {
            _questParts[i].Init();
            _questParts[i].QuestCompleted += QuestPartCompleted;
        }

        _questParts[0].Enter();
    }

    private void QuestPartCompleted()
    {
        _currentQuestPart++;

        if (_currentQuestPart >= _questParts.Length)
        {
            _currentQuestPart = 0;
            _uiController.HidePresent();
        }
        else
        {
            _questParts[_currentQuestPart].Enter();
        }
    }

    private void OnDestroy()
    {
        for (int i = 0; i < _questParts.Length; i++)
            _questParts[i].QuestCompleted -= QuestPartCompleted;
    }
}