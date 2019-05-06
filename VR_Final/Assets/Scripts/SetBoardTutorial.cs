using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetBoardTutorial : Tutorial
{

    public (int, int) lastHighlightLocation;



    public override void pickupPiece(ChessPiece piece)
    {
        //Destroy(piece.gameObject);
        Debug.Log("I picked up " + piece.name);
    }
    public override void extraStart()
    {
        instantiateHighlight(0, 1);
        kingTutorial(board.kingLightPrefab);
        board.tutorial = this; //IMPORTANT
    }

    private void rookTutorial(GameObject rook)
    {
        instantiatePiece(rook, 4, 3);
    }

    private void knightTutorial(GameObject knight)
    {
        instantiatePiece(knight, 4, 3);
    }

    private void bishopTutorial(GameObject bishop)
    {
        instantiatePiece(bishop, 4, 3);
    }

    private void queenTutorial(GameObject queen)
    {
        instantiatePiece(queen, 4, 3);
    }

    private void kingTutorial(GameObject king)
    {
        instantiatePiece(king, 4, 3);
    }

    private void setBoardTutorial(GameObject pawn, GameObject rook, GameObject knight, GameObject bishop, GameObject queen, GameObject king)
    {
        //pawnTutorial(pawn);

    }


}
