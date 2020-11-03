using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class Player : MonoBehaviour
{
    [SerializeField] Object currentTarget;

    private Vector2 touchPosition;
    //private bool onTouchHold = false;

    private ARRaycastManager arRaycastManger;
    private Camera arCamera;
    private static List<ARRaycastHit> hits = new List<ARRaycastHit>();

    void Start()
    {
        arRaycastManger = GetComponent<ARRaycastManager>();
        arCamera = Camera.main;
    }

    void Update()
    {
        //we might have to do something with touch phase

        //checks if there has been atleast 1 touch
        if (Input.touchCount > 0) //if (Input.GetMouseButtonDown(0))
        {
           //stores the first touch
            Touch touch = Input.GetTouch(0);

           
            //variable that will store the object hit by the raycast
            RaycastHit hit;

            //ray that will be shot out
            Ray ray = Camera.main.ScreenPointToRay(touch.position); //Camera.main.ScreenPointToRay(Input.mousePosition);

            //if the ray hit an object Interact
            if (Physics.Raycast(ray, out hit))
            {
                //Check if the object clicked on is interactable by putting it in a variable and see if its null
                Interactable interactable = hit.collider.GetComponent<Interactable>();
                if (interactable != null)
                {
                    //We set the interactable to be our current target
                    currentTarget = interactable;
                    //We start the temp coroutine that temporarily increases target size
                    StartCoroutine(ScaleMe(hit.transform));
                    // Message to show which object was clicked on
                    Debug.Log("You selected the " + hit.transform.name);

                    //if there is a canvas on screen show the dialogue box
                    interactable.TriggerInteraction();
                }
            }
        }
    }

    //temporary coroutine to resize hit object in order to signify that it has been interacted with
    IEnumerator ScaleMe(Transform objTr)
    {
        objTr.localScale *= 1.2f;
        yield return new WaitForSeconds(0.5f);
        objTr.localScale /= 1.2f;
    }
}
