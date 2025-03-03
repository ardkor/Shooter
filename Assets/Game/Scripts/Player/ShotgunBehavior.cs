using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ShotgunBehavior : MonoBehaviour
{

    [SerializeField] private Transform spawn_shotgun_BulletPosition_right;
    [SerializeField] private Transform spawn_shotgun_BulletPosition_left;
    [SerializeField] private Transform spawn_shotgun_roza_left;
    [SerializeField] private Transform spawn_shotgun_roza_right;

    [SerializeField] private GameObject _bullet;
    [SerializeField] private GameObject vfxShoot;

    [SerializeField] private Transform parent;

    [SerializeField] private LayerMask aimColliderLayerMask;

    private Animation _animation;

    private Rigidbody _rigidbody;
    private Collider _collider;

    private Vector3 directionPoint;

    [SerializeField] private float fireRate = 0.2f;
    [SerializeField] private float reloadTime = 1.5f;

    private bool _firstShot = true; //?

    private bool _aiming;

    private bool _canFire;

    private Coroutine fireCoroutine;
    private Coroutine reloadCoroutine;

    public void Unconnect()
    {
       // unconnected = true;
        _collider.enabled = true;
        transform.parent = null;
        AirGunFollow script = GetComponent<AirGunFollow>();
        ShotgunBehavior script2 = GetComponent<ShotgunBehavior>();
        _rigidbody.useGravity = true;
        script.enabled = false;
        script2.enabled = false;
    }
    public void SetDirectionPoint(Vector3 directionPoint)
    {
        this.directionPoint = directionPoint;
    }
    public void SetAiming(bool aiming)
    {
        if (_aiming != aiming)
        {
            if (aiming)
            {

                _animation.Play("posing");
                _aiming = true;
            }
            else
            {
                _animation.PlayQueued("unposing");
                _aiming = false;
            }
        }
    }

    public void TryShoot()
    {
         Vector3 bulletDirection = (directionPoint - spawn_shotgun_BulletPosition_right.position).normalized;

        if (_canFire)
        {
            if (_firstShot)
            {
                fireCoroutine = StartCoroutine(FireRate());
                _animation.Play("shotgun_recoil");
                Instantiate(_bullet, spawn_shotgun_BulletPosition_right.position, Quaternion.LookRotation(bulletDirection, Vector3.up));
                Instantiate(vfxShoot, spawn_shotgun_roza_right.position, Quaternion.identity);
                _firstShot = false;
            }
            else
            {
                reloadCoroutine = StartCoroutine(Reload());
                _animation.Play("shotgun_recoil");
                Instantiate(_bullet, spawn_shotgun_BulletPosition_left.position, Quaternion.LookRotation(bulletDirection, transform.up));
                Instantiate(vfxShoot, spawn_shotgun_roza_left.position, Quaternion.identity);
                _firstShot = true;
                _animation.Play("shotgun_recharge");
            }
        }
    }


    void Start()
    {
        _canFire = true;
        _rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
        _collider.enabled = false;
        _animation = GetComponent<Animation>();
        _animation.Play("shotgun_pos_1");
    }

    void Update()
    {
        if (_aiming)
        {
            transform.LookAt(directionPoint);
        }
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
