using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameController : MonoBehaviour
{
    public GameObject enemyTower;
    public GameObject playerTower;
    public Canvas pauseMenu;
    public Canvas GameOverMenu;
    private bool paused;
    private bool gameOver;
    // Use this for initialization
    void Start()
    {
        paused = false;
        gameOver = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !paused)
        {
            Pause();

        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            Unpause();
        }
        if (enemyTower.GetComponent<TowerBehaviour>().tower.health <= 0)
        {
            Win();
        }
        else if (playerTower.GetComponent<TowerBehaviour>().tower.health <= 0)
        {
            Lose();
        }

    }

    public void Pause()
    {
        Time.timeScale = 0;
        paused = true;
        pauseMenu.enabled = true;
    }
    public void Unpause()
    {
        Time.timeScale = 1;
        paused = false;
        pauseMenu.enabled = false;
    }
    public void Lose()
    {
        GameOverMenu.enabled = true;
        var minions = FindObjectsOfType<MinionBehaviour>().ToList();
        foreach (var min in minions)
        {
            if (min.minion.minionType == Minion.MinionType.PLAYER)
                min.minion.TakeDamage(50000);
            else
                min.anim.SetTrigger("idle");
        }
    }
    public void Win()
    {
        GameOverMenu.enabled = true;
        var minions = FindObjectsOfType<MinionBehaviour>().ToList();
        foreach (var min in minions)
        {
            if (min.minion.minionType == Minion.MinionType.ENEMY)
                min.minion.TakeDamage(50000);
            else
                min.anim.SetTrigger("idle");
        }
    }


}
