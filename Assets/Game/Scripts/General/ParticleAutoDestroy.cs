using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleAutoDestroy : MonoBehaviour
{
    private ParticleSystem _particleSystem;
    private float _time;
    void Start()
    {
        _particleSystem = GetComponent<ParticleSystem>();
        _time = _particleSystem.main.duration;
    }
    void Update()
    {
        _time -= Time.deltaTime;
        if (_time <= 0)
        {
            Destroy(gameObject);
        }
    }
}