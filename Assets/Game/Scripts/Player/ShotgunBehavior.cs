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

    [SerializeField] private GameObject _body;

    //[SerializeField] private Transform _parent;
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

    [SerializeField] private Transform _aimingBinding;
    [SerializeField] private bool _inHands;
    [SerializeField] private Vector3 _offset;

    private Transform _parent;

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
        _rigidbody.isKinematic = false;
    }
    public void SetDirectionPoint(Vector3 directionPoint)
    {
        this.directionPoint = directionPoint;
    }
    public void SetAiming(bool aiming)
    {
        if (!_inHands)
        {
            if (_aiming != aiming)
            {
                if (aiming)
                {

                    _animation.Play("air_shooting_positioning");
                    _aiming = true;
                }
                else
                {
                    _animation.PlayQueued("air_shooting_to_default_positioning");
                    _aiming = false;
                }
            }
        }
        else
        {
            if (_aiming != aiming)
            {
                if (aiming)
                {
                    _aiming = true;
                    _animation.Play("to_hands_positioning");
                    AnimationState animState = _animation["to_hands_positioning"];


                    //transform.SetParent(_aimingBinding, true);
                    //transform.parent = _aimingBinding;
                    StartCoroutine(AimingInHand(animState));

                }
                else
                {
                    transform.parent = _parent;
                    //gameObject.layer = LayerMask.NameToLayer("Player");
                    //_body.layer = LayerMask.NameToLayer("Player");

                    _animation.PlayQueued("out_of_hands_positioning");
                    _aiming = false;
                }
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
                if (_inHands)
                    _animation.Play("shotgun_recoil_in_hands");
                else
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
                if (_inHands)
                    _animation.Play("shotgun_recoil_in_hands");
                else
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
        _parent = transform.parent;
    }

    void FixedUpdate()
    {
        if (_aiming)
        {
            //transform.position = _aimingBinding.position + _offset;
            //transform.position = _aimingBinding.position;
            //transform.LookAt(directionPoint);

            //transform.forward = _directionParent.forward;
            //_rigidbody.MovePosition(_aimingBinding.position + _offset);
        }
    }
    private IEnumerator AimingInHand(AnimationState animState)
    {
        //yield return new WaitForSeconds(animState.length);

        yield return new WaitWhile(() => animState != null && animState.enabled && animState.normalizedTime < 1.0f);
        yield return new WaitForEndOfFrame();
        if (_aiming)
        {
            transform.SetParent(_aimingBinding, true);
            //gameObject.layer = LayerMask.NameToLayer("PlayerFPV");
            //_body.layer = LayerMask.NameToLayer("PlayerFPV");
        }
        //transform.parent = null;
        //Debug.Log(transform.localPosition);
        //transform.localPosition = transform.localPosition + _offset;

    }

    private IEnumerator Reload()
    {
        _canFire = false;

        if (_patronsRemain > 0)
        {
            if (_inHands)
                _animation.Play("shotgun_recharge_in_hands");
            else
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
