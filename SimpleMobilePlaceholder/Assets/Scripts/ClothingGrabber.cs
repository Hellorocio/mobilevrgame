using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Equips nearby clothing objects and keeps track of clothing slots
public class ClothingGrabber : MonoBehaviour
{
    public GameObject[] clothingSlots; //correspond with category
                                       //leave 0 empty, 1 = top, 2 = bottom, 3 = hat, 4 = shoes, 5 = topbottom

    public Material[] clothingColors;
    public GameObject colorPicker;    

    public AudioSource clothingSound;

    GameObject[] defaultSlots;

    GameObject currentClothing; //clothing hovering over bear
    int selectedClothing = -1; //slot clothing that will change color
    HighlightObject highlightedClothing;  //clothing on bear we are about to replace

    
    
    public GameObject debug;
    

    // Start is called before the first frame update
    void Start()
    {
        defaultSlots = new GameObject[clothingSlots.Length];
        for (int i = 0; i < clothingSlots.Length; i++)
        {
            if (clothingSlots[i] != null)
            {
                defaultSlots[i] = clothingSlots[i];
            }
        }
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

            HighlightSelectedClothing(clothingScript);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == currentClothing)
        {
            currentClothing = null;
            UnHighlightSelectedClothing();
        }
    }

    /// <summary>
    /// Also sets selectedClothing to the highlightObject so we can easily unhighlight it
    /// </summary>
    private void HighlightSelectedClothing(ClothingObject newClothingScript)
    {
        GameObject selectedClothing = clothingSlots[(int)newClothingScript.category];
        if (selectedClothing != null)
        {
            HighlightObject highlightScript = selectedClothing.GetComponentInChildren<HighlightObject>();
            if (highlightScript != null)
            {
                
                highlightScript.SetHighlightMaterial(true);

                highlightedClothing = highlightScript;
            }
        }
    }

    private void UnHighlightSelectedClothing ()
    {
        if (highlightedClothing != null)
        {
            highlightedClothing.SetHighlightMaterial(false);
        }
    }

    /// <summary>
    /// Called when the player drops a clothing item to check if it is near the bear (to equip it)
    /// </summary>
    /// <param name="clothing"></param>
    public void CheckEquipClothing (Transform clothing)
    {
        //first set dragging because this is called on pointer up
        clothing.GetComponent<ClothingObject>().SetDragging(false);

        if (currentClothing != null && clothing == currentClothing.transform)
        {
            EquipClothing(clothing);
            PlayClothingSound();
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
            UnHighlightSelectedClothing();


            GameObject oldClothing = clothingSlots[(int)clothingScript.category];
            clothingSlots[(int)clothingScript.category] = clothing.gameObject;
            

            clothingScript.TransformEquippedClothing(oldClothing.transform.parent);

            //deactivate clothing if default, reset it otherwise
            if (oldClothing == defaultSlots[(int)clothingScript.category])
            {
                oldClothing.SetActive(false);
            }
            else
            {
                ClothingObject oldClothingScript = oldClothing.GetComponent<ClothingObject>();
                oldClothingScript.ResetTransform();
            }

            if (colorPicker != null && !colorPicker.activeSelf)
            {
                colorPicker.SetActive(true);
            }

            selectedClothing = (int)clothingScript.category;
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
            PlayClothingSound();
        }

        //set dragging because this is called on pointer down
        clothingScript.SetDragging(true);
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

    /// <summary>
    /// Calculates maximum number of matching colors to get the bonus
    /// </summary>
    /// <returns></returns>
    public int GetMaxMatchingColors ()
    {
        int maxColor = 1;
        int[] colorTotals = new int[System.Enum.GetValues(typeof(ClothingObject.ClothingColor)).Length + 1];
        for (int i = 0; i < clothingSlots.Length; i++)
        {
            if (clothingSlots[i] != null)
            {
                ClothingObject clothingScript = clothingSlots[i].GetComponent<ClothingObject>();
                colorTotals[(int)clothingScript.color]++;
            }
        }
        
        for (int i = 0; i < colorTotals.Length; i++)
        {
            if ((ClothingObject.ClothingColor)i != ClothingObject.ClothingColor.None && colorTotals[i] > maxColor)
            {
                maxColor = colorTotals[i];
            }
        }
        return maxColor;
    }

    /// <summary>
    /// Determines if a clothing item matches the style
    /// </summary>
    /// <returns></returns>
    public string[] GetMatchingStyleScore(StyleManager.Style currentStyle)
    {
        string[] tally = new string[2]; //0 = score, 1 = tally
        int score = 0;

        for (int i = 0; i < clothingSlots.Length; i++)
        {
            if (clothingSlots[i] != null && clothingSlots[i].name != "NoHat")
            {
                ClothingObject clothingScript = clothingSlots[i].GetComponent<ClothingObject>();
                ClothingObject.MatchingCategory clothingMatch = clothingScript.GetClothingMatch(currentStyle);
                tally[1] += GetMatchingBonus((int)clothingMatch) + " " + clothingMatch.ToString() + " " + clothingScript.category.ToString().ToLower() + "\n";


                score += (int)clothingMatch;
            }
        }

        tally[0] = score.ToString();
        return tally;
    }

    private string GetMatchingBonus (int match)
    {
        string result = match.ToString();
        if (match > 0)
        {
            result = "+" + result;
        }        
        return result;
    }

    private void PlayClothingSound ()
    {
        if (clothingSound != null && !clothingSound.isPlaying)
        {
            clothingSound.Play();
        }
    }


    public void ClothingColorChange (ColorButton colorButton)
    {
        if (selectedClothing != -1 && clothingColors[(int)colorButton.color] != null)
        {
            clothingSlots[selectedClothing].GetComponentInChildren<HighlightObject>().SetMaterial(clothingColors[(int)colorButton.color]);
            clothingSlots[selectedClothing].GetComponent<ClothingObject>().color = colorButton.color;
        }
    }

}
