using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
[RequireComponent(typeof(Animator))]
public class ScreenElement : UIElement
{
    public CanvasGroup CanvasGroup;
    public Animator Animator;
    [SerializeField] string ShowAnimName, HideAnimName;
    Action onDisable;

    public virtual void Show()
    {
        gameObject.SetActive(true);
        CanvasGroup.interactable = true;
        if (Animator.runtimeAnimatorController != null)
            Animator.Play((ShowAnimName != "" ? ShowAnimName : "Intro"), 0, 0);
    }

    public virtual void Hide(Action onDisable = null)
    {
        this.onDisable = onDisable;
        CanvasGroup.interactable = false;

        if (Animator.runtimeAnimatorController != null)
            Animator.Play((HideAnimName != "" ? HideAnimName : "Outro"), 0, 0);
        else
            Disable();

    }


    public override void Disable()
    {
        onDisable?.Invoke();
        base.Disable();
    }

    public virtual void ResetScreen()
    {

    }

    private void Reset()
    {
        Animator = GetComponent<Animator>();
        CanvasGroup = GetComponent<CanvasGroup>();
        transform.name = GetType().Name;
        ElementTransform = GetComponent<RectTransform>();
        ElementTransform.anchorMin = Vector2.zero;
        ElementTransform.anchorMax = Vector2.one;
        ElementTransform.sizeDelta = Vector2.zero;
        ElementTransform.anchoredPosition = Vector2.zero;
    }
}
