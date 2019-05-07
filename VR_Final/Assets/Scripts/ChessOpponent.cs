using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessOpponent : MonoBehaviour
{
    public Board chessBoard;
    private ChessPiece[,] logicalBoard;
    bool team = false;

    // Start is called before the first frame update
    void Start()
    {
        GameObject boardGo = GameObject.FindWithTag("board");
        chessBoard = boardGo.GetComponent<Board>();
        
    }

    public (ChessPiece, int, int) makeMove(ChessPiece[,] board)
    {
        logicalBoard = board;

        List<(ChessPiece, int x, int y)> validMoves = getMoves();
        if (chessBoard.currentDifficulty == 0)
        {
            return easy(board, validMoves);
        }
        else if (chessBoard.currentDifficulty == 1)
        {
            //Debug.Log("medium");
            return medium(board, validMoves);
        }
        return validMoves[0];
    }

    public (ChessPiece, int, int) easy(ChessPiece[,] board, List<(ChessPiece, int x, int y)> validMoves)
    {
        logicalBoard = board;
        Random rand = new Random();
        int r = (int)Random.Range(0f, (float)validMoves.Count);
        (ChessPiece, int, int) selection = validMoves[r];
        return selection;
    }

    public (ChessPiece, int, int) medium(ChessPiece[,] board, List<(ChessPiece, int x, int y)> validMoves)
    {
        logicalBoard = board;
        Random rand = new Random();
        int r = (int)Random.Range(0f, (float)validMoves.Count);
        (ChessPiece, int, int) selection = validMoves[r];

        int maxValue = 0;

        for (int i = 0; i < validMoves.Count; i++)
        {
            int x = validMoves[i].Item2;
            int y = validMoves[i].Item3;
            if (logicalBoard[x, y] != null)
            {
                int value = getValue(logicalBoard[x, y]);
                if (value > maxValue)
                {
                    maxValue = value;
                    selection = validMoves[i];
                }
            }
        }

        return selection;
    }

    public (ChessPiece, int, int) hard(ChessPiece[,] board, List<(ChessPiece, int x, int y)> validMoves)
    {
        (ChessPiece, int, int) selectedPiece;
        int maxValue = 0;
        for (int i = 0; i < validMoves.Count; i++)
        {
            int x = validMoves[i].Item2;
            int y = validMoves[i].Item3;
            if (logicalBoard[x, y] != null)
            {
                int value = getValue(logicalBoard[x, y]);
                if (value > maxValue)
                {
                    maxValue = value;
                    selection = validMoves[i];
                }
            }
        }
        return validMoves[0];
    }
    private int minimax(int depth, bool isMax, ChessPiece[,] board)
    {
        // if depth is 0, get the max move

        // get all possible moves

        // if is max
            // set best move to 0
            // for each possible move
            // recurse w/ depth -1, !ismax
            // take the maximum move of best and recursion
        // else
            // set best to 9999
            //  for each move
            // choose best between


        return 0;
    }

    public List<(ChessPiece, int x, int y)> getMoves()
    {
        List<(ChessPiece, int x, int y)> validMoves = new List<(ChessPiece, int x, int y)>();
        for(int i = 0; i < 8; i++)
        {
            for(int j = 0; j < 8; j++)
            {
                ChessPiece currentPiece = logicalBoard[i, j];
                if (logicalBoard[i,j] != null && currentPiece.isLight == team)
                {
                    bool[,] pieceMoves = currentPiece.getValidMoves(logicalBoard, currentPiece);
                    //Debug.Log("Piece = " + currentPiece.name);
                    for (int x = 0; x < 8; x++)
                    {
                        for (int y = 0; y < 8; y++)
                        {
                            //Debug.Log(pieceMoves[x, y]);
                            if (pieceMoves[x, y])
                            {
                                validMoves.Add((currentPiece, x, y));
                                //Debug.Log("added " + currentPiece.name);
                            }
                        }
                    }
                }
            }
        }

        return validMoves;
    }

    
    private int getValue(ChessPiece piece)
    {
        Pawn pawn = piece.GetComponent<Pawn>();
        Queen queen = piece.GetComponent<Queen>();
        King king = piece.GetComponent<King>();
        Rook rook = piece.GetComponent<Rook>();
        Bishop bishop = piece.GetComponent<Bishop>();
        Knight knight = piece.GetComponent<Knight>();

        if(pawn != null)
        {
            return 10;
        }

        if(queen != null)
        {
            return 90;
        }

        if(king != null)
        {
            return 900;
        }
        if(rook != null)
        {
            return 50;
        }
        if(bishop != null)
        {
            return 30;
        }
        if(knight != null)
        {
            return 30;
        }

        return 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
