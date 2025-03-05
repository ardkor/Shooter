using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ShotgunBehavior : MonoBehaviour
{

    [SerializeField] private Transform _bulletSpawnRight;
    [SerializeField] private Transform _bulletSpawnLeft;
    [SerializeField] private Transform _shotVfxSpawnLeft;
    [SerializeField] private Transform _shotVfxSpawnRight;

    [SerializeField] private GameObject _bullet;
    [SerializeField] private GameObject _vfxShot;

    [SerializeField] private Transform parent;
    [SerializeField] private AudioClip _fire;

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

    private AmmunitionPanel _ammunitionPanel;

    private int _patronsRemain = 12;

    private void OnEnable()
    {
        EventBus.Instance.playerDied += Unconnect;
    }

    private void OnDisable()
    {
        EventBus.Instance.playerDied -= Unconnect;
    }

    private void Unconnect()
    {
       // unconnected = true;
        _collider.enabled = true;
        transform.parent = null;
        _rigidbody.useGravity = true;
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
         Vector3 bulletDirection = (directionPoint - _bulletSpawnRight.position).normalized;

        if (_canFire)
        {
            if (_firstShot)
            {
                SoundManager.Instance.PlaySound(_fire, _shotVfxSpawnRight.position);
                _animation.Play("shotgun_recoil");
                Instantiate(_bullet, _bulletSpawnRight.position, Quaternion.LookRotation(bulletDirection, Vector3.up));
                Instantiate(_vfxShot, _shotVfxSpawnRight.position, Quaternion.identity);
                _firstShot = false;
                _ammunitionPanel.SetPatronsCharged(1);
                fireCoroutine = StartCoroutine(FireRate());
            }
            else
            {
                SoundManager.Instance.PlaySound(_fire, _shotVfxSpawnLeft.position);
                _animation.Play("shotgun_recoil");
                Instantiate(_bullet, _bulletSpawnLeft.position, Quaternion.LookRotation(bulletDirection, transform.up));
                Instantiate(_vfxShot, _shotVfxSpawnLeft.position, Quaternion.identity);
                _firstShot = true;
                
                _ammunitionPanel.SetPatronsCharged(0);
                reloadCoroutine = StartCoroutine(Reload());
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
        _ammunitionPanel = FindObjectOfType<AmmunitionPanel>();
        _ammunitionPanel.SetPatronsCharged(2);
        _ammunitionPanel.SetPatronsRemains(_patronsRemain);
    }

    void Update()
    {
        //unconnected
        if (_aiming)
        {
            transform.LookAt(directionPoint);
        }
    }
    private IEnumerator Reload()
    {
        _canFire = false;
        
        if (_patronsRemain > 0)
        {
            _animation.Play("shotgun_recharge");
            yield return new WaitForSeconds(reloadTime);
            _canFire = true;
            _patronsRemain -= 2;
            _ammunitionPanel.SetPatronsCharged(2);
            _ammunitionPanel.SetPatronsRemains(_patronsRemain);
        }
    }
    private IEnumerator FireRate()
    {
        _canFire = false;
        yield return new WaitForSeconds(fireRate);
        _canFire = true;
    }
}
