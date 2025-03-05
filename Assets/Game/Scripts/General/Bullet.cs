using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private int _damage;
    [SerializeField] private float _speed;
    private float _livingTime = 5f;

    [SerializeField] private GameObject _hitVFX;

    private Rigidbody _rigidbody;

    public int Damage => _damage;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }
    private void Start()
    {
        _rigidbody.velocity = transform.forward * _speed;
    }
    private void Update()
    {
        _livingTime -= Time.deltaTime;
        if(_livingTime <= 0)
            Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision other)
    {
        Instantiate(_hitVFX, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

}
