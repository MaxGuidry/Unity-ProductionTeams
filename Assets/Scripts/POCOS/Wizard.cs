using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Events;

public class Wizard : ScriptableObject,IDamager
{
    public GameObject fireball;
    public int damage = 8;
    public void Attack(Minion minion)
    {
        minion.TakeDamage(damage);
    }


  

    public void DoDamage(IDamagable target)
    {
        target.TakeDamage(damage);
    }
    public class OnFireBallHit : UnityEvent
    { }

}
