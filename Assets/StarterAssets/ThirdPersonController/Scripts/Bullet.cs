using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody=GetComponent<Rigidbody>();
    }

    private void Start()
    {
        float speed = 10f;
        _rigidbody.velocity = Vector3.forward * speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out BulletTarget component))
            Debug.Log(component);

        Destroy(gameObject);
    }
}
