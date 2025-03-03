using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BodyRagdoll : MonoBehaviour
{
    public static bool died;
    private Animator Animator;
    private NavMeshAgent  agent;
    [SerializeField] private Rigidbody[] AllRigidbodies;
    void Awake()
    {
        Animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        for (int i = 0; i < AllRigidbodies.Length; i++) {
            AllRigidbodies[i].isKinematic = true;
        }
    }
    void Update()
    {
       /* if (died)
        {
            MakePhysical();
        }*/
    }
    void MakePhysical()
    {
        agent.enabled = false;
        Animator.enabled= false;
        for (int i = 0; i < AllRigidbodies.Length; i++)
        {
            AllRigidbodies[i].isKinematic = false;
        }
    }
}

