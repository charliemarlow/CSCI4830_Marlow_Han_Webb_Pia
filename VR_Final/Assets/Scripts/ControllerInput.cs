using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class ControllerInput : MonoBehaviour
{

    public GameManager gm;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public SteamVR_Action_Boolean grabPinch; //Grab Pinch is the trigger, select from inspecter
    public SteamVR_Input_Sources inputSource = SteamVR_Input_Sources.Any;//which controller
                                                                         // Use this for initialization
    private bool isGrabbed;
    public ChessPiece currentPiece;
    private Collider recentCollision = null;
  
// Update is called once per frame
void Update()
    {
        bool wasTrue = isGrabbed;
        //Debug.Log(SteamVR_Actions._default.GrabPinch.G);

        if (gm.raycastMode)
        {
            if (SteamVR_Actions._default.GrabPinch.GetStateDown(SteamVR_Input_Sources.Any))
            {
                gm.raycastSelect();
            }
            return;
        }

        if (SteamVR_Actions._default.GrabPinch.GetStateDown(SteamVR_Input_Sources.Any) && !isGrabbed)
        {
            Debug.Log("Down");
            isGrabbed = true;
            if(recentCollision != null)
            {
                OnStay(recentCollision);
            }
        }
        if(SteamVR_Actions._default.GrabPinch.GetStateUp(SteamVR_Input_Sources.Any) && isGrabbed)
        {
            Debug.Log("Up");
            isGrabbed = false;
        }
        Debug.Log("isGrabbed = "+ isGrabbed);

        if (wasTrue && !isGrabbed && currentPiece != null)
        {
            Debug.Log("Releasing piece " + currentPiece.name);
            currentPiece.release(this);
            currentPiece = null;
        }
    }

    public bool getIsGrabbed()
    {
        return isGrabbed;
    }


    private void OnTriggerEnter(Collider other)
    {
        recentCollision = other;
    }
    private void OnTriggerExit(Collider other)
    {
        recentCollision = null;
    }

    private void OnStay(Collider other)
    {
        Debug.Log("Trigger = " + other.gameObject.name);
        ChessPiece piece = other.GetComponent<ChessPiece>();
        if (piece == null) return;
        if (currentPiece != null) return;

        currentPiece = piece;
        Debug.Log("Grabbing piece " + piece.name);

       if (isGrabbed || SteamVR_Actions._default.GrabPinch.GetStateDown(SteamVR_Input_Sources.Any))
        {
            Debug.Log("Picking up");
            piece.pickup(this);
        }
    }
}
