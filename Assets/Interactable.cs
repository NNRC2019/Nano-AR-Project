using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField] InteractionCanvas canvas;

    //radius of the gizmo that will be drawn
    [SerializeField] float radius = 2f;
    [SerializeField] string filename = "example1";

    //buttonPrefabs
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

   //triggers this interactable's interaction
    public void TriggerInteraction()
    {
        Debug.Log("Interacted");
        //if there is an interaction canvas on screen show the dialogue box
        if (canvas != null)
        {
            showDialogue(canvas);
        }
    }

    //getter for the filenName that is associated with instance
    public string GetFileName()
    {
        return filename;
    }

    //function to show dialogue box
    void showDialogue(InteractionCanvas intCanvas)
    {
        intCanvas.ShowBox();
    }

    public GameObject[] getButtons()
    {
        return buttons;
    }
}
