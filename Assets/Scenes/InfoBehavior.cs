using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoBehavior : MonoBehaviour
{

    const float SPEED = 6f;

    [SerializeField]
    Transform SectionInfo; //Variable to keep track of level description

    Vector3 desiredScale = Vector3.zero;

    void OnAwake()
    {
        SectionInfo = transform.GetChild(0).gameObject.transform;
    }
    //Description vector switches between normal and desginated scales.
    void Update()
    {
        
        SectionInfo.localScale = Vector3.Lerp(SectionInfo.localScale, desiredScale, Time.deltaTime * SPEED);
    }

    //Sets description to original size
    public void OpenInfo()
    {
        desiredScale = Vector3.one;
    }

    //Sets description to invisible size
    public void CloseInfo()
    {
        desiredScale = Vector3.zero;
    }
}
