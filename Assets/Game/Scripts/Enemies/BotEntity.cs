using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotEntity : MonoBehaviour
{
    [SerializeField] private int _maxHp = 40;
    [SerializeField] private int _hp;

    [SerializeField] private BotGunBehavior gunFollower;

    private BodyRagdoll bodyRagdoll;
    private RobotBehavior robotBehavior;

    public void TakeDamage(int damage)
    {
        _hp -= damage;
        _hp = Mathf.Clamp(_hp, 0, _maxHp);
        if (_hp <= 0)
        {
            bodyRagdoll.MakeRagdoll();
            gunFollower.Unconnect();
            robotBehavior.DisableMovement();
        }
    }

    private void Awake()
    {
        bodyRagdoll = GetComponent<BodyRagdoll>();
        robotBehavior = GetComponent<RobotBehavior>();
        _hp = _maxHp;
    }

    void OnTriggerEnter(Collider collider)
    {
        Bullet bullet = collider.GetComponent<Bullet>();
        if (bullet)// != null)
        {
            TakeDamage(bullet.Damage);
        }
    }
}
