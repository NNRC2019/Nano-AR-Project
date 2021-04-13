using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    
    GameObject target;
    [SerializeField] float moveSpeed = 1f;


    void SetTarget()
    {
        target = FindObjectOfType<Maze>().gameObject; //For now we are targeting the Maze object when it gets instantiated
    }
 

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
