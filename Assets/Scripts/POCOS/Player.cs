using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : ScriptableObject, IDamager
{

    public int health;
    public int damage;
    public float xp;
    public float maxXP;
    public int crystals;
    public int level;
    [SerializeField]
    private GameObject spawner1;
    [SerializeField]
    private GameObject spawner2;
    public void DoDamage(IDamagable target)
    {
        throw new System.NotImplementedException();
    }

    public void SpawnMinion()
    {
        if (crystals > 25)
        {
            int i = Random.Range(1, 3);
            switch (i)
            {
                case 1:
                {
                    spawner1.GetComponent<MinionSpawner>().Spawn(100 * level, 5 * level);
                    break;
                }
                case 2:
                {
                    spawner2.GetComponent<MinionSpawner>().Spawn(100 * level, 5 * level);
                    break;
                }
            }
            crystals -= 25;
        }
    }

}
