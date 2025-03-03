using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private int _damage;
    [SerializeField] private float _speed;

    [SerializeField] private GameObject _hitVFX;

    private Rigidbody _rigidbody;

    public float Damage => _damage;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }
    private void Start()
    {
        _rigidbody.velocity = transform.forward * _speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        Instantiate(_hitVFX, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
