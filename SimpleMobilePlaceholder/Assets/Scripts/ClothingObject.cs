using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClothingObject : MonoBehaviour
{
    
    [System.Flags]
    public enum ClothingTag
    {
        Casual = 1 << 0,
        Tacky = 1 << 1,
        Ironic = 1 << 2,
        Formal = 1 << 3,
        Elegant = 1 << 4,
        Cute = 1 << 5,
        Cool = 1 << 6,
        Sporty = 1 << 7,
        Bad = 1 << 8,
        Stylish = 1 << 9,
        Yeehaw = 1 << 10,
        BusCas = 1 << 11,
    }
    
    public enum ClothingCategory
    {
        Top = 1,
        Bottom = 2,
        Hat = 3,
        Shoes = 4,
        TopBottom = 5
    }

    public enum ClothingColor
    {
        None = 0,
        Red = 1,
        Orange = 2,
        Yellow = 3,
        Green = 4,
        Blue = 5,
        Purple = 6,
        Pink = 7,
        LightOrange = 8,
        LightYellow = 9,
        LightGreen = 10,
        LightBlue = 11,
        LightPurple = 12,
        Brown = 13,
        LightBrown = 14,
        Black = 15,
        Gray = 16,
        White = 17,
        Neutral = 18,
    }

    public enum MatchingCategory
    {
        bad = -5,
        questionable = 0,
        okay = 5,
        great = 10
    }

    public ClothingCategory category;
    public ClothingColor color;

    [EnumFlag(2)]
    public ClothingTag clothingTags;

    public bool removable; //used for hats to allow you to click to take them off
    public bool test;
    public bool equipped;
    public bool dragging;
    private Transform itemSlotParent;

    public Text debugText;

    Quaternion defaultRotation;
    Vector3 defaultPosition;
    Vector3 defaultScale;
    Transform defaultParent;
 

    // Start is called before the first frame update
    void Start()
    {
        defaultPosition = transform.position;
        defaultRotation = transform.rotation;
        defaultScale = transform.localScale;
        defaultParent = transform.parent;
        //print("clothing: " + gameObject.name + "parent = " + defaultParent);

        if (equipped)
        {
            itemSlotParent = transform.parent;
        }
        //print("Tag:" + type + " (" + (int)type + ")");
    }

    // Update is called once per frame
    void Update()
    {
        if (debugText != null)
        {
            //debugText.text = "shirt: pos = " + transform.position + " local = " + transform.localPosition + " parent = " + transform.parent;
            //print("shirt: pos = " + transform.position + " local = " + transform.localPosition + " parent = " + transform.parent);
        }

        if (equipped && itemSlotParent != null && (transform.localPosition != Vector3.zero || transform.parent != itemSlotParent))
        {
            transform.parent = itemSlotParent;
            transform.localPosition = Vector3.zero;
        }
    }

    public void TransformEquippedClothing (Transform parent)
    {
        transform.parent = parent;
        transform.rotation = parent.rotation;
        transform.localScale = Vector3.one;
        transform.localPosition = Vector3.zero;

        itemSlotParent = parent;
        //print("old shirt: pos = " + parent.position);
        //print("new shirt: pos = " + transform.position + " local = " + transform.localPosition + " parent = " + transform.parent);
        equipped = true;
    }

    public void ResetTransform ()
    {
        equipped = false;
        transform.parent = defaultParent;
        transform.position = defaultPosition;
        transform.rotation = defaultRotation;
        transform.localScale = defaultScale;
        //print("transform reset for " + gameObject + " parent = " + transform.parent);
    }

    public void UnequipThis()
    {
        equipped = false;
        
    }

    /// <summary>
    /// Used by Highlight Object to figure out if this object is highlightable (either removable or not equipped)
    /// </summary>
    /// <returns></returns>
    public bool AllowHighlight ()
    {
        if ((!equipped || removable) && !dragging)
        {
            return true;
        }
        return false;
    }


    /// <summary>
    /// Returns how well this clothing item matches the given style
    /// </summary>
    /// <param name="compareStyle"></param>
    /// <returns></returns>
    public MatchingCategory GetClothingMatch (StyleManager.Style compareStyle)
    {
        MatchingCategory match = MatchingCategory.bad;
        if ((clothingTags & TagManager.greatTags[(int)compareStyle - 1]) != 0)
        {
            match = MatchingCategory.great;
        }
        else
        if ((clothingTags & TagManager.okayTags[(int)compareStyle - 1]) != 0)
        {
            match = MatchingCategory.okay;
        }
        else
        if ((clothingTags & TagManager.questionableTags[(int)compareStyle - 1]) != 0)
        {
            match = MatchingCategory.questionable;
        }
        else
        if ((clothingTags & TagManager.badTags[(int)compareStyle - 1]) != 0)
        {
            match = MatchingCategory.bad;
        }
        return match; 
    }

    public void SetDragging (bool set)
    {
        dragging = set;
    }
}
