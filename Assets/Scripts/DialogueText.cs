using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class for the scriptable object that represents a specific dialogue that can be edited in the inspector
/// </summary>
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/DialogueText", order = 1)]
[System.Serializable]
public class DialogueText : ScriptableObject
{
    /// <summary>
    /// The text field where the dialogue sentences are going to be written.
    /// </summary>
    [TextArea(3, 10)]
    [SerializeField] public string[] sentences;


    /// <summary>
    /// Getter for the sentences array.
    /// </summary>
    /// <returns>sentences: The array of sentences in this instance of TextDialogue</returns>
    public string[] getSentences()
    {
        return sentences;
    }

}
