using System.Collections;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

/// <summary>
/// The player script for now is attached to the AR Session Origin as it is from where the player will see the scene.
/// This script will shoot raycasts from the position where the user tapped the screen.
/// </summary>
public class Player : MonoBehaviour
{
    /// <summary>
    /// The current target will be the object that was tapped that is not part of the UI.
    /// </summary>
    [SerializeField] private Object currentTarget;

    /// <summary>
    /// Cached instance of our arCamera in the scene.
    /// </summary>
    private Camera arCamera;


    /***UNUSED FIELDS***/
    // Variable to hold the position on the screen where the tap occured.
    //private Vector2 touchPosition;
    //variable that is true if a tap on the screen is being held continuously
    //private bool onTouchHold = false; //unused for now
    //cached instance of the ARRaycast manager attached to this object
    //private ARRaycastManager arRaycastManger; //unused for now, it is necessary for detecting touches on AR planes!
    //list that will store the AR planes touched by the raycast
    //private static List<ARRaycastHit> hits = new List<ARRaycastHit>(); //unused for now, also necessary for detecting touches in AR planes

    /// <summary>
    /// In the start method we fetch our cached instances from the scene.
    /// </summary>
    void Start()
    {
        //arRaycastManger = GetComponent<ARRaycastManager>();
        arCamera = Camera.main;
    }

    /// <summary>
    /// We run the game with click controls if we are in the Unity editor for testing purposes. Otherwise, we run it with
    /// touch controls.
    /// </summary>
    void Update()
    {
        #if !UNITY_EDITOR
        TouchUpdate();
        #else
        ClickUpdate();
        #endif
        

    }

    /// <summary>
    /// Temporary coroutine to resize hit object for half a second in order to signify that it has been interacted with.
    /// We should remove it when we dont need it anymore.
    /// </summary>
    /// <param name="objectTr">The transform component of the target object.</param>
    /// <returns></returns>
    IEnumerator ScaleMe(Transform objectTr)
    {
        objectTr.localScale *= 1.2f;
        yield return new WaitForSeconds(0.5f);
        objectTr.localScale /= 1.2f;
    }

    /// <summary>
    /// Gets the position where user touched the screen and raycasts from this position.
    /// If an interactable object was hit, then its interaction is triggered.
    /// </summary>
    private void TouchUpdate()
    {
        //checks if there has been atleast 1 touch
        if (Input.touchCount > 0)
        {
            //stores the first touch
            Touch touch = Input.GetTouch(0);


            //variable that will store the object hit by the raycast
            RaycastHit hit;

            //ray that will be shot out
            Ray ray = Camera.main.ScreenPointToRay(touch.position);

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

    /// <summary>
    /// Gets the position where user clicked the screen and raycasts from this position.
    /// If an interactable object was hit, then its interaction is triggered.
    /// </summary>
    private void ClickUpdate()
    {
        //checks if there has been atleast 1 touch
        if (Input.GetMouseButtonDown(0))
        {

            //variable that will store the object hit by the raycast
            RaycastHit hit;

            //ray that will be shot out
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

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
}
