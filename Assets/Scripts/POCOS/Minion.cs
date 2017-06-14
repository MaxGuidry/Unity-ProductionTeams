using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Minion", menuName = "Minion", order = 0)]
public class Minion : ScriptableObject, IDamagable,IDamager
{

    public int health;
    public int damage;
    public enum MinionType
    {
        PLAYER,
        ENEMY,
    }
    public MinionType minionType ;

    void OnEnable()
    {
        minionType = MinionType.ENEMY;
    }
    //add to animation event
    //public void Attack(Tower target)
    //{
    //    target.health -= damage;
    //}

    //public void TakeDamage(int amount)
    //{
    //    health -= amount;
    //}

    public void DoDamage(IDamagable target)
    {
        target.TakeDamage(damage);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
    }
}
