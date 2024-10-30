using UnityEngine;

public class CarModel : MonoBehaviour
{
    [SerializeField] private ElementRotator[] _wheels;

    public void Init(float speed)
    {
        for (int i = 0; i < _wheels.Length; i++)
            _wheels[i].SetSpeed(speed);
    }

    public void Activate()
    {
        for (int i = 0; i < _wheels.Length; i++)
            _wheels[i].enabled = true;
    }

    public void Deactivate()
    {
        for (int i = 0; i < _wheels.Length; i++)
            _wheels[i].enabled = false;
    }
}
