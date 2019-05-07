using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingsDefense : Tutorial
{
    private int index = 0;

    // Start is called before the first frame update
    public override void extraStart()
    {
        setNonMoveableWhite();
        board.tutorial = this;
        nextMove();
    }

    public override void dropPiece(ChessPiece piece){
        nextMove();
    }


      private void nextMove(){

        switch (index)
        {
            case 0:
                Destroy(board.logicalBoard[4,1].gameObject);
                board.instantiatePiece(board.pawnLightPrefab, 4, 1);
                instantiateHighlight(4,1);
                instantiateHighlight(4,3);
                break;
            case 1:
                Destroy(board.logicalBoard[3,1].gameObject);
                board.instantiatePiece(board.pawnLightPrefab, 3, 1);
                instantiateHighlight(3,1);
                instantiateHighlight(3,3);
                break;
            case 2:
                Destroy(board.logicalBoard[1,0].gameObject);
                board.instantiatePiece(board.knightLightPrefab, 1, 0);
                instantiateHighlight(1,0);
                instantiateHighlight(2,2);
                break;
            case 3:
                Destroy(board.logicalBoard[6,0].gameObject);
                board.instantiatePiece(board.knightLightPrefab, 6, 0);
                instantiateHighlight(6,0);
                instantiateHighlight(5,2);
                break;
            case 4:
                Destroy(board.logicalBoard[5,0].gameObject);
                board.instantiatePiece(board.bishopLightPrefab, 5, 0);
                instantiateHighlight(5,0);
                instantiateHighlight(4,1);
                break;
        }
        index++;
    }
}
