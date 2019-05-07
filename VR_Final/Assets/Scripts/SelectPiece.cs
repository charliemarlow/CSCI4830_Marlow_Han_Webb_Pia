using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectPiece : MonoBehaviour
{
    public Canvas sp;
    public GameManager gm;

    private void Awake()
    {
        sp.enabled = false;
    }


    public void kingButton()
    {
        sp.enabled = false;
        //gm.pawnPromote()
    }
    public void queenButton()
    {
        sp.enabled = false;
    }
    public void bishopButton()
    {
        sp.enabled = false;
    }
    public void knightButton()
    {
        sp.enabled = false;
    }
    public void rookButton()
    {
        sp.enabled = false;
    }
}
