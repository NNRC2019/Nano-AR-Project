using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class that desginates a gameObject as an interactable with a specific interaction for each instance.
/// </summary>
public class Interactable : MonoBehaviour
{
    /// <summary>
    /// Cached instance of the UI canvas that will show the dialogue textboxes.
    /// </summary>
    [SerializeField] InteractionCanvas canvas;

    /// <summary>
    /// Radius of the gizmo that will be drawn on the interactable.
    /// </summary>
    [SerializeField] float radius = 2f;

    /// <summary>
    /// Name of the text file that will be loaded when the object is interacted with. 
    /// For now an object has been interacted with if it was tapped.
    /// </summary>
    [SerializeField] string filename = "ExampleText1";


    /// <summary>
    /// buttonPrefabs that may be used by the instance to include options in their dialogue.
    /// </summary>
    [SerializeField] GameObject[] buttons;


    /// <summary>
    /// In the start method we fetch our cached instances from the scene.
    /// </summary>
    void Start()
    {
        canvas = FindObjectOfType<InteractionCanvas>();
        
    }

    /// <summary>
    /// Draw visual component on the object to let developers know its an interactible.
    /// </summary>
    void OnDrawGizmos()
    {
        
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);

    }


    /// <summary>
    ///Make the interaction canvas instantiate the dialogue box.
    /// </summary>
    /// <param name="intCanvas">The cached instance of the interaction canvas in the scene</param>
    void showDialogue(InteractionCanvas intCanvas)
    {
        intCanvas.ShowBox();
    }


    //THESE PUBLIC FUNCTIONS ARE MADE PUBLIC BECAUSE OTHER OBJECTS ARE GOING TO CALL THEM

    /// <summary>
    ///Triggers this interactable's interaction. I made ShowDialogue seperate in case we want to make this more general
    ///so maybe some interactables will call showDialogue but others will call something else. For now it is still only
    ///calling showDialogue.
    /// </summary>
    public void TriggerInteraction()
    {
        Debug.Log("Interacted");
        //if there is an interaction canvas on screen show the dialogue box
        if (canvas != null)
        {
            showDialogue(canvas);
        }
    }

    /// <summary>
    /// Getter for the filenName that is associated with the instance
    /// </summary>
    /// <returns>The name of the TextDialogue file associated with this instance</returns>
    public string GetFileName()
    {
        return filename;
    }

    /// <summary>
    /// For when we have buttons, like for example. A yes or no button to go to the settings menu
    /// </summary>
    /// <returns>The array of buttons associated to this instance</returns>
    public GameObject[] getButtons()
    {
        return buttons;
    }
}
