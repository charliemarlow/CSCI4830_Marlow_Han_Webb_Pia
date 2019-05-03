﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public bool raycastMode;
    public Leaderboard leader;

    public void raycastSelect()
    {
        // gets called when raycastMode is on and trigger is pulled
    }

    // Start is called before the first frame update
    void Start()
    {
        leader = GetComponent<Leaderboard>();
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
        leader.writeToLeaderboard(score, time, name);

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
