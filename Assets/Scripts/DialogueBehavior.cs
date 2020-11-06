using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueBehavior : MonoBehaviour
{
    //cached component
    TextMeshProUGUI tmpro;

    //cached objects
    InteractionCanvas canvas;
    DialogueManager dialogueManager;

    //Dialoguebox fields
    //holds object that will tell the dialogue what to say
    [SerializeField] Interactable owner;
    //[SerializeField] string filename = "example1";
    [SerializeField] float txtSpeed = 0.1f;
    TextAsset txt;
    //arraylist that holds all dialogue buttons that are active
    ArrayList buttons = new ArrayList();

    //array to store sentences
    //[SerializeField] string[] sentences;
    private int currIndex;


    // Start is called before the first frame update
    void Start()
    {
        //get canvas with the script interactionCanvas
        canvas = FindObjectOfType<InteractionCanvas>();
        //get object with the script DialogueManager
        dialogueManager = FindObjectOfType<DialogueManager>();
        //get this object's text mesh pro component
        tmpro = GetComponent<TextMeshProUGUI>();

        //load text file from the resources folder. Has to be from the resources folder.
        txt = Resources.Load("TextFiles/" + owner.GetFileName()) as TextAsset;

        //get all sentences by splitting the text wherever a new line space is found
        //this will end up with one empty sentence at the end will have to deal with that
        //sentences = owner.getDialogue();//txt.text.Split('\n'); <---Previous way to do it

        DialogueText d = Resources.Load("TextFiles/" + owner.GetFileName()) as DialogueText;

        //start index at second position because we will show the first one on instantiation
        currIndex = 1;
        //on creation show first sentence
        if (dialogueManager != null)  dialogueManager.StartDialogue(d, canvas, tmpro, txtSpeed);
       // StartCoroutine(ShowText(sentences[0]));
    }

    /*
    //Coroutine to show next char sequentially
    IEnumerator ShowText(string text)
    {
        //first it is empty
        tmpro.text = "";
        // At the moment we dont need this chunk of code but we will when we decide to incorporate buttons at the end of dialogue
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
        
        foreach (char c in text.ToCharArray())
        {
            //show next char
            tmpro.text += c;
            //wait txtSpeed amount of seconds before showing next char
            yield return new WaitForSeconds(txtSpeed);
        }
    }
    */

    
    //method the continue button will call whenever it is pressed
    public void showNextSentence()
    {

        if(dialogueManager!= null) dialogueManager.DisplayNextSentence(canvas, tmpro, txtSpeed);
        /*
        StopAllCoroutines();
        int size = sentences.Length;

        //Destroy box if the last sentence was shown
        if (currIndex >= size) //last sentence will be empty which will be where we show the options
        {
            canvas.DestroyBox();
        }
        //otherwise show next sentence in the sentences array
        else
        {
            StartCoroutine(ShowText(sentences[currIndex]));
            currIndex++;
        }
        */
    }
    

    //for the buttons
    public void ShowAnswerOptions()
    {

        foreach (GameObject go in owner.getButtons())
        {
            //instantiates the button and adds it to the arrayList;
            buttons.Add(Instantiate(go, transform));
        }
    }

    //for the buttons
    public void DestroyAllDialogueButtons()
    {
        //destroy every single dialogue button in the arrayList
        foreach (GameObject go in buttons)
        {
            Destroy(go);
        }
        //reset arraylist of buttons
        buttons = new ArrayList();
    }

}
