using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BoredBehaviour : StateMachineBehaviour
{
    [SerializeField] private string _animationKey;
    [SerializeField] private float _timeUntilBored;
    [SerializeField] private int _numberOfBoredAnimations;

    private bool _isBored;
    private float _idleTime;
    private int _boredAnimation;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        ResetIdle();
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_isBored)
        {
            _idleTime += Time.deltaTime;

            if (_idleTime > _timeUntilBored && stateInfo.normalizedTime % 1 < 0.02f)
            {
                _isBored = false;
                _boredAnimation = Random.Range(1, _numberOfBoredAnimations);
                _boredAnimation = _boredAnimation * 2 - 1;

                animator.SetFloat(_animationKey, _boredAnimation - 1);
            }
        }
        else if (stateInfo.normalizedTime % 1 > 0.98f)
        {
            ResetIdle();
        }

        animator.SetFloat(_animationKey, _boredAnimation, 0.2f, Time.deltaTime);
    }

    private void ResetIdle()
    {
        if (_isBored)
            _boredAnimation--;

        _isBored = true;
        _idleTime = 0;
    }
}