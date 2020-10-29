using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionCanvas : MonoBehaviour
{

    //element that it will instantiate
    [SerializeField] Object uiElemPrefab;

    //x and y position where we want the instantiated element to appear
    [SerializeField] float elemXPos = 15.4f;
    [SerializeField] float elemtYpos = 78.2f;

    //holds the current instance of the dialogue box
    private Object currentDialogueBox;

    //Function that will show the dialogue box when called
    public void ShowBox()
    {
        //We store the instantiated object in a global variable for accesibility
        //We ensure that there is only ever one currentDialogue box with the following if statement
        if(currentDialogueBox == null)
        {
            currentDialogueBox = Instantiate(uiElemPrefab, transform); //Will want to change this up so that it instantiates exactly where I want it to
        }
        
    }

    //Function that will destroy the dialogue box when called
    public void DestroyBox()
    {
        Destroy(currentDialogueBox);
        currentDialogueBox = null;
    }

}
