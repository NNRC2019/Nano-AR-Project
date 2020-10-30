using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interizable : MonoBehaviour
{
    //Shows Dialogue on Unity in Test Button
    public Dialogue dialogue;

    public void TriggerDialogue ()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }
}
