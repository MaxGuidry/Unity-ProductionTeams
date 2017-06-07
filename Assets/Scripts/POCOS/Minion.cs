using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Minion", menuName = "Minion", order = 0)]
public class Minion : ScriptableObject
{

    public int health;
    public int damage;
    public enum MinionType
    {
        PLAYER,
        ENEMY,
    }
    public MinionType minionType;
    //add to animation event
    public void Attack(Tower target)
    {
        target.health -= damage;
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
    }
}
