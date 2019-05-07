using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    public Canvas main, tutorial, leaderboard, difficulty;


    // Start is called before the first frame update
   void Start()
    {
        main.enabled = true;
        tutorial.enabled = false;
        leaderboard.enabled = false;
        difficulty.enabled = false;
    }


    public void StartButton()
    {
        Debug.Log("in start menu");
        main.enabled = false;
        difficulty.enabled = true;
    }

    public void TutorialButton()
    {
        Debug.Log("STARTING TUTORIAL HERE");
        main.enabled = false;
        tutorial.enabled = true;
    }

    public void HighScoresButton()
    {
        main.enabled = false;
        leaderboard.enabled = true;
    }

}
