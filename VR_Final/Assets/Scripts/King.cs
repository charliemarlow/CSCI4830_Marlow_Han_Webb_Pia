using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class King : ChessPiece
{
    public override bool[,] getValidMoves(ChessPiece[,] board, ChessPiece selectedPiece)
    {
        int x = selectedPiece.currentX;
        int y = selectedPiece.currentY;
        List<(int, int)> validMoveLoc = new List<(int, int)>();
        bool[,] validMoves = new bool[8, 8];

        validMoveLoc.Add((x, y - 1));
        validMoveLoc.Add((x - 1, y - 1));
        validMoveLoc.Add((x + 1, y - 1));

        validMoveLoc.Add((x - 1, y));
        validMoveLoc.Add((x + 1, y));

        validMoveLoc.Add((x - 1, y + 1));
        validMoveLoc.Add((x, y + 1));
        validMoveLoc.Add((x + 1, y + 1));

        /*
        ChessPiece[,] potentialMoves = copyArray(board);
        King newPiece = new King();
        newPiece.isLight = selectedPiece.isLight;
        foreach((int, int) opt in validMoveLoc)
        {
            if (isValidSpot(opt.Item1) && isValidSpot(opt.Item2))
            {
                potentialMoves[opt.Item1, opt.Item2] = newPiece;
            }
        }

        bool[,] attackMoves = getAttackMoves(potentialMoves, selectedPiece);
        */

        foreach ((int, int) opt in validMoveLoc)
        {
            if (isValidSpot(opt.Item1) && isValidSpot(opt.Item2))
            {
                // valid if empty or if it has an enemy in it
                if (board[opt.Item1, opt.Item2] == null ||
                    board[opt.Item1, opt.Item2].isLight != selectedPiece.isLight)
                {
                    validMoves[opt.Item1, opt.Item2] = true;
                }
            }
        }


        return validMoves;
    }

    bool[,] getAttackMoves(ChessPiece[,] board, ChessPiece piece){
        bool team = piece.isLight;
        bool[,] rollingMovesList = new bool[8, 8];

        // for each piece on the board
        for(int i = 0; i < 8; i++)
        {
            for(int j = 0; j < 8; j++)
            {
                // each piece that is on opposite team
                if(board[i,j] != null && board[i,j].isLight != team &&
                    !board[i,j].gameObject.CompareTag("king"))
                {
                    // get their valid moves
                    bool[,] currMoves = board[i, j].getValidMoves(board, piece);
                    // merge with other moves
                    rollingMovesList = mergeLists(rollingMovesList, currMoves);
                }
            }
        }

        // return list of every possible move by other opponent
        return rollingMovesList;
    }

    ChessPiece[,] copyArray(ChessPiece[,] arr)
    {
        ChessPiece[,] newArr = new ChessPiece[8, 8];
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                newArr[i, j] = arr[i, j];
            }
        }
        return newArr;
    }

    /* Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    */
}
