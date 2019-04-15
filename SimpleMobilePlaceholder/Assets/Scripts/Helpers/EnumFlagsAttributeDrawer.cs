using System;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
//source: http://www.sharkbombs.com/2015/02/17/unity-editor-enum-flags-as-toggle-buttons/
[CustomPropertyDrawer(typeof(EnumFlagAttribute))]
public class EnumFlagsAttributeDrawer : PropertyDrawer
{

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        EnumFlagAttribute flagAttribute = attribute as EnumFlagAttribute;
        int enumLength = Mathf.CeilToInt(property.enumNames.Length * 1.0f / flagAttribute.columnCount);
        return (enumLength * (EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing));
    }

    public override void OnGUI(Rect _position, SerializedProperty _property, GUIContent _label)
    {
        EnumFlagAttribute flagAttribute = attribute as EnumFlagAttribute;

        int buttonsIntValue = 0;
        int enumLength = _property.enumNames.Length;
        bool[] buttonPressed = new bool[enumLength];
        float buttonWidth = (_position.width - EditorGUIUtility.labelWidth) / flagAttribute.columnCount;
        float buttonHeight = (_position.height / Mathf.Ceil(enumLength * 1.0f / flagAttribute.columnCount));

        EditorGUI.LabelField(new Rect(_position.x, _position.y, EditorGUIUtility.labelWidth, _position.height), _label);

        EditorGUI.BeginChangeCheck();

        for (int i = 0; i < enumLength; i++)
        {

            // Check if the button is/was pressed
            if ((_property.intValue & (1 << i)) == 1 << i)
            {
                buttonPressed[i] = true;
            }
            float rowIndex = Mathf.Floor(i / flagAttribute.columnCount);
            Rect buttonPos = new Rect(_position.x + EditorGUIUtility.labelWidth + buttonWidth * (i % flagAttribute.columnCount), _position.y + (rowIndex * buttonHeight), buttonWidth, buttonHeight);

            buttonPressed[i] = GUI.Toggle(buttonPos, buttonPressed[i], _property.enumNames[i], "Button");

            if (buttonPressed[i])
                buttonsIntValue += 1 << i;
        }

        if (EditorGUI.EndChangeCheck())
        {
            _property.intValue = buttonsIntValue;
        }
    }
}
#endif