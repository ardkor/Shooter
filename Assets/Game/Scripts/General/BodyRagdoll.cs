using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BodyRagdoll : MonoBehaviour
{
    [SerializeField] private Rigidbody[] _allRigidbodies;

    private Animator _animator;
    public void MakeRagdoll()
    {
        _animator.enabled = false;
        for (int i = 0; i < _allRigidbodies.Length; i++)
        {
            _allRigidbodies[i].isKinematic = false;
        }
    }
    void Awake()
    {
        _animator = GetComponent<Animator>();
        for (int i = 0; i < _allRigidbodies.Length; i++) {
            _allRigidbodies[i].isKinematic = true;
        }
    }
}

