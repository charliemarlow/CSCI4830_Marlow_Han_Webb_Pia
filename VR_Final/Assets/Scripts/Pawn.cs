using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : ChessPiece
{
    public bool isFirstMove = true;

    public void setFirstMove(bool first)
    {
        isFirstMove = first;
    }

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
            if (board[right, forward] != null &&
                board[right, forward].isLight != selectedPiece.isLight)
            {
                validMoves[right, forward] = true;
            }
        }

        if (isValidSpot(forward) && isValidSpot(left))
        {
            if (board[left, forward] != null && 
                board[left, forward].isLight != selectedPiece.isLight)
            {
                validMoves[left, forward] = true;
            }
        }


        // 2 possible forward moves
        if (isFirstMove && isValidSpot(forwardTwice)
            && board[selectedPiece.currentX, forwardTwice] == null &&
            board[selectedPiece.currentX, forward] == null)
        {
            validMoves[selectedPiece.currentX, forwardTwice] = true;
        }

        if (isValidSpot(forward)
            && board[selectedPiece.currentX, forward] == null)
        {
            validMoves[selectedPiece.currentX, forward] = true;
            Debug.Log("x = " + selectedPiece.currentX);
            Debug.Log("Y = " + forward);
        }

        // is first move is now set in board in select highlight
        /*
        if (isFirstMove && hasValidMoves(validMoves))
        {
            isFirstMove = false;
        }
        */

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
    
    bool hasValidMoves(bool[,] validMoves)
    {
        for (int i = 0; i < 8; i++)
        {
            for(int j = 0; j < 8; j++)
            {
                if (validMoves[i, j])
                {
                    return true;
                }
            }
        }
        return false;
    }

    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        
    }
}
