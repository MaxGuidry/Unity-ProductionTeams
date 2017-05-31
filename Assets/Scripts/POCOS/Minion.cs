using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public IEnumerator Attack(Tower target)
    {
        
        while (true)
        {
            target.health -= damage;
            yield return new WaitForSeconds(.6f);
        }
    }
}
