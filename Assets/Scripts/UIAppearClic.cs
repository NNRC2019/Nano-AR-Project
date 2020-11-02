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

    void OnMouseDown()
    {
        customImage.enabled = true;
    }

}

