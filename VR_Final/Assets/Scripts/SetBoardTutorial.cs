using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetBoardTutorial : Tutorial
{

    public List<AudioClip> soundClips;
    AudioSource mysound;
    public (int, int) lastHighlightLocation;

    public List<GameObject> myPrefabs = new List<GameObject>();
    private int index = 0;


    public override void pickupPiece(ChessPiece piece)
    {
        //Destroy(piece.gameObject);
        Debug.Log("I picked up " + piece.name);
    }
    
    public override void dropPiece(ChessPiece piece)
    {
        piece.GetComponent<ChessPiece>().enabled = false;
        int previousLocation = index - 1;
        switch (previousLocation)
        {
            case 0:
                // pawn
                fillInPawns();
                break;
            case 1:
                //rook
                instantiatePiece(board.rookLightPrefab, 7, 0, false);
                Debug.Log("placed rook");
                break;
            case 2:
                // horse
                instantiatePiece(board.knightLightPrefab, 6, 0, false);
                Debug.Log("placed horse");
                break;
            case 3:
                //bishop
                instantiatePiece(board.bishopLightPrefab, 5, 0, false);
                Debug.Log("placed bishop");
                break;
            case 4:
                //queen
                break;
            case 5:
                //king
                break;
        }
        board.clearHighlights();
        if (index < myPrefabs.Count)
        {
            pieceTutorial(myPrefabs[index]);
        }
        else
        {
            // this means they finished the tutorial
            // some sort of congratulations?
            // go back to main menu
            isDone = true;
        }
    }

    public override void extraStart()
    {

        myPrefabs.Add(board.pawnLightPrefab);
        myPrefabs.Add(board.rookLightPrefab);
        myPrefabs.Add(board.knightLightPrefab);
        myPrefabs.Add(board.bishopLightPrefab);
        myPrefabs.Add(board.queenLightPrefab);
        myPrefabs.Add(board.kingLightPrefab);

        mysound = getSource();
        Debug.Log(soundClips.Count);
        mysound.Play();
        soundClips = getAudio();
                mysound.clip = soundClips[0];
        Debug.Log("after sound played");
        board.tutorial = this; //IMPORTANT

        Debug.Log("index = " + index);
        Debug.Log("prefab count " + myPrefabs.Count);
        pieceTutorial(myPrefabs[index]);
    }

    private void fillInPawns()
    {
        for(int i = 1; i < 8; i++)
        {
            instantiatePiece(board.pawnLightPrefab, i, 1, false);
        }
    }

    private void pieceTutorial(GameObject piece)
    {
        instantiatePiece(piece, 4, 3, true);
        switch (index)
        {

            case 0:
                // pawn
                instantiateHighlight(0, 1);
                Debug.Log(soundClips[1].name);
                mysound.clip = soundClips[1];
                mysound.Play();
                break;
            case 1:
                //rook
                instantiateHighlight(0, 0);
                mysound.clip = soundClips[2];
                mysound.Play();
                break;
            case 2:
                // knight
                mysound.clip = soundClips[3];
                mysound.Play();
                instantiateHighlight(1,0);
                break;
            case 3:
                //bishop
                mysound.clip = soundClips[4];
                mysound.Play();
                instantiateHighlight(2, 0);
                break;
            case 4:
                //queen
                mysound.clip = soundClips[5];
                mysound.Play();
                instantiateHighlight(3, 0);
                break;
            case 5:
                //king
                mysound.clip = soundClips[6];
                mysound.Play();
                mysound.PlayOneShot(soundClips[6], 0.8f);
                instantiateHighlight(4, 0);
                break;
        }
        index++;
    }


}
