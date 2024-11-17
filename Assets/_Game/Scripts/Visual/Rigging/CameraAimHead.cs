using Cinemachine;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class CameraAimHead : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private MultiAimConstraint _aim;
    [SerializeField] private CinemachineVirtualCamera _camera;
    [SerializeField] private Vector3 _offset = new Vector3(0f,1.5f,0f);
    [SerializeField] private float _retargetSpeed = 10f;
    [SerializeField] private float _rigSpeed = 2f;
    [SerializeField] private float _targetDistance = 4f;
    [SerializeField] private float _maxAngle = 90f;

    private void Update()
    {
        float rigWeight = 0;
        float angle = Vector3.Angle(transform.forward, _camera.transform.forward);
        Vector3 target = transform.position + _offset + (transform.forward * _targetDistance);

        if (angle < _maxAngle)
        {
            target = transform.position + _offset + _camera.transform.forward * _targetDistance;
            rigWeight = 1;
        }

        _target.position = Vector3.Lerp(_target.position, target, Time.deltaTime * _retargetSpeed);
        _aim.weight = Mathf.Lerp(_aim.weight, rigWeight, Time.deltaTime * _rigSpeed);
    }
}