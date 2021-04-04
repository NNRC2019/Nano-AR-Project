using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// Class that designates the behavior of the Dialogue. It shows the text associated to its owner interactable
/// </summary>
public class DialogueBehavior : MonoBehaviour
{
    /// <summary>
    /// Cached instance of the text mesh pro component in this object.
    /// </summary>
    TextMeshProUGUI tmpro;

    /// <summary>
    /// Cached instance of the interaction canvas in the scene.
    /// </summary>
    InteractionCanvas canvas;
    /// <summary>
    /// Cached DialogueText object associated to the owner interactable.
    /// </summary>
    DialogueText d;

    //Dialoguebox fields
    /// <summary>
    /// Holds prefab of the object that will tell the dialogue what to say.
    /// </summary>
    [SerializeField] Interactable owner;
    //[SerializeField] string filename = "example1";
    /// <summary>
    /// Speed at which the chars of the sentences should be displayed.
    /// </summary>
    [SerializeField] float txtSpeed = 0.1f;
    //TextAsset txt; unsused for now
    /// <summary>
    /// ArrayLst that holds all dialogue buttons that are active.
    /// </summary>
    ArrayList buttons = new ArrayList();

    /// <summary>
    /// array to store sentences from the DIalogueText object.
    /// </summary>
    [SerializeField] string[] sentences;
    /// <summary>
    /// Variable that keeps track of the currentindex of the sentence that is being displayed.
    /// </summary>
    private int currIndex;


    /// <summary>
    /// In the start method we fetch our cached instances from the scene.
    /// We initialize our sentences array and we display the first sentence in the dialogue.
    /// </summary>
    void Start()
    {
        //get canvas with the script interactionCanvas
        canvas = FindObjectOfType<InteractionCanvas>();
        //get this object's text mesh pro component
        tmpro = GetComponent<TextMeshProUGUI>();

        //load text file from the resources folder. Has to be from the resources folder.
        //txt = Resources.Load("TextFiles/" + owner.GetFileName()) as TextAsset; unsused for now

        //Loading the dialogue manager associated to our owner. Because we only have a reference to the owner's prefab, it will search for the file with the name that is stated in the prefab, not the instance
        d = Resources.Load("TextFiles/" + owner.GetFileName()) as DialogueText;

        //get all sentences in our dialogueText object
        sentences = d.sentences;//txt.text.Split('\n'); <---Previous way to do it

        //start index at second position because we will show the first one on instantiation
        currIndex = 1;
        //on creation show first sentence
        StartCoroutine(ShowText(sentences[0]));
    }


    /// <summary>
    /// Coroutine to show next char sequentially with a time interval.
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    IEnumerator ShowText(string text)
    {
        //first it is empty
        tmpro.text = "";
        /*// At the moment we dont need this chunk of code but we will when we decide to incorporate buttons at the end of dialogue
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



    /// <summary>
    /// Method the continue button will call whenever it is pressed in order to show the next sentence in the array.
    /// </summary>
    public void showNextSentence()
    {

        //stops any coroutines currently occurring to not obstruct the one we will start in our else statement
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
        
    }


    /// <summary>
    /// When buttons are involved, displays the buttons.
    /// </summary>
    public void ShowAnswerOptions()
    {

        foreach (GameObject go in owner.getButtons())
        {
            //instantiates the button and adds it to the arrayList;
            buttons.Add(Instantiate(go, transform));
        }
    }

    /// <summary>
    /// When buttons are involved, destroys the buttons.
    /// </summary>
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
