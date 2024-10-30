using UnityEngine;

public abstract class BaseTransition : MonoBehaviour
{
    [SerializeField] private BaseState _targetState;
    [SerializeField] private BaseState _targetAlternativeState;

    public BaseState TargetState => _targetState;
    public BaseState TargetAlternativeState => _targetAlternativeState;
    protected BaseNPC NPC { get; private set; }
    public bool NeedTransit { get; protected set; }
    public bool NeedAlternativeTransit { get; protected set; }


    protected void OnEnable()
    {
        NeedTransit = false;
        NeedAlternativeTransit = false;
    }

    public void Init(BaseNPC npc)
    {
        NPC = npc;
    }
}