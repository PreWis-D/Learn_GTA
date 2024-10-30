using System;
using UnityEngine;

public class QuestNPC : BaseNPC, IInteractable
{
    [SerializeField] private DialogConfig[] _dialogConfigs;
    [SerializeField] private float _radius;
    [SerializeField] private Transform _camFollow;
    [SerializeField] private int _interactableLayer;

    private int _currentDialogIndex = 0;

    public Transform CamFollow => _camFollow; 

    public DialogConfig CurrentDialogConfig { get; private set; }

    public event Action TalkStarted;
    public event Action TalkEnded;

    public new void Awake()
    {
        base.Awake();
        _animator.TalkEnded += OnTalkEnded;
    }

    public void ActivateInteractable()
    {
        _animator.gameObject.layer = _interactableLayer;
    }

    public void DeactivateInteractable()
    {
        _animator.gameObject.layer = 0;
    }

    public void Execute()
    {
        CurrentDialogConfig = _dialogConfigs[_currentDialogIndex];
        TalkStarted?.Invoke();
        _animator.Talk(true);
    }

    private void OnTalkEnded()
    {
        _currentDialogIndex++;
        TalkEnded?.Invoke();
    }

    private void OnDestroy()
    {
        _animator.TalkEnded -= OnTalkEnded;
    }
}