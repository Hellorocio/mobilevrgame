using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

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
        Yeehaw = 1 << 10
    }
    
    public enum ClothingCategory
    {
        Top = 1,
        Bottom = 2,
        Hat = 3,
        Shoes = 4,
        TopBottom = 5
    }

    public ClothingCategory category;

    [EnumFlag(2)]
    public ClothingTag clothingTags;

    public bool test;
    public bool equipped;

    // Start is called before the first frame update
    void Start()
    {
        //print("Tag:" + type + " (" + (int)type + ")");
    }

    // Update is called once per frame
    void Update()
    {
        if (test)
        {
            print("shirt: pos = " + transform.position + " local = " + transform.localPosition + " parent = " + transform.parent);
        }

        if (equipped && transform.localPosition != Vector3.zero)
        {
            transform.localPosition = Vector3.zero;
        }
    }

    public void TransformEquippedClothing (Transform parent)
    {
        transform.parent = parent;
        transform.rotation = parent.rotation;
        transform.localScale = Vector3.one;
        transform.localPosition = Vector3.zero;
        //print("old shirt: pos = " + parent.position);
        //print("new shirt: pos = " + transform.position + " local = " + transform.localPosition + " parent = " + transform.parent);
        equipped = true;
    }
}
