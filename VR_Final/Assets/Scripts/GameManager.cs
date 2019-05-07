﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Tutorial tuts;
    public AudioSource sound;
    public bool raycastMode;
    public Leaderboard leader;
    public Board board;
    public LaserFingers left;
    public LaserFingers right;

    public Tutorial setPieces;
    public Tutorial moves;
    public Tutorial queens;
    public Tutorial kings;

    public AudioClip setBoard;
    public AudioClip setBoardPawn;
    public AudioClip setBoardRook;
    public AudioClip setBoardKnight;
    public AudioClip setBoardBishop;
    public AudioClip setBoardQueen;
    public AudioClip setBoardKing;

    public AudioClip move;
    public AudioClip movePawn;
    public AudioClip moveRook;
    public AudioClip moveKnight;
    public AudioClip moveBishop;
    public AudioClip moveQueen;
    public AudioClip moveKing;
    public AudioClip openings;
    public AudioClip kingsDefense;
    public AudioClip queensGambit;

    public List<AudioClip> setBoardClips = new List<AudioClip>();
    public List<AudioClip> moveClips = new List<AudioClip>();
    public List<AudioClip> kingClips = new List<AudioClip>();
    public List<AudioClip> queenClips = new List<AudioClip>();


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
            Debug.Log("before soundclips");
                setBoardClips.Add(setBoard);
                setBoardClips.Add(setBoardPawn);
                setBoardClips.Add(setBoardRook);
                setBoardClips.Add(setBoardKnight);
                setBoardClips.Add(setBoardBishop);
                setBoardClips.Add(setBoardQueen);
                setBoardClips.Add(setBoardKing);
                Debug.Log("sound list" + setBoardClips.Count);
                tuts = Instantiate(setPieces);
                tuts.setAudio(setBoardClips);
                tuts.setSource(sound);
                break;
            case 1:
                moveClips.Add(move);
                moveClips.Add(movePawn);
                moveClips.Add(moveRook);
                moveClips.Add(moveKnight);
                moveClips.Add(moveBishop);
                moveClips.Add(moveQueen);
                moveClips.Add(moveKing);
                tuts = Instantiate(moves);
                tuts.setAudio(moveClips);
                tuts.setSource(sound);
                break;
            case 2:
                queenClips.Add(openings);
                queenClips.Add(queensGambit);
                tuts = Instantiate(queens);
                tuts.setAudio(queenClips);
                tuts.setSource(sound);
                break;
            case 3:
                kingClips.Add(openings);
                kingClips.Add(kingsDefense);
                tuts = Instantiate(kings);
                tuts.setAudio(kingClips);
                tuts.setSource(sound);
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


    // Update is called once per frame
    void Update()
    {

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

    }



}
