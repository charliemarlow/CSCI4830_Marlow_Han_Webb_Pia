using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    
public class LaserFingers : MonoBehaviour
{
    public bool isLeft;
    public bool surveyTime;

    public float maxLaserDistance;

    public Board gm;

    public Laser laser;

    private Renderer lastColored;
    private bool isLookingAtHighlights = false;

    // Start is called before the first frame update
    void Start()
    {
        surveyTime = false;
        lastColored = null;
        laser.gameObject.SetActive(true);
    }


    float getIndexTriggerState()
    {
        float indexTriggerState = 0.0f;
        if (isLeft)
        {
            indexTriggerState = OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, OVRInput.Controller.LTouch);
        }
        else
        {
            indexTriggerState = OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, OVRInput.Controller.RTouch);
        }
        return indexTriggerState;
    }


    // Update is called once per frame
    void Update()
    {
        // only use laser fingers when taking a survey
  
            RaycastHit hit;
            if (Physics.Raycast(new Ray(laser.transform.position, laser.transform.forward), out hit, maxLaserDistance))
            {
                laser.length = hit.distance;    // shortens the laser

                // use index trigger to select an object
                if (getIndexTriggerState() >= .5)
                {
                   // if its a chess piece, send its transform to the board manager
                   if(hit.collider.gameObject.GetComponent<ChessPiece>() != null && !isLookingAtHighlights)
                {
                    Debug.Log("Hit a chess piecde");
                    gm.vrSelect(hit.collider.gameObject.GetComponent<ChessPiece>());
                    isLookingAtHighlights = true;
                }
                else if(hit.collider.transform.CompareTag("highlight") && isLookingAtHighlights)
                {
                    Debug.Log("Hit a highlight");
                    gm.onDrop(hit.collider.transform);
                    isLookingAtHighlights = false;
                }

                    // if its a highlight, send its infor to board manager
                }
            }
            else
            {
                laser.length = maxLaserDistance;
            }
    }
}


