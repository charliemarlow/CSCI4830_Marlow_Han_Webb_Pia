using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScripts : Tutorial
{
    private List<AudioClip> soundClips = getAudio();
    AudioSource mysound;
    public List<GameObject> myPrefabs = new List<GameObject>();
    private int index = 0;
    private (int, int) victimLoc;

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void extraStart(){
        myPrefabs.Add(board.pawnLightPrefab);
        myPrefabs.Add(board.rookLightPrefab);
        myPrefabs.Add(board.knightLightPrefab);
        myPrefabs.Add(board.bishopLightPrefab);
        myPrefabs.Add(board.queenLightPrefab);
        myPrefabs.Add(board.kingLightPrefab);


        pieceTutorial(myPrefabs[index]);

        mysound = GetComponent<AudioSource>();
        mysound.PlayOneShot(soundClips[0], 0.8f);
        board.tutorial = this; //IMPORTANT
        board.isMoveTutorial = true;
    }



public override void dropPiece(ChessPiece piece)
    {
        (int, int) pieceLoc = (piece.currentX, piece.currentY);
        piece.GetComponent<ChessPiece>().enabled = false;
        int previousLocation = index - 1;

        if(pieceLoc.Item1 != victimLoc.Item1 || pieceLoc.Item2 != victimLoc.Item2)
        {
            board.logicalBoard[piece.currentX, piece.currentY] = null;
            Destroy(piece.gameObject);
        }

        board.clearHighlights();

        if (index < myPrefabs.Count)
        {
            pieceTutorial(myPrefabs[index]);
        }
        
        else if(index == myPrefabs.Count + 1)
        {
            Debug.Log("exiting");
            // this means they finished the tutorial
            // some sort of congratulations?
            // go back to main menu
            board.isMoveTutorial =false;
            board.isTutorial = false;
            board.tutorial = null;
            isDone = true;
        }
    }

    public void fillInPawns(){

    }

    private void pieceTutorial(GameObject piece)
    {
        ChessPiece p = instantiatePiece(piece, 4, 3, true);


        switch (index)
        {

            case 0:
                // pawn
                victimLoc = (5,4);
                instantiateDarkPiece(board.pawnDarkPrefab, 5, 4, true);
                mysound.PlayOneShot(soundClips[1], 0.8f);
                break;
            case 1:
                //rook
                victimLoc = (4,7);
                instantiateDarkPiece(board.pawnDarkPrefab, 4, 7, true);
                mysound.PlayOneShot(soundClips[1], 0.8f);
                break;
            case 2:
                // knight
                victimLoc = (2,4);
                instantiateDarkPiece(board.pawnDarkPrefab, 2, 4, true);
                mysound.PlayOneShot(soundClips[2], 0.8f);
                break;
            case 3:
                //bishop
                victimLoc = (1,0);
                instantiateDarkPiece(board.pawnDarkPrefab, 1, 0, true);
                mysound.PlayOneShot(soundClips[3], 0.8f);
                break;
            case 4:
                //queen
                victimLoc = (0,7);
                instantiateDarkPiece(board.pawnDarkPrefab, 0, 7, true);
                mysound.PlayOneShot(soundClips[4], 0.8f);
                break;
            case 5:
                //king
                victimLoc = (4,4);
                instantiateDarkPiece(board.pawnDarkPrefab, 4, 4, true);
                mysound.PlayOneShot(soundClips[5], 0.8f);
                break;
        }
        index++;

    }


}
