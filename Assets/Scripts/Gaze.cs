using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/*
 * Script by Geidel X. Solivan on Nov 2020
 */
public class Gaze : MonoBehaviour
{
    List<InfoBehavior> infos = new List<InfoBehavior>(); //List to keep track of all objects with description

    //Finds all said objects and saves them on list.
    void Start()
    {
        infos = FindObjectsOfType<InfoBehavior>().ToList(); 
    }

    /*
     * Sends ray from screen towards front and if it hits one of the objects
     * with a description, opens its menu and closes the rest.
     */
    void Update()
    {
        
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit))
        {
            GameObject target = hit.collider.gameObject;
            if (target.CompareTag("hasDescription"))
            {
                infos = FindObjectsOfType<InfoBehavior>().ToList();
                OpenMenu(target.GetComponent<InfoBehavior>());
            }
            else
            {
                CloseAll();
            }
        }
    }

    //Opens target description and closes the rest.
    void OpenMenu(InfoBehavior desiredInfo)
    {
        foreach (InfoBehavior info in infos)
            if (info == desiredInfo)
            {
                info.OpenInfo();
            }
            else
            {
                info.CloseInfo();
            }
    }

    //Loops through all descriptions closing them
    void CloseAll()
    {
        foreach (InfoBehavior info in infos)
        {
            info.CloseInfo();
        }
    }
}
