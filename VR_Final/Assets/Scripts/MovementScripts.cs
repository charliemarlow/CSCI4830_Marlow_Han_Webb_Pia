using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScripts : Tutorial
{

    public List<GameObject> myPrefabs = new List<GameObject>();
    private int index = 0;


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

        board.tutorial = this; //IMPORTANT
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

    public void fillInPawns(){

    }

    private void pieceTutorial(GameObject piece)
    {
        instantiatePiece(piece, 4, 3, true);
        switch (index)
        {

            case 0:
                // pawn
                instantiatePiece(board.pawnDarkPrefab, 5, 4, true);
                break;
            case 1:
                //rook
                instantiatePiece(board.pawnDarkPrefab, 4, 7, true);
                break;
            case 2:
                // knight
                instantiatePiece(board.pawnDarkPrefab, 3, 4, true);
                break;
            case 3:
                //bishop
                instantiatePiece(board.pawnDarkPrefab, 7, 6, true);
                break;
            case 4:
                //queen
                instantiatePiece(board.pawnDarkPrefab, 0, 7, true);
                break;
            case 5:
                //king
                instantiatePiece(board.pawnDarkPrefab, 4, 4, true);
                break;
        }
        index++;
    }


}
