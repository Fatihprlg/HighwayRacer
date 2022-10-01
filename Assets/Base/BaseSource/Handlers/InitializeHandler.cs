using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializeHandler : HandlerBaseModel
{
    [SerializeField] private ObjectModel[] initalizeElement;
    [SerializeField] private bool initializeOnAwake;

    private void Awake()
    {
        if (initializeOnAwake)
            Initialize();
    }

    public override void Initialize()
    {
        foreach (var item in initalizeElement)
        {
            item.Initialize();
        }
    }
}