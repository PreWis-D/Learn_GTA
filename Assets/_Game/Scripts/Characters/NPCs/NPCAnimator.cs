using System;
using UnityEngine;
using UnityEngine.AI;

public class NPCAnimator : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private NavMeshAgent _agent;

    private int _hashIsTalk = Animator.StringToHash("Talk");
    private int _hashIsLiverTalk = Animator.StringToHash("LiverTalk");
    private int _hashIsSpeed = Animator.StringToHash("Speed");
    private int _hashIsMove = Animator.StringToHash("Move");

    public event Action TalkEnded;

    public void Init(NavMeshAgent navMeshAgent)
    {
        _agent = navMeshAgent;
    }

    private void Update()
    {
        if (_agent)
            _animator.SetFloat(_hashIsSpeed, _agent.speed);
    }

    public void Move(bool isMove)
    {
        _animator.SetBool(_hashIsMove, isMove);
    }

    public void Talk(bool isTalk)
    {
        _animator.SetBool(_hashIsTalk, isTalk);
    }

    public void LiverTalk()
    {
        _animator.SetBool(_hashIsLiverTalk, true);
    }

    public void OnTalkEnded()
    {
        Talk(false);
        TalkEnded?.Invoke();
    }
}