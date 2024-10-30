using UnityEngine;
using UnityEngine.AI;

public abstract class BaseNPC : MonoBehaviour
{
    protected NPCAnimator _animator;

    public NavMeshAgent Agent {  get; private set; }

    public void Awake()
    {
        Agent = GetComponent<NavMeshAgent>();
        _animator = GetComponentInChildren<NPCAnimator>();

        _animator.Init(Agent);
    }
}