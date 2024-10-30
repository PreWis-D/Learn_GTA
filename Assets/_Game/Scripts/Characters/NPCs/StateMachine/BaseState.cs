using System.Collections.Generic;
using UnityEngine;

public abstract class BaseState : MonoBehaviour
{
    [SerializeField] protected List<BaseTransition> _transitions;

    protected BaseNPC NPC { get; set; }

    public void Exit()
    {
        if (enabled == true)
        {
            foreach (var transition in _transitions)
                transition.enabled = false;

            enabled = false;
        }
    }

    public BaseState GetNextState()
    {
        foreach (var transition in _transitions)
        {
            if (transition.NeedTransit)
                return transition.TargetState;

            if (transition.NeedAlternativeTransit)
                return transition.TargetAlternativeState;
        }

        return null;
    }
    
    public void Enter(BaseNPC npc)
    {
        if (enabled == false)
        {
            NPC = npc;
            enabled = true;

            foreach (var transition in _transitions)
            {
                transition.Init(NPC);
                transition.enabled = true;
            }
        }
    }
}
