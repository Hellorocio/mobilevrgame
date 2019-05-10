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
        ClothingObject.ClothingTag.Elegant,

        //business casual
        ClothingObject.ClothingTag.Formal |
        ClothingObject.ClothingTag.BusCas
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
        ClothingObject.ClothingTag.BusCas,

        //bus cas
        ClothingObject.ClothingTag.Cute |
        ClothingObject.ClothingTag.Cool |
        ClothingObject.ClothingTag.Stylish |
        ClothingObject.ClothingTag.Elegant,

    };

    public static ClothingObject.ClothingTag[] questionableTags =
    {
        //casual
        0,

        //formal
        ClothingObject.ClothingTag.Casual |
        ClothingObject.ClothingTag.Cool |
        ClothingObject.ClothingTag.Sporty,

        //bus cas
        ClothingObject.ClothingTag.Casual |
        ClothingObject.ClothingTag.Tacky |
        ClothingObject.ClothingTag.Ironic |
        ClothingObject.ClothingTag.Yeehaw
    };

    public static ClothingObject.ClothingTag[] badTags =
    {
        //casual
        ClothingObject.ClothingTag.Bad,

        //formal
        ClothingObject.ClothingTag.Tacky |
        ClothingObject.ClothingTag.Ironic |
        ClothingObject.ClothingTag.Bad |
        ClothingObject.ClothingTag.Yeehaw,

        //bus cas
        ClothingObject.ClothingTag.Bad |
        ClothingObject.ClothingTag.Sporty
    };


}
