using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    
    GameObject targetToMove;
    [SerializeField] float moveSpeed = 1f;


    void SetTarget()
    {
        targetToMove = FindObjectOfType<Maze>().gameObject; //For now we are targeting the Maze object when it gets instantiated
    }
 

    public void MoveUp()
    {
        if (targetToMove == null)
        {
            SetTarget();
        }
        else
        {
            targetToMove.transform.position += new Vector3(0, moveSpeed, 0);
        }
       
    }

    public void MoveDown()
    {
        if (targetToMove == null)
        {
            SetTarget();
        }
        else
        {
            targetToMove.transform.position += new Vector3(0, -moveSpeed, 0);
        }
        
    }

    public void MoveLeft()
    {
        if (targetToMove == null)
        {
            SetTarget();
        }
        else
        {
            targetToMove.transform.position += new Vector3(-moveSpeed, 0, 0);
        }
        
    }

    public void MoveRight()
    {
        if (targetToMove == null)
        {
            SetTarget();
        }
        else
        {
            targetToMove.transform.position += new Vector3(moveSpeed, 0, 0);
        }
        
    }

    public void MoveForward()
    {
        if (targetToMove == null)
        {
            SetTarget();
        }
        else
        {
            targetToMove.transform.position += new Vector3(0, 0, moveSpeed);
        }
    }


    public void MoveBack()
    {
        if (targetToMove == null)
        {
            SetTarget();
        }
        else
        {
            targetToMove.transform.position += new Vector3(0, 0, -moveSpeed);
        }
    }


}
