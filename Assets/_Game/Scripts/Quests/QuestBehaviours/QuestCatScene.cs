using Reflex.Attributes;
using System;
using UnityEngine;
using UnityEngine.Playables;

public class QuestCatScene : MonoBehaviour, IQuestPart
{
    [SerializeField] private PlayableDirector _playble;

    private Player _player;
    private UIController _uiController;

    public event Action QuestCompleted;

    [Inject]
    private void Construct(Player player, UIController uIController)
    {
        _player = player;
        _uiController = uIController;
    }

    public void Init()
    {
        _playble.Stop();
    }

    public void Enter()
    {
        _player.ThirdPersonController.SetActiveControllState(ActiveControllState.Camera);
        _playble.Play();
    }

    public void OnTimelineEnded()
    {
        OnCompleted();
    }

    public void OnCompleted()
    {
        Debug.LogWarning("complete");
        _player.ThirdPersonController.SetActiveControllState(ActiveControllState.All);
        QuestCompleted.Invoke();
    }
}