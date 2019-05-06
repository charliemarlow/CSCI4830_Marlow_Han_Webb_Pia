using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rook : ChessPiece
{
    public override bool[,] getValidMoves(ChessPiece[,] board, ChessPiece selectedPiece)
    {
        // 4 directions
        // iterate in dir one at a time until you hit
        // write four methods: incrementY, incrementX, decrementY, decrementX
        // then call them logically for each light/dark like the pawn class pattern
        // pass validMoves by references!!!
        bool[,] incXMoves = incrementX(board, selectedPiece);
        bool[,] decXMoves = decrementX(board, selectedPiece);
        bool[,] xMoves = mergeLists(incXMoves, decXMoves);

        bool[,] incYMoves = incrementY(board, selectedPiece);
        bool[,] decYMoves = decrementY(board, selectedPiece);
        bool[,] yMoves = mergeLists(incYMoves, decYMoves);

        bool[,] validMoves = mergeLists(yMoves, xMoves);



        return validMoves;
    } 

    public bool[,] incrementX(ChessPiece[,] board, ChessPiece piece)
    {
        int currX = piece.currentX;
        int currY = piece.currentY;
        bool[,] moves = new bool[8, 8];

        bool hitAPiece = false;
        while (isValidSpot(++currX) && !hitAPiece)
        {
            if(board[currX, currY] != null)
            {
                hitAPiece = true;
                if (board[currX, currY].isLight != piece.isLight)
                {
                    moves[currX, currY] = true;
                }
            }
            else
            {
                moves[currX, currY] = true;
            }
        }
        return moves;
    }

    public bool[,] decrementX(ChessPiece[,] board, ChessPiece piece)
    {
        int currX = piece.currentX;
        int currY = piece.currentY;
        bool[,] moves = new bool[8, 8];

        bool hitAPiece = false;
        while (isValidSpot(--currX) && !hitAPiece)
        {
            if (board[currX, currY] != null)
            {
                hitAPiece = true;
                if (board[currX, currY].isLight != piece.isLight)
                {
                    moves[currX, currY] = true;
                }
            }
            else
            {
                moves[currX, currY] = true;
            }
        }
        return moves;
    }

    public bool[,] incrementY(ChessPiece[,] board, ChessPiece piece)
    {
        int currX = piece.currentX;
        int currY = piece.currentY;
        bool[,] moves = new bool[8, 8];

        bool hitAPiece = false;
        while (isValidSpot(++currY) && !hitAPiece)
        {
            if (board[currX, currY] != null)
            {
                hitAPiece = true;
                if (board[currX, currY].isLight != piece.isLight)
                {
                    moves[currX, currY] = true;
                }
            }
            else
            {
                moves[currX, currY] = true;
            }
        }
        return moves;
    }

    public bool[,] decrementY(ChessPiece[,] board, ChessPiece piece)
    {
        int currX = piece.currentX;
        int currY = piece.currentY;
        bool[,] moves = new bool[8, 8];

        bool hitAPiece = false;
        while (isValidSpot(--currY) && !hitAPiece)
        {
            if (board[currX, currY] != null)
            {
                hitAPiece = true;
                if (board[currX, currY].isLight != piece.isLight)
                {
                    moves[currX, currY] = true;
                }
            }
            else
            {
                moves[currX, currY] = true;
            }
        }
        return moves;
    }




    // Update is called once per frame
    void Update()
    {
        
    }
}
