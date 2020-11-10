using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoBehavior : MonoBehaviour
{

    const float SPEED = 6f;//Variable decides how fast description pops up

    [SerializeField]
    Transform SectionInfo; //Variable to assign description

    //Variables to keep track of description state
    Vector3 desiredScale = Vector3.zero;
    private bool visible = false;

    /*
     * When the object is created, method grabs child corresponding to description
     */
    void OnAwake()
    {
        SectionInfo = transform.GetChild(0).gameObject.transform;
    }

    //Description vector switches between normal and desginated scales.
    void Update()
    {
        SectionInfo.localScale = Vector3.Lerp(SectionInfo.localScale, desiredScale, Time.deltaTime * SPEED);
        OnClickAction();
    }

    public void OnClickAction()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
        if (visible && Physics.Raycast(ray, out hit, 100.0f)) {
            GameObject target = hit.collider.gameObject;
            if (target.Equals(SectionInfo.parent.gameObject))
            {
                if (target.name.Equals("Exit")) { Application.Quit(); }
                //Other scene transition code can go here
            }
        }
    }



    //Sets description to original size
    public void OpenInfo()
    {
        desiredScale = Vector3.one;
        visible = true;
    }

    //Sets description to invisible size
    public void CloseInfo()
    {
        desiredScale = Vector3.zero;
        visible = false;
    }
}
