using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This class manages the UI labels that give the player information on the target position and scale properties
/// </summary>
public class UITextManager : MonoBehaviour
{
    [SerializeField] Text xText;
    [SerializeField] Text yText;
    [SerializeField] Text zText;
    [SerializeField] bool posText = true;

    GameObject target;

    void Update()
    {
        if (target == null)
        {
            SetTarget();
        }
        else
        {
            setText();
        }
    }

    /// <summary>
    /// Finds the object of the specified type to be found in the scene
    /// </summary>
    void SetTarget()
    {
        target = FindObjectOfType<Maze>().gameObject; //For now we are targeting the Maze object when it gets instantiated
    }

    /// <summary>
    /// Sets the text for the label depending on whethers its position text or scale text.
    /// </summary>
    void setText()
    {
        if (posText)
        {
            if (xText != null) xText.text = $"X : {target.transform.position.x}";
            if (yText != null) yText.text = $"Y : {target.transform.position.y}";
            if (zText != null) zText.text = $"Z : {target.transform.position.z}";
        }
        else
        {
            if (xText != null) xText.text = $"X : {target.transform.localScale.x}";
            if (yText != null) yText.text = $"Y : {target.transform.localScale.y}";
            if (zText != null) zText.text = $"Z : {target.transform.localScale.z}";
        }
    }
}
