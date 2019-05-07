using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public bool raycastMode;
    public Leaderboard leader;
    public Board board;
    public LaserFingers left;
    public LaserFingers right;

    public Tutorial setPieces;
    public Tutorial moves;
    public Tutorial queens;
    public Tutorial kings;

    public void setTutorial(bool tut)
    {
        board.setIsTutorial(tut);
    }

    public bool getTutorial()
    {
        return board.getIsTutorial();
    }

    public void startTutorial(int tut){
        switch(tut){
            case 0:
                Instantiate(setPieces);
                break;
            case 1:
                Instantiate(moves);
                break;
            case 2:
                Instantiate(queens);
                break;
            case 3:
                Instantiate(kings);
                break;
        }

    }

    public void raycastSelect(ControllerInput controller)
    {
        // gets called when raycastMode is on and trigger is pulled
        if (controller.isLeft)
        {
            Debug.Log(left.selectRaycast().name);
        }
        else
        {
            Debug.Log(right.selectRaycast().name);
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        leader = GetComponent<Leaderboard>();
        GameObject boardGo = GameObject.FindWithTag("board");
        board = boardGo.GetComponent<Board>();
        startTutorial(3);
    }

    public void exitTutorial(){
        board.isMoveTutorial =false;
        board.isTutorial = false;
        Destroy(board.tutorial.gameObject);
        board.tutorial = null;
    }

    public void clackNoise()
    {

    }

    public void makeHappy()
    {

    }

    public void makeSad()
    {

    }

    public void makeThink()
    {

    }
    
    public string pawnPromote()
    {
        // add menu options for what to promote a pawn to
        return "queen";
    }

    public void finishedGame(float score, float time)
    {
        // signals to the manager that the game is finished
        // add options for starting game or going back to main menu here
        // probably want options for leaderboard stuff here as well
        Debug.Log("Score = " + score);
        Debug.Log("Time = " + time);

        string name = "tester";
        //leader.writeToLeaderboard(score, time, name);

    }

    public void moveAvatarHand(ChessPiece movingPiece, int x, int y)
    {
        int oldX = movingPiece.currentX;
        int oldY = movingPiece.currentY;

        // oldx oldy is the board location of the piece currently, x,y is the new location
        // the avatar hand should move to
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
