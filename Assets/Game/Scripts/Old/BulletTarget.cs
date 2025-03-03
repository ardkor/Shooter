using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTarget : MonoBehaviour
{
    // Start is called before the first frame update
    void OnCollisionEnter()
    {
        Debug.Log("collision");
    }

    // Update is called once per frame
    void OnTriggerEnter()
    {
        Debug.Log("triggered");
    }
}
