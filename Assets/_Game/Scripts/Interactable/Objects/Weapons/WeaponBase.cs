using UnityEngine;

[RequireComponent(typeof(Collider), typeof(Rigidbody))]
public abstract class WeaponBase : MonoBehaviour, IInteractable
{
    [SerializeField] private string _description;

    private Rigidbody _rigidbody;
    private Collider _weaponCollider;

    public string InteractionDescription => _description;

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