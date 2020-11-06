using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{

    //Sentences in sentences out
    private Queue<string> sentences;

    // Start is called before the first frame update
    void Start()
    {
        //Initialize Queue
        sentences = new Queue<string>();

    }

    //public method to Start the dialogue
    public void StartDialogue (DialogueText d, InteractionCanvas canvas, TextMeshProUGUI tmpro, float txtSpeed)
    {
        sentences.Clear();
        if(d == null)
        {
            Debug.Log("Not Working");
        }
        else
        {
            foreach (string sentence in d.sentences)
            {
                sentences.Enqueue(sentence);
            }
            DisplayNextSentence(canvas, tmpro, txtSpeed);
        }
        
    }

    
    public void DisplayNextSentence(InteractionCanvas canvas, TextMeshProUGUI tmpro, float txtSpeed)
    {
        StopAllCoroutines();
        int size = sentences.Count;

        //Destroy box if the last sentence was shown
        if (QueueIsEmpty()) //last sentence will be empty which will be where we show the options
        {
            canvas.DestroyBox();
        }
        //otherwise show next sentence in the sentences array
        else
        {
            StartCoroutine(TypeSentence(sentences.Dequeue(), tmpro, txtSpeed));
        }
    }

    public bool QueueIsEmpty()
    {
        return sentences.Count == 0;
    }

    //Coroutine to show next char sequentially
    IEnumerator TypeSentence (string text, TextMeshProUGUI tmpro, float txtSpeed)
    {
        //first it is empty
        tmpro.text = "";
        /* At the moment we dont need this chunk of code but we will when we decide to incorporate buttons at the end of dialogue
        if(text.Length == 0)
        {
            ShowAnswerOptions();
        }
        
        else
        {
            foreach (char c in text.ToCharArray())
            {
                //show next char
                tmpro.text += c;
                //wait txtSpeed amount of seconds before showing next char
                yield return new WaitForSeconds(txtSpeed);
            }
        }
        */
        foreach (char c in text.ToCharArray())
        {
            //show next char
            tmpro.text += c;
            //wait txtSpeed amount of seconds before showing next char
            yield return new WaitForSeconds(txtSpeed);
        }
    }
/*
    void EndDialogue()
    {
        Debug.Log("End of conversation.");
    }  
*/
}
