﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{

    public GameObject graphicChessBoard;
    public GameObject highlightPrefab;
    public GameManager manager;
    public ChessOpponent opponent;
    public bool isLightTurn;
    private float selectedTileX = -1;
    private float selectedTileY = -1;

    private const int boardDimension = 8;
    private ChessPiece selectedPiece = null;
    private GameObject highlight = null;
    private bool isHighlighted = false;
    private bool pieceIsInHand = false;
    public Camera gameCam;

    public GameObject pawnDarkPrefab;
    public GameObject pawnLightPrefab;
    public GameObject rookDarkPrefab;
    public GameObject rookLightPrefab;
    public GameObject knightDarkPrefab;
    public GameObject knightLightPrefab;
    public GameObject bishopDarkPrefab;
    public GameObject bishopLightPrefab;
    public GameObject kingDarkPrefab;
    public GameObject kingLightPrefab;
    public GameObject queenDarkPrefab;
    public GameObject queenLightPrefab;


    private ChessPiece[,] logicalBoard = new ChessPiece[boardDimension, boardDimension];
    private GameObject[,] highlights = new GameObject[boardDimension, boardDimension];

    private float startTime;
    private float endTime;
    bool firstMove = true;
    // set levels 0 to N
    public enum difficulty { EASY, CHALLENGE };
    public int currentDifficulty = 1;

    private void instantiatePiece(GameObject prefab, int newX, int newZ)
    {

        Vector3 position = new Vector3(newX, 0, newZ);
        Quaternion rot = this.transform.rotation;
        GameObject piece = Instantiate(prefab, position, rot,this.transform);
        //piece.transform.localPosition = position;

        ChessPiece p = piece.gameObject.GetComponent<ChessPiece>();
        p.movePiece(newX, newZ);
        p.isLight = setColor(newZ);
        
        // set proper rotation for dark pieces
        if (!p.isLight)
        {
            Vector3 rotation = p.transform.rotation.eulerAngles;
            rotation = new Vector3(rotation.x, rotation.y + 180, rotation.z);
            p.transform.rotation = Quaternion.Euler(rotation);
            //p.transform.localRotation = new Vector3(90, 0, 0);
        }
        logicalBoard[newX, newZ] = p;

    }

    bool setColor(int z)
    {
        return z < 2 ? true : false;
    }

    private void setUpRooks(GameObject prefab, int side)
    {
        instantiatePiece(prefab, 0, side);
        instantiatePiece(prefab, 7, side);
    }

    private void setUpKnights(GameObject prefab, int side)
    {
        instantiatePiece(prefab, 1, side);
        instantiatePiece(prefab, 6, side);
    }

    private void setUpBishops(GameObject prefab, int side)
    {
        instantiatePiece(prefab, 2, side);
        instantiatePiece(prefab, 5, side);
    }

    private void setUpRoyals(GameObject king, GameObject queen, int side)
    {
        instantiatePiece(queen, 3, side);
        instantiatePiece(king, 4, side);
    }

    private void instantiatePieces()
    {
        int lightSide = 0;
        int darkSide = 7;

        // set up pawns
        for (int i = 0; i < boardDimension; i++)
        {
            instantiatePiece(pawnLightPrefab, i, lightSide + 1);
            instantiatePiece(pawnDarkPrefab, darkSide - i, darkSide - 1);
        }

        // set up light pieces
        setUpRooks(rookLightPrefab, lightSide);
        setUpKnights(knightLightPrefab, lightSide);
        setUpBishops(bishopLightPrefab, lightSide);
        setUpRoyals(kingLightPrefab, queenLightPrefab, lightSide);

        // set up dark pieces
        setUpRooks(rookDarkPrefab, darkSide);
        setUpKnights(knightDarkPrefab, darkSide);
        setUpBishops(bishopDarkPrefab, darkSide);
        setUpRoyals(kingDarkPrefab, queenDarkPrefab, darkSide);
    }

    // Start is called before the first frame update
    void Start()
    {
        instantiatePieces();
        isLightTurn = true;
        gameCam = GameObject.FindObjectOfType<Camera>();
    }

    public void selectPiece(int x, int y)
    {
        if (firstMove)
        {
            startTime = Time.time;
            firstMove = false;
        }
        if (logicalBoard[x,y] == null) return;
        
        // select new piece
        selectedPiece = logicalBoard[x,y];
        //Debug.Log("Just selected " + selectedPiece.name);
        if(selectedPiece.isLight != isLightTurn)
        {
            selectedPiece.movePiece(selectedPiece.currentX, selectedPiece.currentY);
            selectedPiece = null;
            return;
        }
        //Debug.Log("Selected " + selectedPiece.gameObject.name);
        //ebug.Log("Selected x " + selectedPiece.currentX + " Selected y = " + selectedPiece.currentY);
        
        // now we want to get all possible moves
        bool[,] possible = selectedPiece.getValidMoves(logicalBoard, selectedPiece);

        // then we want to paint those moves on the board with the highlight prefab
        paintHighlights(possible);
        pieceIsInHand = true;
    }


    // Flow:
    // pick it up with the hand, hand calls to boardmanager to alert it 
    // board manager makes necessary changes and shows the options
    // then the raycast from the bottom of the piece is activate on drop
    public void vrSelect(ChessPiece piece){
        int currX = piece.currentX;
        int currY = piece.currentY;

        selectedPiece = logicalBoard[currX, currY];
        if(selectedPiece.isLight != isLightTurn){
            selectedPiece = null;
            // drop it from the hand, move it back
            piece.movePiece(currX, currY);

            return;
        }

        // now we want to get all possible moves
        bool[,] possible = selectedPiece.getValidMoves(logicalBoard, selectedPiece);

        // then we want to paint those moves on the board with the highlight prefab
        paintHighlights(possible);
        pieceIsInHand = true;

    }

    private void selectHighlight(int x, int y)
    {
        // don't move to null loc
        Debug.Log("selected piece = " + selectedPiece.name);
        if (highlights[x,y] == null)
        {
            Debug.Log("null return");
            selectedPiece.movePiece(selectedPiece.currentX, selectedPiece.currentY);
            clearHighlights();
            isLightTurn = !isLightTurn;
            return;
        }
        


        // move the piece to the new location
        int oldX = selectedPiece.currentX;
        int oldY = selectedPiece.currentY;
        logicalBoard[oldX, oldY] = null;

        // destroy old piece
        // trick: destroy the gameobject, not the chess piece
        if(logicalBoard[x,y] != null)
        {
            if (logicalBoard[x, y].CompareTag("king"))
            {
                Debug.Log("Game Over");
                gameOver(0);
                return;
            }
            if (logicalBoard[x, y].isLight)
            {
                manager.makeHappy();
                //haptics here
            }
            else
            {
                manager.makeSad();
                // haptics here
            }
            Destroy(logicalBoard[x, y].gameObject);
        }

        // set the piece in new loc
        logicalBoard[x, y] = selectedPiece;
        selectedPiece.movePiece(x, y);
        //haptics here

        // set pawn's first move to false
        Pawn checkForFirst = selectedPiece.GetComponent<Pawn>();
        if(checkForFirst != null)
        {
            Debug.Log("checking for move");
            checkForFirst.setFirstMove(false);

            // check for pawn promotion
            //if(checkForFirst.currentX)
            if (checkForFirst.isLight)
            {
                if(selectedPiece.currentY == 7)
                {
                    Debug.Log("it is 7");
                    int currentX = selectedPiece.currentX;
                    int currentY = selectedPiece.currentY;
                    string type = manager.pawnPromote();
                    promotePawn(type, true);


                    Destroy(selectedPiece.gameObject);
                    selectedPiece = logicalBoard[x, y];
                    selectedPiece.isLight = true;
                    logicalBoard[currentX, currentY] = selectedPiece;
                    Debug.Log("promoted " + logicalBoard[x, y]);
                }
            }
            else
            {
                /*
                if(selectedPiece.currentY == 0)
                {
                    string type = manager.pawnPromote();
                    promotePawn(type, false);
                }
                */
            }
        }
        selectedPiece = null;

        // clear highlights
        clearHighlights();
        isHighlighted = false;
        isLightTurn = !isLightTurn;
        pieceIsInHand = false;

    }

    private void promotePawn(string type, bool isLight)
    {
        switch (type) {
            case "queen":
                if (isLight)
                {
                    instantiatePiece(queenLightPrefab, selectedPiece.currentX, selectedPiece.currentY);
                }
                else
                {
                    instantiatePiece(queenDarkPrefab, selectedPiece.currentX, selectedPiece.currentY);
                }
                break;
            case "king":
                if (isLight)
                {
                    instantiatePiece(kingLightPrefab, selectedPiece.currentX, selectedPiece.currentY);
                }
                else
                {
                    instantiatePiece(kingDarkPrefab, selectedPiece.currentX, selectedPiece.currentY);
                }
                break;
            case "pawn":
                if (isLight)
                {
                    instantiatePiece(pawnLightPrefab, selectedPiece.currentX, selectedPiece.currentY);
                }
                else
                {
                    instantiatePiece(pawnDarkPrefab, selectedPiece.currentX, selectedPiece.currentY);
                }
                break;
            case "knight":
                if (isLight)
                {
                    instantiatePiece(knightLightPrefab, selectedPiece.currentX, selectedPiece.currentY);
                }
                else
                {
                    instantiatePiece(knightDarkPrefab, selectedPiece.currentX, selectedPiece.currentY);
                }
                break;
            case "bishop":
                if (isLight)
                {
                    instantiatePiece(bishopLightPrefab, selectedPiece.currentX, selectedPiece.currentY);
                }
                else
                {
                    instantiatePiece(bishopDarkPrefab, selectedPiece.currentX, selectedPiece.currentY);
                }
                break;
            case "rook":
                if (isLight)
                {
                    instantiatePiece(rookLightPrefab, selectedPiece.currentX, selectedPiece.currentY);
                }
                else
                {
                    instantiatePiece(rookDarkPrefab, selectedPiece.currentX, selectedPiece.currentY);
                }
                break;

        }
    }

    private void gameOver(int win)
    {
        // win: 0 is user, 1 is AI, 3 is stalemate
        endTime = Time.time;
        for(int i =0; i < boardDimension; i++)
        {
            for(int j =0; j< boardDimension; j++)
            {
                if(logicalBoard[i,j] != null)
                {
                    Destroy(logicalBoard[i, j].gameObject);
                    logicalBoard[i, j] = null;
                    
                }
            }
        }
        Destroy(selectedPiece.gameObject);
        clearHighlights();

        if (win == 0)
            manager.finishedGame(1, endTime - startTime);
        else if (win == 1)
            manager.finishedGame(0, endTime - startTime);
        else if (win == 3)
            manager.finishedGame(.5f, endTime - startTime);


        instantiatePieces();
        firstMove = true;
    }
    private void paintHighlights(bool[,] possible)
    {
        for (int i = 0; i < 8; i++)
        {

            for (int j = 0; j < 8; j++)
            {
                if (possible[i,j])
                {
                    isHighlighted = true;
                    Vector3 position = new Vector3(i, 0, j);
                    Quaternion rot = this.transform.rotation;
                    GameObject piece = Instantiate(highlightPrefab, position, rot, this.transform);
                    piece.transform.localPosition = position;
                    highlights[i, j] = piece;

                }

            }
        }
    }
    private void clearHighlights()
    {
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                if (highlights[i,j] != null)
                {
                    Destroy(highlights[i, j]);
                    highlights[i, j] = null;

                }

            }
        }
    }
    void printLogicalBoard()
    {
        for(int i =0; i < 8; i++)
        {
            for(int j =0; j <8; j++)
            {
                if(logicalBoard[i,j] != null)
                {
                    Debug.Log("Location (x,y) =" + i + ", " + j  + " name : " + logicalBoard[i, j].name);
                }

            }
        }
    }

    float getSelectedTileNumber(float tileNum){
        float selectedTile;
         if(tileNum > .5)
        {
            selectedTile = Mathf.Ceil(tileNum);
        }
        else
        {
            selectedTile = 0;
        }

         if(selectedTile >= 8)
        {
            selectedTile = 7;
        }
        return selectedTile;
    }
    public bool onMouseClick(){
        bool validPiece = false;
         RaycastHit hit;
        if (Physics.Raycast(gameCam.ScreenPointToRay(Input.mousePosition), out hit, 10))
        {
            if (hit.transform.gameObject.GetComponent<ChessPiece>() != null ||
                hit.transform.gameObject.CompareTag("highlight"))
            {
                Vector3 localPos = hit.transform.localPosition;
                float tempX = hit.transform.localPosition.x;
                float tempY = hit.transform.localPosition.z;

                selectedTileX = getSelectedTileNumber(tempX);
                selectedTileY = getSelectedTileNumber(tempY);

                validPiece = true;
            }
            else
            {
                validPiece = false;
            }
        }
        selectPiece((int)selectedTileX, (int)selectedTileY);
        return validPiece;
    }

