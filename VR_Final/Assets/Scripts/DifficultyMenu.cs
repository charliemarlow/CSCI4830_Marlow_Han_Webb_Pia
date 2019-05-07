using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyMenu : MonoBehaviour
{
    public Canvas dm;
    public GameManager gm;

    private void Awake()
    {
        dm.enabled = false;
    }

    public void easyButton()
    {
        dm.enabled = false;
    }

    public void mediumButton()
     {
        dm.enabled = false;
    }

    public void challengingButton()
    {
        dm.enabled = false;
    }

}
