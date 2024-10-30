using StarterAssets;
using System;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Player _player;
    private ThirdPersonController _thirdPersonController;

    private Animator _animator;
    private int _animIDSpeed = Animator.StringToHash("Speed");
    private int _animIDGrounded = Animator.StringToHash("Grounded");
    private int _animIDJump = Animator.StringToHash("Jump");
    private int _animIDFreeFall = Animator.StringToHash("FreeFall");
    private int _animIDMotionSpeed = Animator.StringToHash("MotionSpeed");
    private int _animIDTimeoutIdle = Animator.StringToHash("TimeoutToIdle");
    private int _animIDAirboneVelocity = Animator.StringToHash("AirborneVerticalSpeed");

    public void Init(Player player)
    {
        _animator = GetComponent<Animator>();

        if (_animator == null)
            throw new ArgumentException("animator is null");

        _player = player;
        _thirdPersonController = _player.ThirdPersonController;

        Subscribe();
    }

    private void Subscribe()
    {
        _thirdPersonController.GroundedStateChanged += OnGroundedStateChanged;
        _thirdPersonController.MoveSpeedChanged += OnMoveSpeedChanged;
        _thirdPersonController.JumpStateChanged += OnJumpStateChanged;
        _thirdPersonController.FreeFallStateChanged += OnFreeFallStateChanged;
        _thirdPersonController.TimeoutToIdleEntered += OnTimeoutToIdleEntered;
        _thirdPersonController.TimeoutToIdleExeted += OnTimeoutToIdleExeted;
        _thirdPersonController.AirboneVelocityChanged += OnAirboneVelocityChanged;
    }

    private void Unsubscribe()
    {
        _thirdPersonController.GroundedStateChanged -= OnGroundedStateChanged;
        _thirdPersonController.MoveSpeedChanged -= OnMoveSpeedChanged;
        _thirdPersonController.JumpStateChanged -= OnJumpStateChanged;
        _thirdPersonController.FreeFallStateChanged -= OnFreeFallStateChanged;
        _thirdPersonController.TimeoutToIdleEntered -= OnTimeoutToIdleEntered;
        _thirdPersonController.TimeoutToIdleExeted -= OnTimeoutToIdleExeted;
        _thirdPersonController.AirboneVelocityChanged -= OnAirboneVelocityChanged;
    }

    #region Move
    private void OnGroundedStateChanged(bool isValue)
    {
        _animator.SetBool(_animIDGrounded, isValue);
    }
    
    private void OnMoveSpeedChanged(float animationBlend, float inputMagnitude)
    {
        _animator.SetFloat(_animIDSpeed, animationBlend);
        _animator.SetFloat(_animIDMotionSpeed, inputMagnitude);
    }

    private void OnJumpStateChanged(bool value)
    {
        _animator.SetBool(_animIDJump, value);
    }

    private void OnFreeFallStateChanged(bool value)
    {
        _animator.SetBool(_animIDFreeFall, value);
    }

    private void OnAirboneVelocityChanged(float value)
    {
        _animator.SetFloat(_animIDAirboneVelocity, value);
    }

    private void OnTimeoutToIdleEntered()
    {
        _animator.SetTrigger(_animIDTimeoutIdle);
    }

    private void OnTimeoutToIdleExeted()
    {
        _animator.ResetTrigger(_animIDTimeoutIdle);
    }
    #endregion

    #region Events
    internal void OnRespawnFinished()
    {
        _player.OnRespawnFinished();
    }
    #endregion
}