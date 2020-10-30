using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ARHoverPoint : MonoBehaviour
{

    public GameObject level1;
    public GameObject level1Menu;
   
    void Start()
    {
        
    }

    void Update()
    {
        //RaycastHit hit;
        //Ray ray = Camera.main.ScreenPointToRay(Camera.current.ViewportToScreenPoint(new Vector3(0.5f, 0.5f)));
        //if (Physics.Raycast(ray, out hit, Mathf.Infinity)) {
        //    GameObject target = hit.collider.gameObject.;
        //}

        if (Input.touchCount > 0)//if (Input.GetMouseButtonDown(0))
        {
       
            Touch touch = Input.GetTouch(0);

            //variable that will store the object hit by the raycast
            RaycastHit hit;

            //ray that will be shot out
            Ray ray = Camera.main.ScreenPointToRay(touch.position);//(Input.mousePosition);

            //if the ray hit an object Interact
            if (Physics.Raycast(ray, out hit, 100.0f))
            {
                //Check if the object clicked on is interactable by putting it in a variable and see if its null
                GameObject touched = hit.collider.gameObject;
                if (touched != null)
                {
                   ActivateMenu(touched);
                }
            }

        }
  
    }

    private void ActivateMenu(GameObject target)
    {
        if (target.name.Equals(level1.name))
            level1Menu.SetActive(true);

    }
}
