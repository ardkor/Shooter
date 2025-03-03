using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEntity : MonoBehaviour
{
    [SerializeField] private int _maxHp = 100;
    [SerializeField] private int _hp;

    private BodyRagdoll _bodyRagdoll;

    public void TakeDamage(int damage)
    {
        _hp -= damage;
        _hp = Mathf.Clamp(_hp, 0, _maxHp);
        EventBus.Instance.playerHpChanged(_hp);
        if (_hp <= 0)
        {
            EventBus.Instance.playerDied?.Invoke();
            _bodyRagdoll.MakeRagdoll();
        }
    }

    private void Start()
    {
        _hp = _maxHp;
        EventBus.Instance.playerHpChanged?.Invoke(_hp);
        _bodyRagdoll = GetComponent<BodyRagdoll>();
    }
    private void OnTriggerEnter(Collider collider)
    {
        Bullet bullet = collider.GetComponent<Bullet>();
        if (bullet)// != null)
        {
            TakeDamage(bullet.Damage);
        }
    }
}
