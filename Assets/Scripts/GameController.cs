using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.SceneManagement;
public class GameController : MonoBehaviour
{
    public GameObject enemyTower;
    public GameObject playerTower;
    public Canvas pauseMenu;
    public Canvas GameOverMenu;
    public Text winlose;
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
        if (enemyTower.GetComponent<TowerBehaviour>().tower.health <= 0 && !gameOver)
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
        pauseMenu.gameObject.SetActive(true);
    }
    public void Unpause()
    {
        Time.timeScale = 1;
        paused = false;
        pauseMenu.gameObject.SetActive(false);
    }
    public void Lose()
    {
        winlose.text = "<b>You Lose</b>";
        gameOver = true;
        GameOverMenu.gameObject.SetActive(true);
        var minions = FindObjectsOfType<MinionBehaviour>().ToList();
        var spawners = FindObjectsOfType<MinionSpawner>().ToList();
        foreach (var minionSpawner in spawners)
        {
            minionSpawner.enabled = false;
        }
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
        winlose.text = "<b>You Win</b>";
        gameOver = true;
        GameOverMenu.gameObject.SetActive(true);
        var minions = FindObjectsOfType<MinionBehaviour>().ToList();
        var spawners = FindObjectsOfType<MinionSpawner>().ToList();
        foreach (var minionSpawner in spawners)
        {
            minionSpawner.enabled = false;
        }
        foreach (var min in minions)
        {
            if (min.minion.minionType == Minion.MinionType.ENEMY)
                min.minion.TakeDamage(50000);
            else
                min.anim.SetTrigger("idle");
        }
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("101.mainmenu");
    }

    public void Restart()
    {
        SceneManager.LoadScene("101.minionspawner");
    }
}
