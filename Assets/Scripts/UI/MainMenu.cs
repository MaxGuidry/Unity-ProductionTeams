using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MainMenu : MonoBehaviour
{
    public Text MenuExitText;
    public Text MenuStartText;
    public Text InstructionsText;
    public Text CreditsText;
    
    void Start()
    {

    }
    public void MenuExit()
    {
        MenuExitText.text = "Exit Pressed";
    
    }
    public void MenuStart()
    {
        SceneManager.LoadScene("101.minionspawner");   
    }
    public void Instructions()
    {
        InstructionsText.text = "Duh Instructions";
    }
    public void Credits()
    {
        CreditsText.text = "it worked";
    }
    void Update()
    {

    }
}
