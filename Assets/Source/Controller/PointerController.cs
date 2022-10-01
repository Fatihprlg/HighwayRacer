using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[System.Serializable]
public class PointerController
{
    public Vector2 PointerDownPosition;
    public Vector2 PointerPosition;
    public Vector2 PointerUpPosition;
    public Vector2 PointerLastPosition;
    public Vector2 DeltaPosition
    {
        get
        {
            return new Vector2((PointerPosition.x - PointerLastPosition.x) / Screen.width, (PointerPosition.y - PointerLastPosition.y) / Screen.height);
        }
    }

    public UnityEvent OnPointerDownEvent;
    public UnityEvent OnPointerEvent;
    public UnityEvent OnPointerUpEvent;

    public void ControllerUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            OnPointerDown();
        }

        if (Input.GetMouseButton(0))
        {
            OnPointer();
        }

        if (Input.GetMouseButtonUp(0))
        {
            OnPointerUp();
        }
    }

    public void OnPointerDown()
    {
        PointerDownPosition = Input.mousePosition;
        PointerLastPosition = PointerDownPosition;
        if (OnPointerDownEvent != null)
            OnPointerDownEvent.Invoke();
    }

    public void OnPointer()
    {
        PointerPosition = Input.mousePosition;
        if (OnPointerEvent != null)
            OnPointerEvent.Invoke();
        PointerLastPosition = Input.mousePosition;
    }

    public void OnPointerUp()
    {
        PointerUpPosition = Input.mousePosition;
        PointerLastPosition = PointerUpPosition;
        if (OnPointerUpEvent != null)
            OnPointerUpEvent.Invoke();
    }
   
    public void ResetPointer()
    {
        PointerDownPosition = Input.mousePosition;
        PointerLastPosition = PointerDownPosition;

    }
}
