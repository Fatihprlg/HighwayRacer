using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class UIElement : ObjectModel
{
    [HideInInspector] public RectTransform ElementTransform;

    private void Reset()
    {
        if (ElementTransform == null)
            ElementTransform = GetComponent<RectTransform>();
    }

    public virtual void Disable()
    {
        gameObject.SetActive(false);
    }

}