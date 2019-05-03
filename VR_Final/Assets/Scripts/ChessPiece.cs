using System.Collections;
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

    public void movePiece(int x, int y)
    {
        transform.localPosition = new Vector3(x, 1, y);
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
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        GameObject boardGo = GameObject.FindWithTag("board");
        board = boardGo.GetComponent<Board>();
        //Debug.Log(board.name + " in start method of chess piece " + this.name);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(holder != null)
        {
            Vector3 desiredPos = holder.localToWorldMatrix.MultiplyPoint(positionHolder);
            Vector3 currentPos = this.transform.position;
            Quaternion desiredRot = holder.rotation * rotationHolder;
            Quaternion currentRot = this.transform.rotation;
            rb.velocity = (desiredPos - currentPos) / Time.fixedDeltaTime;

            Quaternion offsetRot = desiredRot * Quaternion.Inverse(currentRot);
            float angle; Vector3 axis;
            offsetRot.ToAngleAxis(out angle, out axis);
            Vector3 rotationDiff = angle * Mathf.Deg2Rad * axis;
            rb.angularVelocity = rotationDiff / Time.fixedDeltaTime;
        }
    }

    //will cause object to be picked up
    public void pickedUp(Transform t)
    {
        if (holder != null)
        {
            return;
        }
        positionHolder = t.worldToLocalMatrix.MultiplyPoint(this.transform.position);
        rotationHolder = Quaternion.Inverse(t.rotation) * this.transform.rotation;
        // maybe make non kinematic??
        //rb.isKinematic = false;
        
        rb.useGravity = false;
        rb.maxAngularVelocity = Mathf.Infinity;
        holder = t;

    }

    public void released(Transform t, Vector3 vel)
    {
        if(t==holder)
        {
            rb.velocity = vel;
            holder = null;
            rb.isKinematic = true;
            // make kinematic again if you changed it
        }
    }
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
            movePiece(currentX, currentY);
            isPickedUp = false;
            return;
        }
        if(this == null || board == null)
        {
            return;
        }
        if (board.isLightTurn)
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
            board.onDrop(this.castARay());
        }
        else
        {
            this.movePiece(currentX, currentY);
        }
    }
}
