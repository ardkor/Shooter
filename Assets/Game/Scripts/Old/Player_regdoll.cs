using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_regdoll : MonoBehaviour
{
    public static bool died;
    private Animator Animator;
    [SerializeField] private Rigidbody[] AllRigidbodies;
    void Awake()
    {
        Animator = GetComponent<Animator>();
        for (int i = 0; i < AllRigidbodies.Length; i++)
        {
            AllRigidbodies[i].isKinematic = true;
        }
    }
    void Update()
    {
        if (died)
        {
            MakePhysical();
        }
    }
    void MakePhysical()
    {
        Animator.enabled = false;
        for (int i = 0; i < AllRigidbodies.Length; i++)
        {
            AllRigidbodies[i].isKinematic = false;
        }
    }
}
