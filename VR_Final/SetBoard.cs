using System;
using InstantiatePieces;

public class SetBoard
{
	public SetBoard()
	{
	}

    private void setBoardPawn(GameObject pawn)
    {
        instantiatePiece(pawn, 4, 3);
    }

    private void setBoardRook(GameObject rook)
    {
        instantiatePiece(rook, 4, 3);
    }

    private void setBoardKnight(GameObject knight)
    {
        instantiatePiece(knight, 4, 3);
    }

    private void setBoardBishop(GameObject bishop)
    {
        instantiatePiece(bishop, 4, 3);
    }

    private void setBoardQueen(GameObject queen)
    {
        instantiatePiece(queen, 4, 3);
    }

    private void setBoardKing(GameObject king)
    {
        instantiatePiece(king, 4, 3);
    }

    private void setBoardTutorial(GameObject pawn, GameObject rook, GameObject knight, GameObject bishop, GameObject queen, GameObject king)
    {
        setBoardPawn(pawn);


    }

    private void pawnTutorial(GameObject pawn)
    {

    }
}
