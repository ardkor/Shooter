using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEntity : MonoBehaviour
{
    [SerializeField] private int _maxHp = 100;
    [SerializeField] private int _hp;

    private BodyRagdoll _bodyRagdoll;

    private HpBar _hpBar;

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
        _hpBar = FindObjectOfType<HpBar>();
        _bodyRagdoll = GetComponent<BodyRagdoll>();
        _hpBar.SetMaxHp(_maxHp);
        _hpBar.UpdateHpDisplay(_maxHp);
    }
    private void OnCollisionEnter(Collision other)
    {
        Bullet bullet = other.gameObject.GetComponent<Bullet>();
        if (bullet)// != null)
        {
            TakeDamage(bullet.Damage);
        }
    }
}
