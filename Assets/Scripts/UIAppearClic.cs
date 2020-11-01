using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIAppearClic : MonoBehaviour
{
    [SerializeField] private Image customImage;

   private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            customImage.enabled = false;
        }
    }

}

