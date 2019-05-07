using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    public Canvas main, tutorial, leaderboard, difficulty;


    // Start is called before the first frame update
    private void Awake()
    {
        main.enabled = true;
        tutorial.enabled = false;
        leaderboard.enabled = false;
        difficulty.enabled = false;
    }


    public void StartButton()
    {
        main.enabled = false;
        difficulty.enabled = true;
    }

    public void TutorialButton()
    {
        main.enabled = false;
        tutorial.enabled = true;
    }

    public void HighScoresButton()
    {
        leaderboard.enabled = true;
    }

}
