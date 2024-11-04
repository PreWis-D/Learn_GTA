using Cinemachine;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class CameraAimHead : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private Rig _headRig;
    [SerializeField] private CinemachineVirtualCamera _camera;
    [SerializeField] private float _retargetSpeed = 10f;
    [SerializeField] private float _rigSpeed = 2f;
    [SerializeField] private float _maxAngle = 90f;

    private void Update()
    {
        float rigWeight = 0;
        float angle = Vector3.Angle(transform.forward, _camera.transform.forward);
        Vector3 target = transform.position + (transform.forward * 2);

        if (angle < _maxAngle)
        {
            target = transform.position + _camera.transform.forward;
            rigWeight = 1;
        }

        _target.position = Vector3.Lerp(_target.position, target, Time.deltaTime * _retargetSpeed);
        _headRig.weight = Mathf.Lerp(_headRig.weight, rigWeight, Time.deltaTime * _rigSpeed);
    }
}