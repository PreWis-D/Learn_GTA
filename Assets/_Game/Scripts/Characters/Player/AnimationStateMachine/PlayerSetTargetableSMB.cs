using UnityEngine;

public class PlayerSetTargetableSMB : StateMachineBehaviour
{
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        PlayerAnimator playerAnimator = animator.GetComponent<PlayerAnimator>();

        if (playerAnimator != null)
        {
            playerAnimator.OnRespawnFinished();
        }
    }
}
