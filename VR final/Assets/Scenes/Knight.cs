using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : ChessPiece
{
    public override bool[,] getValidMoves(ChessPiece[,] board, ChessPiece selectedPiece)
    {
        bool[,] validMoves = new bool[8, 8];
        List<(int, int)> options = new List<(int, int)>();
        
        // eight possible options
        // down2Right
        (int, int) down2Right = getDown2Right(selectedPiece);
        options.Add(down2Right);
        // down2Left
        (int, int) down2Left = getDown2Left(selectedPiece);
        options.Add(down2Left);
        // up2Right
        (int, int) up2Right = getUp2Right(selectedPiece);
        options.Add(up2Right);
        // up2Left
        (int, int) up2Left = getUp2Left(selectedPiece);
        options.Add(up2Left);
        // right2Up
        (int, int) right2Up = getRight2Up(selectedPiece);
        options.Add(right2Up);
        // right2Down
        (int, int) right2Down = getRight2Down(selectedPiece);
        options.Add(right2Down);
        // left2Up
        (int, int) left2Up = getLeft2Up(selectedPiece);
        options.Add(left2Up);
        // left2Down
        (int, int) left2Down = getLeft2Down(selectedPiece);
        options.Add(left2Down);

        foreach((int, int) opt in options)
        {
            if (isValidSpot(opt.Item1) && isValidSpot(opt.Item2))
            {
                Debug.Log("Valid move at (x,y) = " + opt.Item1 + " " + opt.Item2);
                validMoves[opt.Item1, opt.Item2] = true;
            }
        }
        return validMoves;
    }

    (int, int) getDown2Right(ChessPiece piece)
    {
        int x = 0;
        int y = 0;
        if (piece.isLight)
        {
            // light
            // down 2 is subtract on y
            y = piece.currentY - 2;
            // right is sub 1 on x
            x = piece.currentX - 1;
        }
        else
        {
            // down 2 is add on y
            y = piece.currentY + 2;
            // right is add 1 on x
            x = piece.currentX + 1;

        }
        return (x, y);
    }
    (int, int) getDown2Left(ChessPiece piece)
    {
        int x = 0;
        int y = 0;
        if (piece.isLight)
        {
            // down 2 is subtract on y
            y = piece.currentY - 2;
            x = piece.currentX + 1;
        }
        else
        {
            // down 2 is add on y
            y = piece.currentY + 2;
            x = piece.currentX - 1;
        }
        return (x, y);
    }
    (int, int) getUp2Right(ChessPiece piece)
    {
        int x = 0;
        int y = 0;
        if (piece.isLight)
        {
            // light implementation
            // add 2 to y axis
            y = piece.currentY + 2;
            // right on x axis, sub 1
            x = piece.currentX - 1;
        }
        else
        {
            Debug.Log("My cases");
            //dark implementation
            // up on y axis, for dark subtract 2
            y = piece.currentY - 2;
            Debug.Log("y = " + y);
            // right on x axis, for dark add 1
            x = piece.currentX + 1;
            Debug.Log("X = " + x);
        }
        return (x,y);
    }
    (int, int) getUp2Left(ChessPiece piece)
    {
        int x = 0;
        int y = 0;
        if (piece.isLight)
        {
            // add 2 to y axis
            y = piece.currentY + 2;
            x = piece.currentX + 1;
        }
        else
        {
            // up on y axis, for dark subtract 2
            y = piece.currentY - 2;
            x = piece.currentX - 1;
            Debug.Log("X = " + x + "Y =" + y);
        }
        return (x, y);
    }
    (int, int) getRight2Up(ChessPiece piece)
    {
        int x = 0;
        int y = 0;
        if (piece.isLight)
        {
            // right 2 for light is sub 2 on x
            x = piece.currentX - 2;
            // up for light is add on y
            y = piece.currentY + 1;

        }
        else
        {
            x = piece.currentX + 2;
            y = piece.currentY - 1;
        }
        return (x, y);
    }
    (int, int) getRight2Down(ChessPiece piece)
    {
        int x = 0;
        int y = 0;
        if (piece.isLight)
        {
            // right 2 on light is x sub 2
            x = piece.currentX - 2;
            y = piece.currentY - 1;
        }
        else
        {
            x = piece.currentX + 2;
            y = piece.currentY + 1;
        }
        return (x, y);
    }
    (int, int) getLeft2Up(ChessPiece piece)
    {
        int x = 0;
        int y = 0;
        if (piece.isLight)
        {
            // left 2 is add 2 on x
            x = piece.currentX + 2;
            y = piece.currentY + 1;
        }
        else
        {
            x = piece.currentX - 2;
            y = piece.currentY - 1;
        }
        return (x, y);
    }
    (int, int) getLeft2Down(ChessPiece piece)
    {
        int x = 0;
        int y = 0;
        if (piece.isLight)
        {
            // left 2 is add 2 on x
            // ERROR area
            x = piece.currentX + 2;
            y = piece.currentY - 1;
        }
        else
        {
            x = piece.currentX - 2;
            y = piece.currentY + 1;
        }
        return (x, y);
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
