using UnityEngine;

[RequireComponent(typeof(Collider), typeof(Rigidbody))]
public abstract class WeaponBase : MonoBehaviour, IInteractable
{
    [SerializeField] private string _description;

    [Space(10)]
    [Header("Body storage")]
    [SerializeField] private BodyPartType _storageBodyPartType;
    [SerializeField] private Vector3 _storageBodyPartTargetPosition;
    [SerializeField] private Vector3 _storageBodyPartTargetRotation;

    [Space(10)]
    [Header("Hand storage")]
    [SerializeField] private BodyPartType _targetHandType;
    [SerializeField] private Vector3 _handTargetPosition;
    [SerializeField] private Vector3 _handTargetRotation;

    private Rigidbody _rigidbody;
    private Collider _weaponCollider;

    public string InteractionDescription => _description;

    public BodyPartType StorageBodyPartType => _storageBodyPartType;
    public Vector3 StorageBodyPartTargetPosition => _storageBodyPartTargetPosition;
    public Vector3 StorageBodyPartTargetRotation => _storageBodyPartTargetRotation;

    public BodyPartType HandTargetType => _targetHandType;
    public Vector3 HandTargetPosition => _handTargetPosition;
    public Vector3 HandTargetRotation => _handTargetRotation;

    private void Awake()
    {
        _weaponCollider = GetComponent<Collider>();
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.isKinematic = true;
    }

    public void Execute()
    {
        _weaponCollider.enabled = false;
        _rigidbody.isKinematic = true;
    }

    public void Throw()
    {
        _weaponCollider.enabled = true;
        _rigidbody.isKinematic = false;
    }
}