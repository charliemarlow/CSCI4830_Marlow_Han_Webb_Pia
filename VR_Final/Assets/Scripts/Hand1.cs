using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Hand1 : MonoBehaviour
{

    public Board boardManager;

    ChessPiece currentPiece = null;
    public OVRInput.Controller mycontoller;
    public float pickupThreshold;
    public float releaseThreshold;


    // responsible for calling board funcs 
    // VR pickup
    // on drop 

    // Start is called before the first frame update
    void Start()
    {
    }


    // Update is called once per frame
    void Update()
    {
        
    }


    private void onTriggerStay(Collider c){
        Debug.Log("collider name " + c.name);
        Rigidbody rb = c.attachedRigidbody;
        if(rb == null) return;

        ChessPiece p = rb.GetComponent<ChessPiece>();
        if(p == null) return;

        // get steam vr input
        float triggerValue;

        if (mycontoller == OVRInput.Controller.LTouch)
        {
            triggerValue = OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger);
        }
        else
        {
            triggerValue = OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger);
        }
        

        if (currentPiece == null && triggerValue > pickupThreshold)
        {
            Debug.Log("Picking up");
            pickup(p);

            // pick up the object
        }

        if(currentPiece != null && triggerValue < releaseThreshold)
        {
            // drop the object
            Debug.Log("Dropping");
            drop(p);
        }
    }

    private void pickup(ChessPiece piece){
        Debug.Log("being picked up");
        currentPiece = piece;
        piece.transform.localPosition = this.transform.position;

       // currentPiece.pickedUp(this.transform);
    }

    private void drop(ChessPiece piece){
       // currentPiece.released(this.transform, new Vector3(0,0,0));
        currentPiece = null;
    }
}
 