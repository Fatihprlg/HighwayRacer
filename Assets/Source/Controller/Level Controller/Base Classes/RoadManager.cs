using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;


[DisallowMultipleComponent]
public abstract class RoadManager : MonoBehaviour
{
    [SerializeField] public List<Object> roadLevels;
    
    #if UNITY_EDITOR
    public abstract void E_SaveRoad();
    public abstract void E_OverrideRoad();
    public abstract void E_ResetRoad();
    #endif
    public abstract void LoadRoadWithIndex(int index,Action onRebuild);
}