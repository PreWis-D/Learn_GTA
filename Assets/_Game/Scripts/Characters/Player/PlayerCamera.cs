using Cinemachine;
using StarterAssets;
using System;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _camera;
    [SerializeField] private Player _player;

    [Space(10)]
    [SerializeField] private float _minZoom = -1.0f;
    [SerializeField] private float _maxZoom = 20f;
    [SerializeField][Range(0, 1)] private float _sensZoom = 0.0075f;
    [SerializeField] private float _speedZoom = 2f;

    private StarterAssetsInputs _input;
    private Cinemachine3rdPersonFollow _personFollow;
    private bool _isAim;
    private float _defaultDistance = 6;
    private Vector3 _defaultShoulderOffset;
    private Vector3 _targetShoulderOffset = new Vector3(3,0,0);

    public CinemachineVirtualCamera Camera => _camera;

    private void Awake()
    {
        _personFollow = _camera.GetCinemachineComponent<Cinemachine3rdPersonFollow>();
        _input = _player.GetComponent<StarterAssetsInputs>();

        _personFollow.CameraDistance = _defaultDistance;
        _defaultShoulderOffset = _personFollow.ShoulderOffset;
    }

    private void Update()
    {
        Aim();

        if (_isAim)
            return;
        
        Zoom();
    }

    private void Aim()
    {
        if (_player.CurrentWeapon == null)
            return;

        _isAim = _input.aim;
        _personFollow.ShoulderOffset = _targetShoulderOffset;
        _personFollow.CameraDistance = _isAim ? 1 : _defaultDistance;
    }

    private void Zoom()
    {
        var zoom = _personFollow.CameraDistance;

        zoom += _input.zoom * _sensZoom;

        if (zoom < _minZoom)
            zoom = _minZoom;
        else if (zoom > _maxZoom)
            zoom = _maxZoom;

        _personFollow.CameraDistance = Mathf.Lerp(_personFollow.CameraDistance, zoom, Time.deltaTime * _speedZoom);
    }
}