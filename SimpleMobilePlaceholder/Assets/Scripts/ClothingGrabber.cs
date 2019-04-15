using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Equips nearby clothing objects and keeps track of clothing slots
public class ClothingGrabber : MonoBehaviour
{
    public GameObject[] clothingSlots; //correspond with category

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject collidedObj = other.gameObject;

        if (collidedObj.tag == "Clothing")
        {
            collidedObj.tag = "Untagged";
            
            EquipClothing(collidedObj.transform);
        }
    }

    private void EquipClothing (Transform clothing)
    {
        ClothingObject clothingScript = clothing.GetComponent<ClothingObject>();
        DraggableScript draggableScript = clothing.GetComponent<DraggableScript>();
        if (clothingScript != null && draggableScript != null)
        {
            draggableScript.Release();


            GameObject oldClothing = clothingSlots[(int)clothingScript.category];
            clothingSlots[(int)clothingScript.category] = clothing.gameObject;
            

            clothingScript.TransformEquippedClothing(oldClothing.transform.parent);

            oldClothing.SetActive(false);            
        }
    }
}
