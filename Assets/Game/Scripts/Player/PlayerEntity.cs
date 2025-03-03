using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEntity : MonoBehaviour
{
    [SerializeField] private ShotgunBehavior _shotgunBehavior;

    [SerializeField] private int _hp;

    public void TakeDamage(int damage)
    {
        _hp -= damage;

        Player_regdoll.died = true;
        _shotgunBehavior.Unconnect();
    }

    private int low_gun_damage = 6;
    void OnTriggerEnter(Collider collider)
    {
        Bullet bullet = collider.GetComponent<Bullet>();
        if (bullet)// != null)
        {
            TakeDamage(bullet.Damage);
        }

    }
}
