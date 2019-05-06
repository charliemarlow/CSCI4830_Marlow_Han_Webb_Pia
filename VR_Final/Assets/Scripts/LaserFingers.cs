using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class LaserFingers : MonoBehaviour
{

    public float maxLaserDistance;

    public GameManager gm;

    public Laser laser;
    public GameObject currentSelection;

    // Start is called before the first frame update
    void Start()
    {
        laser.gameObject.SetActive(true);
    }

    public GameObject selectRaycast()
    {
        Debug.Log("current selection name is " + currentSelection.name);
        Debug.Log("Im printing from " + this.name);
        return currentSelection;
    }

    // Update is called once per frame
    void Update()
    {
        // only use laser fingers when taking a survey
        if (gm.raycastMode)
        {
            laser.gameObject.SetActive(true);
        }
        else
        {
            laser.gameObject.SetActive(false);
            return;
        }

        RaycastHit hit;
        if (Physics.Raycast(new Ray(laser.transform.position, laser.transform.forward), out hit, maxLaserDistance))
        {
            laser.length = hit.distance;    // shortens the laser

            // use index trigger to select an object
            currentSelection = hit.transform.gameObject;

            // we can check here what it actually is
        }
        else
        {
            laser.length = maxLaserDistance;
        }


    }
}


