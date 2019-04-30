using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand1 : MonoBehaviour
{

    public Board boardManager;

    ChessPiece currentPiece = null;


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

/* 
    private void onTriggerStay(Collider c){
        //Rigidbody rb = c.attachedRigidbody;
        if(rb == null) return;

        ChessPiece p = rb.GetComponent<ChessPiece>();
        if(p == null) return;

        // get steam vr input

        if(currentPiece == null){
            // pick up the object
        }

        if(currentPiece != null){
            // drop the object
        }
    }
*/
    private void pickup(ChessPiece piece){
        currentPiece = piece;
        currentPiece.pickedUp(this.transform);
    }

    private void drop(ChessPiece piece){
        currentPiece.released(this.transform, new Vector3(0,0,0));
        currentPiece = null;
    }
}
 