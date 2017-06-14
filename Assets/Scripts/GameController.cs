using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
    public GameObject enemyTower;
    public GameObject playerTower;
    public Canvas pauseMenu;
    private bool paused;
    private bool gameOver;
	// Use this for initialization
	void Start () {
        paused = false;
        gameOver = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Escape) && !paused)
        {
            Pause();

        }
        else if(Input.GetKeyDown(KeyCode.Escape))
        {
            Unpause();
        }
        if (enemyTower.GetComponent<TowerBehaviour>().tower.health <= 0)
        {
            Win();
        }
        else if(playerTower.GetComponent<TowerBehaviour>().tower.health <= 0)
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

    }
    public void Win()
    {

    }

   
}
