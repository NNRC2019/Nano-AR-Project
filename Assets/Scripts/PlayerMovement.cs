using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script contains the variables and methods to allow player movement. The "AR Session Origin"
/// is treated as the player and the "AR Camera" is treated as the player view.
/// </summary>
public class PlayerMovement : MonoBehaviour
{

    GameObject player;
    GameObject view;
    [SerializeField] float moveSpeed = 1f;

    /// <summary>
    /// Method assigns "AR Session Origin" to player property and "AR Camera" to view property.
    /// </summary>
    void SetTarget()
    {
        player = GameObject.Find("AR Session Origin");
        view = GameObject.Find("AR Camera");
    }

    /// <summary>
    /// Method calculates a new position for the player based on where they are looking and, if 
    /// it is within the allowed height, assigns this new position to the player.
    /// </summary>
    public void MoveForward()
    {
        if (player == null)
        {
            SetTarget();
        }

        var newPosition = player.transform.position + view.transform.forward * moveSpeed;

        //-1 and 2 are subject to change depending on allowed height.
        if (newPosition.y > -1 && newPosition.y < 2)
        {
            player.transform.position = newPosition;
        }
    }
}
