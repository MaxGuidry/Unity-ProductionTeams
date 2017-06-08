using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : ScriptableObject, IDamagable
{

    public int health;
    public void TakeDamage(int damage)
    {
        health -= damage;
    }
}
