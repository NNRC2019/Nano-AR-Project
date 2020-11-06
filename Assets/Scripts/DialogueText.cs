using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/DialogueText", order = 1)]
[System.Serializable]
public class DialogueText : ScriptableObject
{
    [TextArea(3, 10)]
    [SerializeField] public string[] sentences;


    public string[] getSentences()
    {
        return sentences;
    }

}
