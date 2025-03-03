using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotGunBehavior : MonoBehaviour
{
    [SerializeField] private GameObject _bullet;
    [SerializeField] private Transform _bulletSpawn;

    [SerializeField] private Transform _directionParent;
    [SerializeField] private Transform _leftArm;
    //[SerializeField] private Transform parent_right_arm;

    //[SerializeField] private Vector3 offset;

    private Collider col;
    private Rigidbody rb;
    private bool _connected;

    [SerializeField] private float fireRate = 0.9f;
    [SerializeField] private float reloadTime = 1.5f;

    private bool _canFire;

    private Coroutine fireCoroutine;
    private Coroutine reloadCoroutine;

    private Vector3 _playerCenterOffset; 

    public void TryShoot(Vector3 playerPosition)
    {
        if (_canFire)
        {
            fireCoroutine = StartCoroutine(FireRate());
            Instantiate(_bullet, _bulletSpawn.position, Quaternion.LookRotation(playerPosition + _playerCenterOffset - _bulletSpawn.position, Vector3.up));
        }
    }

    public void Unconnect()
    {
        rb.useGravity = true;
        col.enabled = true;
        _connected = false;
        //enabled = false;
    }
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
        col.enabled = false;
        rb.useGravity = false;
        _playerCenterOffset = new Vector3(0, 1.3f, 0);
        _connected = true;
        _canFire = true;
    }

    void Update()
    {
        if (_connected)
        {
            transform.forward = _directionParent.forward;
            rb.MovePosition((_leftArm.position));
        }
        //follower.position = parent_arm.position + offset;
        //rb.MovePosition(new Vector3(((parent_right_arm.position.x + parent_left_arm.position.x)/2), ((parent_right_arm.position.y + parent_left_arm.position.y)/ 2),((parent_right_arm.position.z + parent_left_arm.position.z)/2))+Vector3.forward);
        //(Mathf.Sqrt(Mathf.Pow((parent_right_arm.position.x - parent_left_arm.position.x), 2))
    }
    private IEnumerator Reload()
    {
        _canFire = false;
        yield return new WaitForSeconds(reloadTime);
        _canFire = true;
    }
    private IEnumerator FireRate()
    {
        _canFire = false;
        yield return new WaitForSeconds(fireRate);
        _canFire = true;
    }
}
