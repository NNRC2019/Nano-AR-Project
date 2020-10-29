using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    //cached component
    TextMeshProUGUI tmpro;

    //cached object
    InteractionCanvas canvas;

    //Dialoguebox fields
        //holds object that will tell the dialogue what to say
    [SerializeField] Interactable owner;
    //[SerializeField] string filename = "example1";
    [SerializeField] float txtSpeed = 0.1f;
    TextAsset txt;
    //arraylist that holds all dialogue buttons that are active
    ArrayList buttons = new ArrayList();

    //array to store sentences
    [SerializeField] string[] sentences;
    private int currIndex;


    // Start is called before the first frame update
    void Start()
    {
        //get canvas with the script interactionCanvas
        canvas = FindObjectOfType<InteractionCanvas>();
        //get this object's text mesh pro component
        tmpro = GetComponent<TextMeshProUGUI>();

        //load text file from the resources folder. Has to be from the resources folder
        txt = Resources.Load("TextFiles/" + owner.GetFileName()) as TextAsset;

        //get all sentences by splitting the text wherever a period is found
        //this will end up with one empty sentences at the end will have to deal with that
        sentences = txt.text.Split('\n'); //WE WILL HAVE TO BE CAREFUL WITH ABREVIATIONS OR FIND A WAY TO DEAL WITH THEM

        //start index at second position because we will show the first one on instantiation
        currIndex = 1;
        //on creation show first sentence
        StartCoroutine(ShowText(sentences[0]));
    }

    //Coroutine to show next char sequentially
    IEnumerator ShowText(string text)
    {
        //first it is empty
        tmpro.text = "";
        /*
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

    //method the button will call whenever it is pressed
    public void showNextSentence()
    {
        StopAllCoroutines();
        int size = sentences.Length;

        //Destroy box if the last sentence was shown
        if (currIndex +1 >= size) //last sentence will be empty which will be where we show the options
        {
            canvas.DestroyBox();
        }
        //otherwise show next sentence in the sentences array
        else
        {
            StartCoroutine(ShowText(sentences[currIndex]));
            currIndex++;
        }
    }

    public void ShowAnswerOptions()
    {

        foreach(GameObject go in owner.getButtons())
        {
            //instantiates the button and adds it to the arrayList;
           buttons.Add(Instantiate(go, transform));
        }
    }

    public void DestroyAllDialogueButtons()
    {
        //destroy every single dialogue button in the arrayList
        foreach(GameObject go in buttons)
        {
            Destroy(go);
        }
        //reset arraylist of buttons
        buttons = new ArrayList();
    }

}
