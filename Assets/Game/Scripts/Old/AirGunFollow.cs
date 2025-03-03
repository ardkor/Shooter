using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AirGunFollow : MonoBehaviour
{
    [SerializeField] private Transform follower;
    //    private Transform trans=null;
    int counter;
    [SerializeField] private Vector3 offset;      // A variable that allows us to offset the position (x, y, z)
    [SerializeField] private Vector3 rotation;
    private Vector3 rot;

    private Transform transformik;


    private void Awake()
    {

        transformik = GetComponent<Transform>();
    }
    void Update()
    {
        
    }
}

