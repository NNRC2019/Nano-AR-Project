using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIAppearClic : MonoBehaviour
{
    [SerializeField] private Image customImage;

    void Start()
    {
        customImage.enabled = false;
    }
    
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            customImage.enabled = true;

        if (Input.GetMouseButtonDown(1))
            customImage.enabled = false;

    }

}

