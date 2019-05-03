using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Queen : ChessPiece
{
    public override bool[,] getValidMoves(ChessPiece[,] board, ChessPiece selectedPiece)
    {
        bool[,] incXMoves = incrementX(board, selectedPiece);
        bool[,] decXMoves = decrementX(board, selectedPiece);
        bool[,] xMoves = mergeLists(incXMoves, decXMoves);

        bool[,] incYMoves = incrementY(board, selectedPiece);
        bool[,] decYMoves = decrementY(board, selectedPiece);
        bool[,] yMoves = mergeLists(incYMoves, decYMoves);

        bool[,] validRookMoves = mergeLists(yMoves, xMoves);

        bool[,] incxy = incXincY(board, selectedPiece);
        bool[,] incxDecy = incXdecY(board, selectedPiece);
        bool[,] moves1 = mergeLists(incxy, incxDecy);

        bool[,] decxy = decXdecY(board, selectedPiece);
        bool[,] decxIncy = decXincY(board, selectedPiece);
        bool[,] moves2 = mergeLists(decxy, decxIncy);

        bool[,] validBishopMoves = mergeLists(moves1, moves2);

        bool[,] validQueenMoves = mergeLists(validRookMoves, validBishopMoves);
        return validQueenMoves;
    }


    public bool[,] incXincY(ChessPiece[,] board, ChessPiece piece)
    {
        int currX = piece.currentX;
        int currY = piece.currentY;
        bool[,] moves = new bool[8, 8];

        bool hitAPiece = false;
        while (isValidSpot(++currX) && isValidSpot(++currY) && !hitAPiece)
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

    public bool[,] incXdecY(ChessPiece[,] board, ChessPiece piece)
    {
        int currX = piece.currentX;
        int currY = piece.currentY;
        bool[,] moves = new bool[8, 8];

        bool hitAPiece = false;
        while (isValidSpot(++currX) && isValidSpot(--currY)
 && !hitAPiece)
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

    public bool[,] decXdecY(ChessPiece[,] board, ChessPiece piece)
    {
        int currX = piece.currentX;
        int currY = piece.currentY;
        bool[,] moves = new bool[8, 8];

        bool hitAPiece = false;
        while (isValidSpot(--currX) && isValidSpot(--currY) && !hitAPiece)
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

    public bool[,] decXincY(ChessPiece[,] board, ChessPiece piece)
    {
        int currX = piece.currentX;
        int currY = piece.currentY;
        bool[,] moves = new bool[8, 8];

        bool hitAPiece = false;
        while (isValidSpot(--currX) && isValidSpot(++currY) && !hitAPiece)
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


    public bool[,] incrementX(ChessPiece[,] board, ChessPiece piece)
    {
        int currX = piece.currentX;
        int currY = piece.currentY;
        bool[,] moves = new bool[8, 8];

        bool hitAPiece = false;
        while (isValidSpot(++currX) && !hitAPiece)
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
