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
        Red = 1,
        Blue = 2,
        Yellow = 3,
        Green = 4,
        Neutral = 5,
        Black = 6
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
    private Transform itemSlotParent;

    public Text debugText;

    Quaternion defaultRotation;
    Vector3 defaultPosition;
    Vector3 defaultScale;
    
 

    // Start is called before the first frame update
    void Start()
    {
        defaultPosition = transform.position;
        defaultRotation = transform.rotation;
        defaultScale = transform.localScale;

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

        if (equipped && (transform.localPosition != Vector3.zero || transform.parent != itemSlotParent))
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
        transform.parent = null;
        transform.position = defaultPosition;
        transform.rotation = defaultRotation;
        transform.localScale = defaultScale;
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
        if (!equipped || removable)
        {
            return true;
        }
        return false;
    }

    public MatchingCategory GetClothingMatch (StyleManager.Style compareStyle)
    {
        MatchingCategory match = MatchingCategory.bad;
        switch (compareStyle)
        {
            case StyleManager.Style.Casual:
                {
                    if ((clothingTags & ClothingTag.Casual) != 0)
                    {
                        match = MatchingCategory.great;
                    }
                    break;
                }
            case StyleManager.Style.Formal:
                {
                    if ((clothingTags & ClothingTag.Formal) != 0)
                    {
                        match = MatchingCategory.great;
                    }
                    break;
                }
            default:
                break;
        }
        return match; 
    }
}
