using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    //public OVRInput.Controller mycontoller;
    PickupObj currentObject = null;
    public float pickupThreshold;
    public float releaseThreshold;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
/* 
    private void OnTriggerStay(Collider other)
    {
        //Rigidbody rb = other.attachedRigidbody;

        if(rb == null)
        {
            return;
        }

        PickupObj p = rb.GetComponent<PickupObj>();

        if(p != null)
        {
            return;
        }

        float triggerValue;
        
        if(mycontoller == OVRInput.Controller.LTouch)
        {
            triggerValue = OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger);
        }
        else
        {
            triggerValue = OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger);
        }
        if (currentObject == null && triggerValue > pickupThreshold)
        {
            currentObject = p;
            rb.isKinematic = true;
            currentObject.transform.parent = this.transform;
        }
        if(currentObject!= null && triggerValue < releaseThreshold)
        {
            currentObject.transform.parent = null;
            rb.isKinematic = false;
            currentObject = null;
        }

        }
        */
    
}
