using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocationTextManager : MonoBehaviour
{
    
    [SerializeField] Text xPosText;
    [SerializeField] Text yPosText;
    [SerializeField] Text zPosText;

    GameObject target;
    
    void Update()
    {
        if(target == null)
        {
            SetTarget();
        }
        else
        {
            if(xPosText != null) xPosText.text = $"X : {target.transform.position.x}";
            if(yPosText != null) yPosText.text = $"Y : {target.transform.position.y}";
            if(zPosText != null) zPosText.text = $"Z : {target.transform.position.z}";
        }
    }

    void SetTarget()
    {
        target = FindObjectOfType<Maze>().gameObject; //For now we are targeting the Maze object when it gets instantiated
    }
}
