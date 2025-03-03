using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunFollower : MonoBehaviour
{
    private Transform follower;
   // private GameObject gm;
    [SerializeField] private Transform parent_right_arm;
    [SerializeField] private Transform parent;
    [SerializeField] private Transform parent_left_arm;
    //    private Transform trans=null;
    int counter;
    private Collider col;
    private Rigidbody rb;
    [SerializeField] private Vector3 offset;
    public static bool unconnected=false;
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
        follower = GetComponent<Transform>();
        //  gm = GetComponent<GameObject>();
        col.enabled = false;
        rb.useGravity = false;
    }

    void Update()
    {
        Debug.Log(follower.position);
        if (unconnected)
        {
            rb.useGravity = true;
            col.enabled = true;
            GunFollower script = GetComponent<GunFollower>();
            script.enabled = false;
        }
         follower.forward = parent.forward;
        //follower.position = parent_arm.position + offset;
        //rb.MovePosition(new Vector3(((parent_right_arm.position.x + parent_left_arm.position.x)/2), ((parent_right_arm.position.y + parent_left_arm.position.y)/ 2),((parent_right_arm.position.z + parent_left_arm.position.z)/2))+Vector3.forward);
        //(Mathf.Sqrt(Mathf.Pow((parent_right_arm.position.x - parent_left_arm.position.x), 2))
        rb.MovePosition((parent_left_arm.position));
    }
}
