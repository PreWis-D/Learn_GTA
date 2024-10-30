using UnityEngine;

public class AnimatorIdleStateSwitcher : StateMachineBehaviour
{
    [SerializeField] private float _timeUntilActive;
    [SerializeField] private int _IdleAnimationsCount;

    private bool _isActive;
    private float _idleTime;
    private int _idleAnimationIndex;
    private const string _idleAnimation = "Idle";

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        ResetIdle();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_isActive == false)
        {
            _idleTime += Time.deltaTime;

            if (_idleTime > _timeUntilActive && stateInfo.normalizedTime % 1 < 0.02f)
            {
                _isActive = true;
                _idleAnimationIndex = Random.Range(1, _IdleAnimationsCount + 1);
                _idleAnimationIndex = _idleAnimationIndex * 2 - 1;

                animator.SetFloat(_idleAnimation, _idleAnimationIndex - 1);
            }
        }
        else if (stateInfo.normalizedTime % 1 > 0.98f)
        {
            ResetIdle();
        }

        animator.SetFloat(_idleAnimation, _idleAnimationIndex, 0.2f, Time.deltaTime);
    }

    private void ResetIdle()
    {
        if (_isActive)
        {
            _idleAnimationIndex--;
        }

        _isActive = false;
        _idleTime = 0;
    }
}
