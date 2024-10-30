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

    private Cinemachine3rdPersonFollow _personFollow;
    
    public CinemachineVirtualCamera Camera => _camera;

    private void Awake()
    {
        _personFollow = _camera.GetCinemachineComponent<Cinemachine3rdPersonFollow>();    
    }

    private void Update()
    {
        //if (_personFollow != null && _input != null)
        Zoom();
    }

    private void Zoom()
    {
        var zoom = _personFollow.CameraDistance;

        zoom += _input.zoom * _sensZoom;

        if (_personFollow.CameraDistance < _minZoom)
            zoom = _minZoom;

        if (_personFollow.CameraDistance > _maxZoom)
            zoom = _maxZoom;

        _personFollow.CameraDistance = zoom;
    }
}