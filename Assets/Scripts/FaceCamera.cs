
using UnityEngine;

/*
 * Script by Geidel X. Solivan on Nov 2020
 */
public class FaceCamera : MonoBehaviour
{
    Transform cam; //Variable to track camera
    Vector3 targetAngle = Vector3.zero;

    void Start()
    {
        cam = Camera.main.transform; //Grabs main camera, in our case the AR camera
    }

    /*
     * Makes the description move along with the camera on the "y" axis.  
     */
    void Update()
    {
        transform.LookAt(cam);
        targetAngle = transform.localEulerAngles;
        targetAngle.x = 0;
        targetAngle.z = 0;
        transform.localEulerAngles = targetAngle;
    }
}
