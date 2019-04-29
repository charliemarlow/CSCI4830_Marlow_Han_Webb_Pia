using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : ChessPiece
{
    bool isFirstMove = true;

    public override bool[,] getValidMoves(ChessPiece[,] board, ChessPiece selectedPiece)
    {
        bool[,] validMoves = new bool[8, 8];
        int currentX = selectedPiece.currentX;
        int currentY = selectedPiece.currentY;
  
        // get possible move points
        int forward = getCorrectForward(selectedPiece);
        int forwardTwice = getCorrectForwardTwice(selectedPiece);
        int right = getCorrectRight(selectedPiece);
        int left = getCorrectLeft(selectedPiece);

        // 2 attacks
        if (isValidSpot(forward) && isValidSpot(right))
        {
            if (board[right, forward] != null)
            {
                validMoves[right, forward] = true;
            }
        }

        if (isValidSpot(forward) && isValidSpot(left))
        {
            if (board[left, forward] != null)
            {
                validMoves[left, forward] = true;
            }
        }


        // 2 possible forward moves
        if (isFirstMove && isValidSpot(forwardTwice)
            && board[selectedPiece.currentX, forwardTwice] == null)
        {
            validMoves[selectedPiece.currentX, forwardTwice] = true;
        }

        if (isValidSpot(forward)
            && board[selectedPiece.currentX, forward] == null)
        {
            validMoves[selectedPiece.currentX, forward] = true;
        }
        isFirstMove = false;


        return validMoves;
    }

    int getCorrectForward(ChessPiece selectedPiece)
    {
        int forward;
        if (selectedPiece.isLight)
        {
            // y+1
            forward = selectedPiece.currentY + 1;
        }
        else
        {
            //y -1
            forward = selectedPiece.currentY - 1;
        }
        return forward;
    }

    int getCorrectForwardTwice(ChessPiece selectedPiece)
    {
        int forwardTwice;
        if (selectedPiece.isLight)
        {
            // y+ 2 is forward
            forwardTwice = selectedPiece.currentY + 2;
        }
        else
        {
            // y -2
            forwardTwice = selectedPiece.currentY - 2;
        }
        return forwardTwice;
    }

    int getCorrectLeft(ChessPiece selectedPiece)
    {
        int left;
        if (selectedPiece.isLight)
        {
            // x + 1
            left = selectedPiece.currentX + 1;
        }
        else
        {
            // x -1
            left = selectedPiece.currentX - 1;
        }
        return left;
    }

    int getCorrectRight(ChessPiece selectedPiece)
    {
        int right;
        if (selectedPiece.isLight)
        {
            // x -1
            right = selectedPiece.currentX - 1;
        }
        else
        {
            // x + 1
            right = selectedPiece.currentX + 1;
        }
        return right;
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
