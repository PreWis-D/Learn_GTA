using Cinemachine;
using Reflex.Attributes;
using System;
using UnityEngine;

public class QuestTalkTargetNPC : MonoBehaviour, IQuestPart
{
    [SerializeField] private QuestNPC _questNpc;
    [SerializeField] private QuestViewConfig _questViewConfig;
    [SerializeField] private ParticleSystem _zoneParticle;
    [SerializeField] private ParticleSystem _signParticle;
    [SerializeField] private CinemachineVirtualCamera _camera;

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
        HideEffects();
    }

    public void Enter()
    {
        _camera.m_Follow = _questNpc.CamFollow;
        _camera.m_LookAt = _questNpc.CamFollow;

        _questNpc.ActivateInteractable();
        _questNpc.TalkStarted += OnTalkStarted;
        _questNpc.TalkEnded += OnCompleted;

        ShowEffects();

        _uiController.ShowQuestView(_questViewConfig);
    }

    private void OnTalkStarted()
    {
        _camera.Priority = 10;
        HideEffects();
        _player.ThirdPersonController.SetActiveControllState(ActiveControllState.None);
        _uiController.ShowDialogPanel(_questNpc.CurrentDialogConfig);
    }

    public void OnCompleted()
    {
        _questNpc.TalkStarted -= OnTalkStarted;
        _questNpc.TalkEnded -= OnCompleted;
        _questNpc.DeactivateInteractable();
        _uiController.ShowViewPanel();

        _camera.Priority = 0;
        _player.ThirdPersonController.SetActiveControllState(ActiveControllState.All);
        QuestCompleted?.Invoke();
    }

    private void ShowEffects()
    {
        _zoneParticle.gameObject.SetActive(true);
        _signParticle.gameObject.SetActive(true);
    }

    private void HideEffects()
    {
        _zoneParticle.gameObject.SetActive(false);
        _signParticle.gameObject.SetActive(false);
    }
}