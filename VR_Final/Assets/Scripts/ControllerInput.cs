using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class ControllerInput : MonoBehaviour
{
    //public SteamVR_Action_Vibration hapticSignal;
    public GameManager gm;
    public SteamVR_Behaviour_Pose controller;
    public SteamVR_Input_Sources source;
    public bool isLeft;
    public bool menuUp;
    public GameObject currentMenu;
    public GameObject menuPrefab;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<SteamVR_Behaviour_Pose>();
        Debug.Log(controller.name);
        if (isLeft)
        {
            source = SteamVR_Input_Sources.LeftHand;
        }
        else
        {
            source = SteamVR_Input_Sources.RightHand;
        }

    }

    public SteamVR_Action_Boolean grabPinch; //Grab Pinch is the trigger, select from inspecter
    public SteamVR_Input_Sources inputSource = SteamVR_Input_Sources.Any;//which controller
                                                                         // Use this for initialization
    private bool isGrabbed;
    public ChessPiece currentPiece;
    private Collider recentCollision = null;
  

    public void pickupHaptic()
    {
        //SteamVR_Action_Vibration.Execute(float secondsFromNow, float durationSeconds, float frequency, float amplitude, SteamVR_Input_Sources inputSource)
        controller.hapticSignal.Execute(0f, .05f, 100, 0.5f, inputSource);
    }

    public void putDownHaptic()
    {
        controller.hapticSignal.Execute(0f, .08f, 100, 0.5f, inputSource);
    }

    public void goodHaptic()
    {
        pickupHaptic();
    }

    public void badHaptic()
    {
        //SteamVR_Action_Vibration.Execute(float secondsFromNow, float durationSeconds, float frequency, float amplitude, SteamVR_Input_Sources inputSource)
        controller.hapticSignal.Execute(0f, .8f, 200, 1f, inputSource);
    }

    bool isSelecting = false;
    int counter = 0;
// Update is called once per frame
    void FixedUpdate()
    {
        bool wasTrue = isGrabbed;
        //Debug.Log(SteamVR_Actions._default.GrabPinch.G);


        if (SteamVR_Actions._default.GrabPinch.GetStateDown(source) && !isGrabbed)
        {
            isGrabbed = true;
            Debug.Log("Is grabbed");
            //putDownHaptic();
            if(recentCollision != null)
            {
                OnStay(recentCollision);
            }
            /* 
            if (gm.raycastMode && !isSelecting)
            {
                isSelecting = true;
                gm.raycastSelect(this);
            }
            */
        }
        if(SteamVR_Actions._default.GrabPinch.GetStateUp(source) && isGrabbed)
        {
            Debug.Log("Up");
            isGrabbed = false;
            isSelecting = false;
        }

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

    private void onCollisionEnter(Collider other){
             Debug.Log("Entering");
        if(other.CompareTag("button")){
            Debug.Log("HERE");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
                    Debug.Log("Entering");
        if(other.CompareTag("button")){
            Debug.Log("HERE");
            if(menuUp){
                Destroy(currentMenu);
                menuUp = false;
                currentMenu = null;
            }else{
                currentMenu = Instantiate(menuPrefab);
                menuUp = true;
            }
        }
        recentCollision = other;
        Debug.Log("collided with " + other.name);
    }
    private void OnTriggerExit(Collider other)
    {
        Debug.Log("exiting with " + other.name);
        recentCollision = null;
    }

    public void startMenu(){
        menuUp = true;
        if(currentMenu != null) return;
        currentMenu = Instantiate(menuPrefab);
    }

    public void killMenu(){
        menuUp = false;
        if(currentMenu == null) return;
        Destroy(currentMenu);
        currentMenu = null;
    }

    private void OnStay(Collider other)
    {
        Debug.Log("Trigger = " + other.gameObject.name);

        ChessPiece piece = other.GetComponent<ChessPiece>();
        if (piece == null) return;
        if (currentPiece != null || !piece.isActiveAndEnabled) return;

        currentPiece = piece;
        Debug.Log("Grabbing piece " + piece.name);

       if (isGrabbed || SteamVR_Actions._default.GrabPinch.GetStateDown(SteamVR_Input_Sources.Any))
        {
            Debug.Log("Picking up");
            piece.pickup(this);
        }
    }
}
