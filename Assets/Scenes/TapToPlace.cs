using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.Experimental.XR;
using System;
using UnityEngine.XR.ARSubsystems;

public class TapToPlace : MonoBehaviour
{
    
    public GameObject placementIndicator; //Element to visibly see if an AR plane is being tracked
    public GameObject hubWorld; //Element containing the objects to place

    private bool placed = false; //Boolean to track if object has been placesd

    //Variables to track AR plane and see if it is valid
    private ARSessionOrigin arOrigin;
    private Pose placementPose;
    private bool placementPoseIsValid = false;

    /*
     * Finds the AR Session Origin object for AR aspects
     */
    void Start()
    {
        arOrigin = FindObjectOfType<ARSessionOrigin>();
    }

    /*
     * Keeps calling the AR plane tracking methods until one is found and the
     * player touches the screen to place the objects.
     */ 
    void Update()
    {
        if (!placed)
        {
            UpdatePlacementPose();
            UpdatePlacementIndicator();
        }

        if (!placed && placementPoseIsValid && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            PlaceObject();
        }
    }

    /*
     * Instantiates objects and turns off placement indicator since it is no longer needed.
     */
    private void PlaceObject()
    {
        Instantiate(hubWorld, placementPose.position, placementPose.rotation);
        placementIndicator.SetActive(false);
        placed = true; 
    }

    /*
     * If an active plane is being tracked, turns on the placement indicator. Else, turns it off.
     */
    private void UpdatePlacementIndicator()
    {
        if (placementPoseIsValid)
        {
            placementIndicator.SetActive(true);
            placementIndicator.transform.SetPositionAndRotation(placementPose.position, placementPose.rotation);
        }
        else
        {
            placementIndicator.SetActive(false);
        }
    }

    /*
     * Calculates the camera's center point and sends rays from that position to track AR enabled planes. If any hits
     * of these places are found, placement becomes valid and the first plane hit is saved.
     */
    private void UpdatePlacementPose()
    {
        var screenCenter = Camera.main.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        var hits = new List<ARRaycastHit>();
        arOrigin.GetComponent<ARRaycastManager>().Raycast(screenCenter, hits, TrackableType.Planes);

        placementPoseIsValid = hits.Count > 0;
        if (placementPoseIsValid)
        {
            placementPose = hits[0].pose;
        }

    }
}
