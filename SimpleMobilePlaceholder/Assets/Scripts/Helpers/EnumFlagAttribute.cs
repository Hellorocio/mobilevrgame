using UnityEngine;
using System;

//Source: http://www.sharkbombs.com/2015/02/17/unity-editor-enum-flags-as-toggle-buttons/
public class EnumFlagAttribute : PropertyAttribute
{
    public int columnCount;
    public EnumFlagAttribute(int rowCount)
    {
        this.columnCount = rowCount;
    }
}