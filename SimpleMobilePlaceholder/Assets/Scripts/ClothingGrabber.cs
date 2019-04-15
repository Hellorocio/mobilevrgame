using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Equips nearby clothing objects and keeps track of clothing slots
public class ClothingGrabber : MonoBehaviour
{
    public GameObject[] clothingSlots; //correspond with category

    GameObject[] defaultSlots;

    GameObject currentClothing;
    

    // Start is called before the first frame update
    void Start()
    {
        defaultSlots = new GameObject[clothingSlots.Length];
        for (int i = 0; i < clothingSlots.Length; i++)
        {
            if (clothingSlots != null)
            {
                defaultSlots[i] = clothingSlots[i];
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    //sets clothing to default (called by button)
    public void ResetClothing ()
    {
        for (int i = 0; i < clothingSlots.Length; i++)
        {
            if (defaultSlots[i] != null && defaultSlots[i] != clothingSlots[i])
            {
                ClothingObject clothingScript = clothingSlots[i].GetComponent<ClothingObject>();
                clothingScript.ResetTransform();
                clothingSlots[i] = defaultSlots[i];
                clothingSlots[i].SetActive(true);
            }
            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject collidedObj = other.gameObject;
        ClothingObject clothingScript = collidedObj.GetComponent<ClothingObject>();
        if (collidedObj.tag == "Clothing" && clothingScript != null && !clothingScript.equipped && collidedObj != currentClothing)
        {
            currentClothing = collidedObj;
            //collidedObj.tag = "Untagged";
            
            //EquipClothing(collidedObj.transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == currentClothing)
        {
            currentClothing = null;
        }
    }

    public void CheckEquipClothing (Transform clothing)
    {
        if (currentClothing != null && clothing == currentClothing.transform)
        {
            EquipClothing(clothing);
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
            currentClothing = null;
        }
    }
}
