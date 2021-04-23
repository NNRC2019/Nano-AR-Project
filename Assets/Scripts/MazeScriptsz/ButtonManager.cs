using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class handles the behavior of the buttons in the user interface. So far there are 8 buttons, 6 to move the selected object
/// and 2 to alter its size. All of its methods are meant to be called by the UI buttons.
/// </summary>
public class ButtonManager : MonoBehaviour
{
    
    GameObject target;
    [SerializeField] float moveSpeed = 1f;

    /// <summary>
    /// Finds the object of the specified type to be found in the scene
    /// </summary>
    void SetTarget()
    {
        target = FindObjectOfType<Maze>().gameObject; //For now we are targeting the Maze object when it gets instantiated
    }
 
    /// <summary>
    /// If the selected object is found in the scene. It moves it up. How far it moves the object is determined by the moveSpeed property.
    /// </summary>
    public void MoveUp()
    {
        if (target == null)
        {
            SetTarget();
        }
        else
        {
            target.transform.position += new Vector3(0, moveSpeed, 0);
        }
       
    }

    /// <summary>
    /// If the selected object is found in the scene. It moves it down. How far it moves the object is determined by the moveSpeed property.
    /// </summary>
    public void MoveDown()
    {
        if (target == null)
        {
            SetTarget();
        }
        else
        {
            target.transform.position += new Vector3(0, -moveSpeed, 0);
        }
        
    }
    /// <summary>
    /// If the selected object is found in the scene. It moves it to the left based on the user's perspective. How far it moves the object is determined by the moveSpeed property.
    /// </summary>
    public void MoveLeft()
    {
        if (target == null)
        {
            SetTarget();
        }
        else
        {
            target.transform.position += new Vector3(-moveSpeed, 0, 0);
        }
        
    }

    /// <summary>
    /// If the selected object is found in the scene. It moves it to the right based on the user's perspective. How far it moves the object is determined by the moveSpeed property.
    /// </summary>
    public void MoveRight()
    {
        if (target == null)
        {
            SetTarget();
        }
        else
        {
            target.transform.position += new Vector3(moveSpeed, 0, 0);
        }
        
    }

    /// <summary>
    /// If the selected object is found in the scene. It moves it away from the user. How far it moves the object is determined by the moveSpeed property.
    /// </summary>
    public void MoveForward()
    {
        if (target == null)
        {
            SetTarget();
        }
        else
        {
            target.transform.position += new Vector3(0, 0, moveSpeed);
        }
    }

    /// <summary>
    /// If the selected object is found in the scene. It moves it towards the user. How far it moves the object is determined by the moveSpeed property.
    /// </summary>
    public void MoveBack()
    {
        if (target == null)
        {
            SetTarget();
        }
        else
        {
            target.transform.position += new Vector3(0, 0, -moveSpeed);
        }
    }

    /// <summary>
    /// If the selected object is found in the scene. It halves it size by altering its scaling.
    /// </summary>
    public void Shrink()
    {
        if (target == null)
        {
            SetTarget();
        }
        else
        {
            target.transform.localScale /= 2;
        }
    }

    /// <summary>
    /// If the selected object is found in the scene. It duplicates its size by altering its scaling.
    /// </summary>
    public void Expand()
    {
        if (target == null)
        {
            SetTarget();
        }
        else
        {
            target.transform.localScale *= 2;
        }
    }
}
