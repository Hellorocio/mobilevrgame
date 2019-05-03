using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClothingRackObject : MonoBehaviour
{
    private ClothingObject[] clothingOnRack;

    // Start is called before the first frame update
    void Start()
    {
        clothingOnRack = gameObject.GetComponentsInChildren<ClothingObject>();
    }
    
    /// <summary>
    /// Called by ClothingRackManager when reset button is pressed
    /// </summary>
    public void ResetRack ()
    {
        foreach (ClothingObject c in clothingOnRack)
        {
            c.ResetTransform();
        }
    }
    
    
}
