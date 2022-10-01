using UnityEngine;

[System.AttributeUsage(System.AttributeTargets.Field, Inherited = true)]
public class EditorShowIf : PropertyAttribute
{
    public bool IsVisible;

    public EditorShowIf(bool isVisible)
    {
        IsVisible = isVisible;
    }
}

