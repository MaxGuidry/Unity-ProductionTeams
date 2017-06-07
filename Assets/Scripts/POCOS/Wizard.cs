using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Events;

public class Wizard : ScriptableObject
{

    public int damage = 5;
    public float attackCooldown = 2f;
    public void Attack(Minion minion)
    {
        minion.TakeDamage(damage);
    }


    //maybe
    public class OnLevelUp : UnityEvent
    {

    }
}
