using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bot_Gun_bullet : MonoBehaviour
{
    private Rigidbody bulletRigidbody;
    [SerializeField] float speed;
    [SerializeField] private Transform vfxHit;
    [SerializeField] private Transform gun;
   /* private Transform transformik;
    [SerializeField] private LayerMask aimColliderLayerMask;*/

    private void Awake()
    {
        bulletRigidbody = GetComponent<Rigidbody>();
       // transformik = GetComponent<Transform>();
    }
    private void Start()
    {  
        bulletRigidbody.velocity = transform.forward * speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<BulletTarget>() != null)
        {
            Instantiate(vfxHit, transform.position, Quaternion.identity);
        }
        else if (other.GetComponent<Bot_behavior>() != null)
            Destroy(gameObject);
    }
}
