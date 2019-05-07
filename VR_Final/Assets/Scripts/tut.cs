using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class tut : MonoBehaviour
{
    public Canvas t;
    public GameManager gm;

    private void Awake()
    {
        //t.enabled = false;
    }

    public void tut1()
    {
        Debug.Log("Tut 1 starting");
        t.enabled = false;
        gm.raycastMode = false;
        gm.startTutorial(0);
    }

    public void tut2()
    {
        t.enabled = false;
        gm.raycastMode = false;
        gm.startTutorial(1);
    }

    public void tut3()
    {
        t.enabled = false;
        gm.raycastMode = false;
        gm.startTutorial(2);
    }
    public void tut4()
    {
        t.enabled = false;
        gm.raycastMode = false;
        gm.startTutorial(3);
    }
}
