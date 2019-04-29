using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ChessPiece : MonoBehaviour
{
    public bool isLight;
    public int currentX;
    public int currentY;

    public void movePiece(int x, int y)
    {
        transform.localPosition = new Vector3(x, 0, y);
        currentX = x;
        currentY = y;
    }
     public virtual bool[,] getValidMoves(ChessPiece[,] board, ChessPiece selectedPiece)
    {
        bool[,] fail = new bool[1,1];
        return fail;
    }

    public bool isValidSpot(int index)
    {
        if(index < 8 && index >= 0)
        {
            return true;
        }
        return false;
    }

    public bool[,] mergeLists(bool[,] moves1, bool[,] moves2)
    {
        bool[,] moves = new bool[8, 8];
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                moves[i, j] = moves1[i, j] || moves2[i, j];
            }
        }

        return moves;
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
