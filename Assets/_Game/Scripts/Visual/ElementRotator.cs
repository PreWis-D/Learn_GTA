using UnityEngine;

public class ElementRotator : MonoBehaviour
{
    [SerializeField] private float _speedRotate;
    [SerializeField] private Vector3 _rotateDirection;
    [SerializeField] private bool _isUnscaled;

    private Vector3 _currentAngel;
    private Vector3 _startAngel;

    private void Awake()
    {
        _startAngel = transform.localEulerAngles;
    }

    public void SetSpeed(float speed)
    {
        _speedRotate = speed;
    }

    private void OnEnable()
    {
        _currentAngel = _startAngel;
    }

    private void Update()
    {
        _currentAngel += _rotateDirection * _speedRotate * (_isUnscaled ? Time.unscaledDeltaTime : Time.deltaTime);
        transform.localEulerAngles = _currentAngel;
    }
}
