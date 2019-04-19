using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Equips nearby clothing objects and keeps track of clothing slots
public class ClothingGrabber : MonoBehaviour
{
    public GameObject[] clothingSlots; //correspond with category
                                       //leave 0 empty, 1 = top, 2 = bottom, 3 = hat, 4 = shoes, 5 = topbottom

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

    /// <summary>
    /// sets all clothing to default (called by reset button)
    /// </summary>
    public void ResetAllClothing ()
    {
        for (int i = 1; i < clothingSlots.Length; i++)
        {
            ResetClothingItem((ClothingObject.ClothingCategory)i);
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

    /// <summary>
    /// Called when the player drops a clothing item to check if it is near the bear (to equip it)
    /// </summary>
    /// <param name="clothing"></param>
    public void CheckEquipClothing (Transform clothing)
    {
        if (currentClothing != null && clothing == currentClothing.transform)
        {
            EquipClothing(clothing);
        }
    }

    /// <summary>
    /// Equips clothing when the player drops a clothing item on the bear
    /// </summary>
    /// <param name="clothing"></param>
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

    /// <summary>
    /// Called when the player clicks on a clothing item already on the bear, unequips the clothing
    /// </summary>
    /// <param name="clothingScript"></param>
    public void UnequipClothing (ClothingObject clothingScript)
    {
        if (clothingScript.removable && clothingScript.equipped)
        {
            ResetClothingItem(clothingScript.category, false);
        }
    }

    /// <summary>
    /// Resets the default clothing item on the bear
    /// </summary>
    /// <param name="slot"></param>
    /// <param name="resetPos"> When true, resets positon of clothing. We don't want this when the player is dragging clothing off the bear</param>
    public void ResetClothingItem (ClothingObject.ClothingCategory slot, bool resetPos = true)
    {
        int clothingSlot = (int)slot;
        if (defaultSlots[clothingSlot] != null && defaultSlots[clothingSlot] != clothingSlots[clothingSlot])
        {
            ClothingObject clothingScript = clothingSlots[clothingSlot].GetComponent<ClothingObject>();

            if (resetPos)
            {
                clothingScript.ResetTransform();
            }
            else
            {
                clothingScript.UnequipThis();
            }
            

            clothingSlots[clothingSlot] = defaultSlots[clothingSlot];
            clothingSlots[clothingSlot].SetActive(true);
        }
    }
}
