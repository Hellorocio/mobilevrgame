using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TagManager
{
    public static ClothingObject.ClothingTag[] greatTags =
    {
        ClothingObject.ClothingTag.Casual | 
        ClothingObject.ClothingTag.Ironic | 
        ClothingObject.ClothingTag.Cute | 
        ClothingObject.ClothingTag.Cool |
        ClothingObject.ClothingTag.Sporty |
        ClothingObject.ClothingTag.Stylish |
        ClothingObject.ClothingTag.Yeehaw |
        ClothingObject.ClothingTag.BusCas,

        ClothingObject.ClothingTag.Formal |
        ClothingObject.ClothingTag.Elegant
    };

    public static ClothingObject.ClothingTag[] okayTags =
    {
        ClothingObject.ClothingTag.Tacky |
        ClothingObject.ClothingTag.Formal |
        ClothingObject.ClothingTag.Elegant, 

        ClothingObject.ClothingTag.Cute |
        ClothingObject.ClothingTag.Stylish
    };

    
}
