using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpeningsTutorial : Tutorial
{
    // Start is called before the first frame update
    void Start()
    {
        
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
}
