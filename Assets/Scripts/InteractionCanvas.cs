using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Canvas that shows the dialogue box for an interactable
/// </summary>
public class InteractionCanvas : MonoBehaviour
{

    /// <summary>
    /// TextBox that it will instantiate
    /// </summary>
    [SerializeField] Object uiElemPrefab;

    /// <summary>
    /// x position where we want the instantiated element to appear.
    /// </summary>
    [SerializeField] float elemXPos = 15.4f;
    /// <summary>
    /// y position where we want the instantiated element to appear.
    /// </summary>
    [SerializeField] float elemtYpos = 78.2f;

    /// <summary>
    /// Holds the current instance of the dialogue box
    /// </summary>
    private Object currentDialogueBox;

    /// <summary>
    /// Function to instantiate the dialogue box if one is not already created.
    /// </summary>
    public void ShowBox()
    {
        //We store the instantiated object in a global variable for accesibility
        //We ensure that there is only ever one currentDialogue box with the following if statement
        if(currentDialogueBox == null)
        {
            currentDialogueBox = Instantiate(uiElemPrefab, transform); //Will want to change this up so that it instantiates exactly where I want it to
        }
        
    }

    /// <summary>
    /// Function to destroy the dialogue box.
    /// </summary>
    public void DestroyBox()
    {
        Destroy(currentDialogueBox);
        currentDialogueBox = null;
    }

}
