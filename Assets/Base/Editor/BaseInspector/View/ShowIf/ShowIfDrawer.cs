using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(EditorShowIf))]
public class ShowIfDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorShowIf showIf = attribute as EditorShowIf;

        if (showIf.IsVisible)
        {
            EditorGUI.PropertyField(position, property, label);
        }
    }
}
