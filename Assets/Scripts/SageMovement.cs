using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class SageMovement : MonoBehaviour
{
    private Camera arCamera;
    private Rigidbody rb;

    void Start()
    {
        arCamera = Camera.main;
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate() {

    }
        
      
  
}
