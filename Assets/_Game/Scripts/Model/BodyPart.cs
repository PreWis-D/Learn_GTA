using UnityEngine;

public class BodyPart : MonoBehaviour
{
    [SerializeField] private BodyPartType _type;
    [SerializeField] private Transform _itemPositionPoint;

    public BodyPartType Type => _type;

    public Rigidbody Rigidbody { get; private set; }
    public Collider Collider { get; private set; }
    public Transform ItemPositionPoint => _itemPositionPoint;

    public void TryDisableKinematic()
    {
        if (Rigidbody == null || Collider == null) return;
        ChangeKinematic(false);
    }

    public void TryEnableKinematic()
    {
        if (Rigidbody == null || Collider == null) return;
        ChangeKinematic(true);
    }

    public void Impuls(float force, Vector3 startPosition, float radius, float modifier)
    {
        Rigidbody.AddExplosionForce(force, startPosition, radius, modifier);
    }

    private void ChangeKinematic(bool isKinematic)
    {
        Rigidbody.isKinematic = isKinematic;
        Collider.enabled = !isKinematic;
    }
}