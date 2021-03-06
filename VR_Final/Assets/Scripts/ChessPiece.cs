﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ChessPiece : MonoBehaviour
{
    public bool isLight;
    public int currentX;
    public int currentY;
    
    public Rigidbody rb;
    public Transform holder;
    private Vector3 positionHolder;     //local offset
    private Quaternion rotationHolder;  //local offset
    public Board board;
    public Transform originalParent;
    public Highlight currentHighlight = null;
    public Quaternion originalRot;
    public ControllerInput currentInput;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        GameObject boardGo = GameObject.FindWithTag("board");
        board = boardGo.GetComponent<Board>();
        //Debug.Log(board.name + " in start method of chess piece " + this.name);
        originalParent = transform.parent;
    }
    
    void Update()
    {
        
    }

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

    public bool checkMate(ChessPiece[,] board, ChessPiece piece)
    {

        // check mate == no moves for a specific color
        bool[,] moves = new bool[8, 8];
        bool targetTeam = piece.isLight;
        //go through each spot on the board, calculate moves, merge them
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                if (board[i, j] != null)
                {
                    ChessPiece currentPiece = board[i, j];
                    if (targetTeam == currentPiece.isLight)
                    {
                        bool[,] individualMoves = currentPiece.getValidMoves(board, currentPiece);
                        mergeLists(moves, individualMoves);
                    }
                }
            }
        }

        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                if (moves[i, j])
                {
                    return true;
                }
            }
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

    public Transform castARay(){
        Vector3 localPosition = new Vector3(0,0,0);
        // cast a ray out
        BoxCollider collider = this.GetComponentInChildren<BoxCollider>();
        Vector3 boxPos = collider.transform.position;
        Vector3 direction = collider.transform.TransformDirection(Vector3.forward);
        RaycastHit hit;
        float maxDistance = 100f;
        Debug.Log("casting");
        if(Physics.Raycast(boxPos, direction, out hit, maxDistance)){
            Debug.Log("hit = " + hit.transform.name);
            return hit.transform;
        }else{
            Debug.Log("NULL");
            return null;
        }

        // get the hit

        // return the local position
    }


    // code for getting picked up below
    // Start is called before the first frame update



    private Vector3 mOffset;
    private float mzCoord;
    private void OnMouseDown()
    {
        mzCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        mOffset = gameObject.transform.position - getMouseWorldPos();
    }

    private Vector3 getMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = mzCoord;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    bool isPickedUp;
    private void OnMouseOver()
    {
        if (!isLight && Input.GetMouseButtonDown(0))
        {
            isPickedUp = true;
            return;
        }
        if (Input.GetMouseButtonDown(0) && board.isLightTurn)
        {
            //Debug.Log("I just got clicked");
            board.selectPiece(currentX, currentY);
        }
    }

    private void OnMouseDrag()
    {
        transform.position = getMouseWorldPos() + mOffset;
    }

    public void pickup(ControllerInput input)
    {
        Debug.Log("parent = " + input.gameObject.name);
        transform.SetParent(input.gameObject.transform);

        if (!isLight)
        {
            isPickedUp = true;
            return;
        }
        if (board.isLightTurn || board.getIsTutorial())
        {
            board.selectPiece(currentX, currentY);
        }
        currentInput = input;
        Debug.Log("Picked up " + this.name);
    }

    public void release(ControllerInput input)
    {
        currentInput = input;
        if(transform.parent == input.gameObject.transform)
        {
            if(originalParent != input.gameObject.transform)
            {
                transform.SetParent(originalParent);
                
            }
            else
            {
                transform.SetParent(board.transform);
            }
            this.transform.rotation = originalRot;
            Debug.Log("releasing " + this.name);
            OnMouseUp();
            currentHighlight = null;
        }
    }

    private void OnMouseUp()
    {
        if(isPickedUp && !isLight)
        {
            Pawn p = this.GetComponent<Pawn>();
            if(p != null)
            {
                currentX = p.currentX;
                currentY = p.currentY;
            }
            currentInput.badHaptic();
            movePiece(currentX, currentY);
            isPickedUp = false;
            currentInput = null;
            return;
        }
        if(this == null || board == null)
        {
            return;
        }
        if (board.isLightTurn || board.getIsTutorial())
        {
            //Debug.Log("board " + board.name);
            if(board == null)
            {
                Debug.Log("NULL BOARD");
            }
            if(this == null)
            {
                Debug.Log("NULL this");
            }

            
            if(currentHighlight != null)
            {
                Debug.Log("current highlight =" + currentHighlight.name);
                board.onDrop(currentHighlight.transform);
            }
            else
            {
                Transform t = castARay();
                if(t == null)
                {
                    this.movePiece(currentX, currentY);
                    board.clearHighlights();
                    currentHighlight = null;
                }
                Debug.Log("casting those rays");
                board.onDrop(castARay());
                /*

                */
            }
            //board.onDrop(this.castARay());
        }
        else
        {
            // bad haptic
            Debug.Log("Bad haptics");
            if(!board.debugMode && currentInput != null) currentInput.badHaptic();
            this.movePiece(currentX, currentY);
            board.clearHighlights();
            currentHighlight = null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Highlight high = other.GetComponent<Highlight>();
        if (high == null) return;
        currentHighlight = high;
        Debug.Log("hit that highlight");
    }
}
