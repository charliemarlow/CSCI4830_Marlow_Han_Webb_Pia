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

    public Vector3 castARay(){
        Vector3 localPosition = new Vector3(0,0,0);
        // cast a ray out
        BoxCollider collider = this.GetComponentInChildren<BoxCollider>();
        Vector3 boxPos = collider.transform.position;
        Vector3 direction = collider.transform.TransformDirection(Vector3.forward);
        RaycastHit hit;
        float maxDistance = 100f;

        if(Physics.Raycast(boxPos, direction, out hit, maxDistance)){
            Debug.Log("hit = " + hit.transform.name);
            return hit.transform.localPosition;
        }else{
            Debug.Log("NULL");
            return localPosition;
        }

        // get the hit

        // return the local position
    }


    // code for getting picked up below
    // Start is called before the first frame update
    void Start()
    {
         rb = GetComponent<Rigidbody>();
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
        rb.isKinematic = false;
        
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
}
