using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Makes class available to show
[System.Serializable] 
public class Dialogue 
{

    //NPC name
    public string name;

    //Expands text lines on Unity
    [TextArea(3, 10)]

    //Sentences to put in the Queue
    public string[] sentences;
}
