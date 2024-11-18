using UnityEngine;

public class RigidbodyModel : MonoBehaviour
{
    private BodyPart[] _bodyParts;

    private void Awake()
    {
        _bodyParts = GetComponentsInChildren<BodyPart>();
    }

    public void Impuls(float force, Vector3 startPosition, float radius, float modifier)
    {
        for (int i = 0; i < _bodyParts.Length; i++)
            _bodyParts[i].Impuls(force, startPosition, radius, modifier);
    }

    public void DisableKinematic()
    {
        for (int i = 0; i < _bodyParts.Length; i++)
            _bodyParts[i].TryDisableKinematic();
    }

    public void EnableKinematic()
    {
        for (int i = 0; i < _bodyParts.Length; i++)
            _bodyParts[i].TryEnableKinematic();
    }

    public BodyPart GetBodyPart(BodyPartType type)
    {
        for (int i = 0; i < _bodyParts.Length; i++)
        {
            if (_bodyParts[i].Type == type)
                return _bodyParts[i];
        }

        return null;
    }
}
