using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TagManager
{
    public static ClothingObject.ClothingTag[] greatTags =
    {
        //casual
        ClothingObject.ClothingTag.Casual | 
        ClothingObject.ClothingTag.Ironic | 
        ClothingObject.ClothingTag.Cute | 
        ClothingObject.ClothingTag.Cool |
        ClothingObject.ClothingTag.Sporty |
        ClothingObject.ClothingTag.Stylish |
        ClothingObject.ClothingTag.Yeehaw |
        ClothingObject.ClothingTag.BusCas,

        //formal
        ClothingObject.ClothingTag.Formal |
        ClothingObject.ClothingTag.Elegant
    };

    public static ClothingObject.ClothingTag[] okayTags =
    {
        //casual
        ClothingObject.ClothingTag.Tacky |
        ClothingObject.ClothingTag.Formal |
        ClothingObject.ClothingTag.Elegant, 

        //formal
        ClothingObject.ClothingTag.Cute |
        ClothingObject.ClothingTag.Stylish |
        ClothingObject.ClothingTag.BusCas
    };

    public static ClothingObject.ClothingTag[] questionableTags =
    {
        //casual
        0,

        //formal
        ClothingObject.ClothingTag.Casual |
        ClothingObject.ClothingTag.Cool |
        ClothingObject.ClothingTag.Sporty
    };

    public static ClothingObject.ClothingTag[] badTags =
    {
        //casual
        ClothingObject.ClothingTag.Bad,

        //formal
        ClothingObject.ClothingTag.Tacky |
        ClothingObject.ClothingTag.Ironic |
        ClothingObject.ClothingTag.Bad |
        ClothingObject.ClothingTag.Yeehaw
    };


}
