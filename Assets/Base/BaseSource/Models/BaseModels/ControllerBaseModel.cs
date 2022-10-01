using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerBaseModel : ObjectModel
{

    public virtual void OnStateChange(GameStates gameStates)
    {

    }

    public virtual void ControllerUpdate(GameStates currentState)
    {

    }

    public virtual void ResetController()
    {

    }

    protected void Reset()
    {
        transform.name = GetType().Name;
        transform.ResetLocal();
    }
}