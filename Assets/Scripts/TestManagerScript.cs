using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestManagerScript : MonoBehaviour
{
    public GameObject canvas;
    public GameObject button;

    void Start()
    {
    
            GameObject newCanvas = Instantiate(canvas) as GameObject;
            GameObject newButton = Instantiate(button) as GameObject;
            newButton.transform.SetParent(newCanvas.transform, false);
    }
    
}
