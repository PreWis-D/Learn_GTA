using StarterAssets;
using System;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Player _player;
    private ThirdPersonController _thirdPersonController;
    private bool _isAim;

    public Animator Animator { get; private set; }
    private int _animIDSpeed = Animator.StringToHash("Speed");
    private int _animIDGrounded = Animator.StringToHash("Grounded");
    private int _animIDJump = Animator.StringToHash("Jump");
    private int _animIDFreeFall = Animator.StringToHash("FreeFall");
    private int _animIDMotionSpeed = Animator.StringToHash("MotionSpeed");
    private int _animIDTimeoutIdle = Animator.StringToHash("TimeoutToIdle");
    private int _animIDAirboneVelocity = Animator.StringToHash("AirborneVerticalSpeed");
    private int _hashIsSpeedX = Animator.StringToHash("Velocity X");
    private int _hashIsSpeedZ = Animator.StringToHash("Velocity Z");
    private int _hashIsAim = Animator.StringToHash("IsAim");

    public void Init(Player player)
    {
        Animator = GetComponent<Animator>();

        if (Animator == null)
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
    public void SetAim(bool isAim)
    {
        _isAim = isAim;
        Animator.SetBool(_hashIsAim, _isAim);
    }

    private void OnGroundedStateChanged(bool isValue)
    {
        //Animator.SetBool(_animIDGrounded, isValue);
    }

    private void OnMoveSpeedChanged(float animationBlend, Vector3 direction, float magnitude)
    {
        if (_isAim)
            SetAimMove(animationBlend, direction);
        else
            SetDefaultMove(animationBlend, magnitude);
    }

    private void OnJumpStateChanged(bool value)
    {
        //Animator.SetBool(_animIDJump, value);
    }

    private void OnFreeFallStateChanged(bool value)
    {
        //Animator.SetBool(_animIDFreeFall, value);
    }

    private void OnAirboneVelocityChanged(float value)
    {
        //Animator.SetFloat(_animIDAirboneVelocity, value);
    }

    private void OnTimeoutToIdleEntered()
    {
        //Animator.SetTrigger(_animIDTimeoutIdle);
    }

    private void OnTimeoutToIdleExeted()
    {
        //Animator.ResetTrigger(_animIDTimeoutIdle);
    }

    private void SetAimMove(float animationBlend, Vector3 direction)
    {
        if (direction.x < 0)
            Animator.SetFloat(_hashIsSpeedX, -animationBlend);
        else if (direction.x > 0)
            Animator.SetFloat(_hashIsSpeedX, animationBlend);
        else
            Animator.SetFloat(_hashIsSpeedX, 0);

        if (direction.z < 0)
            Animator.SetFloat(_hashIsSpeedZ, -animationBlend);
        else if (direction.z > 0)
            Animator.SetFloat(_hashIsSpeedZ, animationBlend);
        else
            Animator.SetFloat(_hashIsSpeedZ, 0);
    }

    private void SetDefaultMove(float animationBlend, float inputMagnitude)
    {
        Animator.SetFloat(_animIDSpeed, animationBlend);
        Animator.SetFloat(_animIDMotionSpeed, inputMagnitude);
    }
    #endregion

    #region Events
    internal void OnRespawnFinished()
    {
        _player.OnRespawnFinished();
    }
    #endregion
}