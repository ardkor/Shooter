using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RobotBehavior : MonoBehaviour
{
    [SerializeField] private BotGunBehavior _botGunBehavior;

    [SerializeField] private Transform _firstPoint;
    [SerializeField] private Transform _secondPoint;

    private Animator animator;
    private NavMeshAgent agent;
    private Transform _player;
    private Observer _observer;

    private Vector3 target;

    private float distance;
    private float targetSpeed;
    private float SpeedChangeRate = 5f;

    private bool _onFirstPath;
    private bool _movementEnabled;

    private bool _playerAlive = true;

    public void DisableMovement()
    {
        agent.enabled = false;
        _movementEnabled = false;
    }
    private void OnEnable()
    {
        EventBus.Instance.playerDied += SetPlayerDead;
    }

    private void OnDisable()
    {
        EventBus.Instance.playerDied -= SetPlayerDead;
    }
    private void SetPlayerDead()
    {
        _playerAlive = false;
    }
    private void Awake()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        _player = FindFirstObjectByType<PlayerEntity>().transform;
        _observer = GetComponentInChildren<Observer>();
    }
    private void Start()
    {
        _movementEnabled = true;
        targetSpeed = 2.5f;
        _onFirstPath = true;
        agent.destination = _firstPoint.position;
    }
    private void CalculateDistance()
    {
        Vector3 path = agent.destination - transform.position;
        distance = path.magnitude;
    }
    private void TryChangeDestinationPoint()
    {
        if (distance <= 1f && !_observer.observing)
        {
            if (_onFirstPath)
            {
                agent.destination = _secondPoint.position;
                _onFirstPath = false;
            }
            else if (!_onFirstPath)
            {
                agent.destination = _firstPoint.position;
                _onFirstPath = true;
            }
        }
    }
    private void ContinueDestinating()
    {
        if (_onFirstPath)
        {
            agent.destination = _firstPoint.position;
        }
        else if (!_onFirstPath)
        {
            agent.destination = _secondPoint.position;
        }
    }
    private void Update()
    {
        if (_movementEnabled)
        {
            agent.speed = Mathf.Lerp(agent.speed, targetSpeed, Time.deltaTime * SpeedChangeRate);
            animator.SetFloat("speed", agent.speed);

            CalculateDistance();
            TryChangeDestinationPoint();

            if (_playerAlive)
            {
                if (_observer.observed)
                {
                    if (_observer.observing)
                    {
                        transform.rotation = Quaternion.LookRotation(_player.position - transform.position, Vector3.up);
                        targetSpeed = 0f;
                        agent.destination = _player.position;
                        _botGunBehavior.TryShoot(_player.position);
                    }
                    else
                    {
                        targetSpeed = 4.2f;
                    }
                }
                else
                {
                    if (!_observer.observing)
                    {
                        targetSpeed = 2.5f;
                        ContinueDestinating();
                    }
                }
            }
            else
            {
                targetSpeed = 2.5f;
                ContinueDestinating();
            }
        }
    }
}