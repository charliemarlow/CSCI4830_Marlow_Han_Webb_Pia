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
    }


    public void StartButton()
    {
        main.enabled = false;
    }

    public void TutorialButton()
    {
        main.enabled = false;
        tutorial.enabled = true;
    }

    public void HighScoresButton()
    {

    }

}
