using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Makes class available to show
[System.Serializable] 
public class Dialogue 
{

    //NPC name
    //[SerializeField] string name;

    //Expands text lines on Unity
    [TextArea(3, 10)]
    //Sentences to put in the Queue. They must be written in the inspector
    [SerializeField] string[] sentences;

    public string[] getSentences()
    {
        return sentences;
    }

    /*
    public string getName()
    {
        return name;
    }
    */
}
