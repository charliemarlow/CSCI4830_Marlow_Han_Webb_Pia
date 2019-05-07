using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    public Canvas menu;
    public Button tutorial, start, leaderboard;
    public ControllerInput left, right;
    public LaserFingers laser;
    private bool active;


    // Start is called before the first frame update
    void Start()
    {
        active = false;
        menu.enabled = true;
    }

    void Update()
    {




    }


    public void StartButton()
    {

        menu.enabled = false;
    }

    public void TutorialButton()
    {

    }

    public void HighScoresButton()
    {

    }

}
