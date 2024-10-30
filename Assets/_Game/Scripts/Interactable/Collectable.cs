using System;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    private Collider _collider;

    public event Action<Collectable> PlayerReached;

    private void Awake()
    {
        _collider = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
            PlayerReached?.Invoke(this);
    }
}