using StarterAssets;
using System;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Player _player;
    private ThirdPersonController _thirdPersonController;
    private bool _isAim;
    private float _camRotateValue;

    public Animator Animator { get; private set; }
    private int _hashIsSpeedX = Animator.StringToHash("Velocity X");
    private int _hashIsSpeedZ = Animator.StringToHash("Velocity Z");
    private int _hashIsCameraRotation = Animator.StringToHash("CameraRotation");
    private int _hashIsAim = Animator.StringToHash("IsAim");

    public void Init(Player player)
    {
        Animator = GetComponentInChildren<Animator>();

        if (Animator == null)
            throw new ArgumentException("animator is null");

        _player = player;
        _thirdPersonController = _player.ThirdPersonController;

        Subscribe();
    }

    private void Subscribe()
    {
        _thirdPersonController.MoveSpeedChanged += OnMoveSpeedChanged;
        _thirdPersonController.CameraRotated += OnCameraRotated;
    }

    private void Unsubscribe()
    {
        _thirdPersonController.MoveSpeedChanged -= OnMoveSpeedChanged;
        _thirdPersonController.CameraRotated -= OnCameraRotated;
    }

    private void OnCameraRotated(float value)
    {
        _camRotateValue = Mathf.Lerp(_camRotateValue, value, 10 * Time.deltaTime);
        Animator.SetFloat(_hashIsCameraRotation, _camRotateValue);
    }

    #region Move
    public void SetAim(bool isAim)
    {
        if (_isAim == isAim)
            return;

        _isAim = isAim;
        Animator.SetBool(_hashIsAim, _isAim);
    }

    private void OnMoveSpeedChanged(float animationBlend, Vector3 direction, float magnitude)
    {
        SetAimMove(animationBlend, direction);
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
    #endregion
}