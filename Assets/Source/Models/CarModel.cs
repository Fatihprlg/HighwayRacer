using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarModel : ObjectModel
{
    public bool isStopped;
    
    [SerializeField] private float roadLimit;
    [SerializeField] private float sensitivity;
    [SerializeField] private float rotationAngle;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private int dragSpeed;
    [SerializeField] private PointerController pointerController;
    [SerializeField] private Rigidbody rb;


    private bool finishLineReached;
    private bool isSwiping;
    private float xPosition;
    private float lastXPosition;
    
    public void ModelUpdate()
    {
        pointerController.ControllerUpdate();
        if (!finishLineReached)
        {
            MovementUpdate();
            PlayerModelRotation();
        }
    }
    private void MovementUpdate()
    {
        transform.localPosition = new Vector3(xPosition, 0, 0);
    }
    public void OnFinishLineReached()
    {
        transform.DOLocalMove(Vector3.zero, .3f);
        transform.localRotation = Quaternion.Euler(Vector3.zero);
        finishLineReached = true;
    }

    public void OnCrushEnded()
    {
        pointerController.ResetPointer();
    }

    public void OnDragStarted()
    {
        rb.velocity = transform.forward * dragSpeed;
    }

    public void OnDragAddSpeed(float dragAdditionalSpeed)
    {
        rb.velocity += transform.forward * dragAdditionalSpeed;
    }

    public void OnDragEnd()
    {
        var startVel = rb.velocity;
        DOTween.To(() => startVel, x => rb.velocity = x, Vector3.zero, 1f);
    }

    public void OnPointerDown()
    {
        xPosition = lastXPosition;
        isSwiping = false;
    }
    public void OnPointer()
    {
        xPosition = lastXPosition + pointerController.DeltaPosition.x * sensitivity;
        xPosition = Mathf.Clamp(xPosition, -roadLimit, roadLimit);
        isSwiping = true;
    }
    public void OnPointerUp()
    {
        lastXPosition = xPosition;

        isSwiping = false;
    }


    public virtual void RotateModelToLeftRight()
    {
        if (transform.localPosition.x > lastXPosition)
        {
            // print("right");
            transform.localRotation = Quaternion.Lerp(
                transform.localRotation,
                Quaternion.Euler(0,
                    transform.localRotation.y +
                    rotationAngle, 0),
                Time.deltaTime * rotationSpeed);
        }
        else if (transform.localPosition.x < lastXPosition)
        {
            // print("left");
            transform.localRotation = Quaternion.Lerp(
                transform.localRotation,
                Quaternion.Euler(0,
                    transform.localRotation.y -
                    rotationAngle, 0),
                Time.deltaTime * rotationSpeed);
        }
        else
        {
            RotateModelToCenter();
        }
    }

    public virtual void RotateModelToCenter()
    {
        transform.localRotation = Quaternion.Lerp(
            transform.localRotation, Quaternion.Euler(0, 0, 0),
            Time.deltaTime * rotationSpeed / 2f);
    }

    public virtual void PlayerModelRotation()
    {
        switch (isSwiping)
        {
            case true:
                RotateModelToLeftRight();
                lastXPosition = transform.localPosition.x;
                break;
            case false:
                RotateModelToCenter();
                break;
        }
    }
}
