using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandlerBaseModel : ObjectModel
{
    public virtual void ResetHandler()
    {

    }


    private void Reset()
    {
        transform.name = GetType().Name;
        transform.ResetLocal();
    }
}