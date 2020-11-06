using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField] InteractionCanvas canvas;

    //radius of the gizmo that will be drawn
    [SerializeField] float radius = 2f;
    //name of the text file that will be loaded when the object is interacted with. For now it is when it is tapped.
    [SerializeField] string filename = "example1";
    //Variable that will store the sentences that will be shown by this object, they must be set in the inspector.
    [SerializeField] DialogueText text = null;

    //buttonPrefabs that may be used by the instance
    [SerializeField] GameObject[] buttons;

    void Start()
    {
        canvas = FindObjectOfType<InteractionCanvas>();
        
    }

    void OnDrawGizmos()
    {
        //visual component to let developers know its an interactible
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);

    }

    void showDialogue(InteractionCanvas intCanvas)
    {
        intCanvas.ShowBox();
    }


    //THESE PUBLIC FUNCTIONS ARE MADE PUBLIC BECAUSE OTHER OBJECTS ARE GOING TO CALL THEM

    //triggers this interactable's interaction. I made ShowDialogue seperate in case we want to make this more general
    //so maybe some interactables will call showDialogue but others will call something else. For now it is still only
    //calling showDialogue tho.
    public void TriggerInteraction()
    {
        Debug.Log("Interacted");
        //if there is an interaction canvas on screen show the dialogue box
        if (canvas != null)
        {
            showDialogue(canvas);
        }
    }

    //getter for the filenName that is associated with the instance
    public string GetFileName()
    {
        return filename;
    }

    //getter for the dialogueTexts
    public DialogueText getDialogue()
    {
        return text;
    }

    //For when we have buttons, like for example. A yes or no button to go to the settigns menu
    public GameObject[] getButtons()
    {
        return buttons;
    }
}
