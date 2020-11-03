using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class DragItem : MonoBehaviour
{
    [SerializeField] GameObject target;

    private Vector2 touchPosition;
    private bool onTouchHold = false;

    private ARRaycastManager arRaycastManger;
    private Camera arCamera;

    private static List<ARRaycastHit> hits = new List<ARRaycastHit>();

    void Awake()
    {
        arRaycastManger = GetComponent<ARRaycastManager>();
        arCamera = Camera.main;
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            touchPosition = touch.position;

            if (touch.phase == TouchPhase.Began)
            {
                Ray ray = arCamera.ScreenPointToRay(touch.position);
                RaycastHit hitObject;

                if (Physics.Raycast(ray, out hitObject))
                {
                    if (hitObject.transform.name.Contains("Floating"))
                    {
                        onTouchHold = true;
                    }
                }

            }

            if (touch.phase == TouchPhase.Ended)
            {
                onTouchHold = false;
            }
        }


        if(arRaycastManger.Raycast(touchPosition, hits, TrackableType.PlaneWithinBounds))
        {
            Pose hitPose = hits[0].pose;

            if (onTouchHold)
            {
                target.transform.position = hitPose.position;
            }
        }


    }
}
