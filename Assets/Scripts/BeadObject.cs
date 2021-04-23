using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script contains all properties and methods pertaining
/// to the bead object.
/// </summary>
public class BeadObject : MonoBehaviour
{

    [SerializeField] GameObject molecule;
    private bool isUV = false;

    /// <summary>
    /// Start function designates "isUV" property to True or
    /// False based on a 7:3 ratio. (70% true : 30% false)
    /// </summary>
    public void Start()
    {
        int num = Random.Range(1, 11);
        if (num > 3)
        {
            isUV = true;
        }
    }

    /// <summary>
    /// Determines if bead is UV by accessing its boolean property.
    /// </summary>
    /// <returns>Returns a boolean based on isUV property.</returns>
    public bool IsUV()
    {
        return isUV;
    }

    /// <summary>
    /// Method will call molecule property and display it
    /// for inspection by player.
    /// </summary>
    public void Inspect()
    {
        //ToDo call molecule property
    }

    /// <summary>
    /// Method changes color of bead object to a lighter version
    /// if it is categorized as UV bead.
    /// </summary>
    public void Shine()
    {
        if (isUV)
        {
            var beadMaterial = this.GetComponent<Renderer>().material;
            beadMaterial.color *= 1.5f;
        }
    }

    /// <summary>
    /// Method changes color of bead object to a darker version
    /// to reverse the shine effect.
    /// </summary>
    public void UnShine()
    {
        if (isUV)
        {
            var beadMaterial = this.GetComponent<Renderer>().material;
            beadMaterial.color /= 1.5f;
        }
    }

}
