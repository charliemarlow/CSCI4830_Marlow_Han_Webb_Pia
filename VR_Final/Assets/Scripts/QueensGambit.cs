using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpeningsTutorial : Tutorial
{

    private int index = 0;
    // Start is called before the first frame update
    public override void extraStart()
    {
        InstantiateWhiteOnly();
        board.tutorial = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void InstantiateWhiteOnly(){
        int lightSide = 0;

        // set up pawns
        for (int i = 0; i < 8; i++)
        {
            board.instantiatePiece(board.pawnLightPrefab, i, lightSide + 1);
        }

        // set up light pieces
        board.setUpRooks(board.rookLightPrefab, lightSide);
        board.setUpKnights(board.knightLightPrefab, lightSide);
        board.setUpBishops(board.bishopLightPrefab, lightSide);
        board.setUpRoyals(board.kingLightPrefab, board.queenLightPrefab, lightSide);
    }

    private void nextMove(){

        switch (index)
        {

            case 0:
                // pawn
                
                break;
            case 1:
                
               
                break;
            case 2:
                
                break;
            case 3:
              
                break;
         
        }
        index++;
    }
}
