using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DragScreen : ObjectModel
{
    public Vector3 pointPosition => pointer.localPosition;
    public bool isPointInGreen => pointPosition.y <= 89 && pointPosition.y >= -89;
    
    [SerializeField] private Text startTimer;
    [SerializeField] private RectTransform pointer;
    [SerializeField] private GameObject dragBar;
    [SerializeField] private Animator animator;

    public void StartTimer(Action onTimerEnded)
    {
        startTimer.text = "3";
        startTimer.DOCounter(3, 0, 3f).OnComplete(() => {
            startTimer.gameObject.SetActive(false);
            dragBar.SetActive(true);
            animator.Play("DragBarAnim");
            onTimerEnded.Invoke();
        });
    }

}
