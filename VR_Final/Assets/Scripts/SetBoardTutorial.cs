using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetBoardTutorial : Tutorial
{

    public (int, int) lastHighlightLocation;

    public override void extraStart()
    {
        instantiateHighlight(0, 1);
    }

    private void rookTutorial(GameObject rook)
    {
        board.instantiatePiece(rook, 4, 3);
    }

    private void knightTutorial(GameObject knight)
    {
        board.instantiatePiece(knight, 4, 3);
    }

    private void bishopTutorial(GameObject bishop)
    {
        board.instantiatePiece(bishop, 4, 3);
    }

    private void queenTutorial(GameObject queen)
    {
        board.instantiatePiece(queen, 4, 3);
    }

    private void kingTutorial(GameObject king)
    {
        board.instantiatePiece(king, 4, 3);
    }

    private void setBoardTutorial(GameObject pawn, GameObject rook, GameObject knight, GameObject bishop, GameObject queen, GameObject king)
    {
        //pawnTutorial(pawn);

    }


}
