using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetBoardTutorial : Tutorial
{

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


        pieceTutorial(myPrefabs[index]);

        board.tutorial = this; //IMPORTANT
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
                break;
            case 1:
                //rook
                instantiateHighlight(0, 0);
                break;
            case 2:
                // knight
                instantiateHighlight(1,0);
                break;
            case 3:
                //bishop
                instantiateHighlight(2, 0);
                break;
            case 4:
                //queen
                instantiateHighlight(3, 0);
                break;
            case 5:
                //king
                instantiateHighlight(4, 0);
                break;
        }
        index++;
    }


}
