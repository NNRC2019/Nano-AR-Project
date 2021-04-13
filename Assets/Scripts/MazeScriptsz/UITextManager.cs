using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    void SetTarget()
    {
        target = FindObjectOfType<Maze>().gameObject; //For now we are targeting the Maze object when it gets instantiated
    }


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