// valid piece is set to true 
// if it's valid, should move it there
    public bool onDrop(Transform highlight){
        bool validPiece = false;

        // get a raycast from bottom of chess piece
      
        if(highlight == null){
            Debug.Log("Null hit");
            // set the piece back to its position
            selectedPiece.movePiece(selectedPiece.currentX, selectedPiece.currentY);
            clearHighlights();
            // more to force a drop
            selectedPiece = null;
            return false;
        }

        Vector3 localPos = highlight.localPosition;
        // check that it is equal to a chess piece OR a highlight

        // get local pos, get selected tile x and selected tile y
        float tempX = localPos.x;
        float tempY = localPos.z;
        
        selectedTileX = getSelectedTileNumber(tempX);
        selectedTileY = getSelectedTileNumber(tempY);
        Debug.Log(selectedTileX);
        Debug.Log(selectedTileY);
        validPiece = true;

        // set valid piece to true, or set it to false
        selectHighlight((int)selectedTileX, (int)selectedTileY);

        return validPiece;
    }

    private void detectStalemate()
    {
        if(selectedPiece != null)
        {
            ChessPiece piece = selectedPiece;
            if(piece.checkMate(logicalBoard, piece))
            {
                Debug.Log("Game over");
                gameOver(3);
            }
        }

    }

    private int count = 0;
    bool inUse = false; // non atomic mutex lock

    // Update is called once per frame
    void Update()
    {
        if (!isLightTurn && !inUse)
        {
            inUse = true;
            manager.makeThink();
            (ChessPiece, int, int) myTuple = opponent.makeMove(logicalBoard);
            ChessPiece movePiece = myTuple.Item1;
            int x = myTuple.Item2;
            int y = myTuple.Item3;
            manager.moveAvatarHand(movePiece, x, y);

            logicalBoard[movePiece.currentX, movePiece.currentY] = null;
            if (logicalBoard[x, y] != null)
            {
                if (logicalBoard[x, y].CompareTag("king"))
                {
                    Debug.Log("Game Over");
                    selectedPiece = movePiece;
                    gameOver(2);
                    return;
                }
                if (logicalBoard[x, y].isLight)
                {
                    manager.makeHappy();
                    //haptics here
                }
                else
                {
                    manager.makeSad();
                }
                Destroy(logicalBoard[x, y].gameObject);
            }
            logicalBoard[x, y] = movePiece;
            movePiece.movePiece(x, y);
            isLightTurn = true;
            inUse = false;
        }
        /*
        bool validPiece = onMouseClick();


        
        if (Input.GetMouseButtonDown(0))
        {
            if (selectedPiece == null && !isHighlighted && validPiece)
            {
                Debug.Log("Selecting piece");
                selectPiece((int)selectedTileX, (int)selectedTileY);
                detectStalemate();
            }/*
            else if (isHighlighted)
            {
                Debug.Log("selecting highlight");
                selectHighlight((int)selectedTileX, (int)selectedTileY);
            }//
            else
            {
                Debug.Log("Unselecting");
                selectedPiece = null;
            }
        }*/
        
            
        }
}
