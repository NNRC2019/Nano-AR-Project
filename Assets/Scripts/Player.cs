using System.Collections;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

//The player script for now is attached to the AR Session Origin as it is from where the player will see the scene
public class Player : MonoBehaviour
{
    //The current target will be the object that was tapped that is not part of the UI
    [SerializeField] Object currentTarget;

    //variable to hold the position on the screen where the tap occured
    private Vector2 touchPosition;
    //variable that is true if a tap on the screen is being held continuously
    //private bool onTouchHold = false; //unused for now

    //cached instance of our arCamera in the scene
    private Camera arCamera;

    //cached instance of the ARRaycast manager attached to this object
    //private ARRaycastManager arRaycastManger; //unused for now, it is necessary for detecting touches on AR planes!
    //list that will store the AR planes touched by the raycast
    //private static List<ARRaycastHit> hits = new List<ARRaycastHit>(); //unused for now, also necessary for detecting touches in AR planes

    void Start()
    {
        //arRaycastManger = GetComponent<ARRaycastManager>();
        arCamera = Camera.main;
    }

    void Update()
    {

        //checks if there has been atleast 1 touch
        if (Input.touchCount > 0) //if (Input.GetMouseButtonDown(0))  I leave this here so we can quickly switch to clicking for tests
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
                //Check if the object that was clicked/tapped on is interactable by putting it in a variable and see if its null
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

    //temporary coroutine to resize hit object in order to signify that it has been interacted with, we should remove it when we dont need it anymore
    IEnumerator ScaleMe(Transform objTr)
    {
        objTr.localScale *= 1.2f;
        yield return new WaitForSeconds(0.5f);
        objTr.localScale /= 1.2f;
    }
}
