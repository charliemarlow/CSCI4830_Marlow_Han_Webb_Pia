using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bishop : ChessPiece 
{
    public override bool[,] getValidMoves(ChessPiece[,] board, ChessPiece selectedPiece)
    {
        return new bool[8, 8];
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
