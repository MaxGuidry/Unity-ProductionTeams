using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MainMenu : MonoBehaviour
{
    
    public Text MenuStartText;
    public Text InstructionsText;
    public Text CreditsText;
    
    void Start()
    {

    }
    public void MenuExit()
    {
        
        Application.Quit();
    
    }
    public void MenuStart()
    {
        SceneManager.LoadScene("101.minionspawner");   
    }
    public void Instructions()
    {
        InstructionsText.text = "Left Click- Left click on any minion to attack it!!" +"\n" + "Right Click- Right click anywhere on the map to move there!!" + "\n" + "W,S,A,D- This will allow the player to navigate through the map!!";
        
    }
    public void Credits()
    {
        CreditsText.text = "Max Guidry-Programming"+ "\n"+ "Dylan Mitchell-Programming" + "\n" + "Ricky Pham-Artist"+  "\n" + "Eric Shelling-Artist" + "\n" + "Dustin Pouncey-Artist";
    }

    public void GetText(Text textObj)
    {
        textObj.enabled = !textObj.enabled;
    }
    void Update()
    {

    }
}
