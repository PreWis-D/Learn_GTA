using Cinemachine;
using StarterAssets;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _camera;
    [SerializeField] private StarterAssetsInputs _input;

    [Space(10)]
    [SerializeField] private float _minZoom = -1.0f;
    [SerializeField] private float _maxZoom = 20f;
    [SerializeField][Range(0, 1)] private float _sensZoom = 0.0075f;
    [SerializeField] private float _speedZoom = 2f;

    private Cinemachine3rdPersonFollow _personFollow;

    public CinemachineVirtualCamera Camera => _camera;

    private void Awake()
    {
        _personFollow = _camera.GetCinemachineComponent<Cinemachine3rdPersonFollow>();
    }

    private void Update()
    {
        Zoom();
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