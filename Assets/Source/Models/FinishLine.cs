using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : ObjectModel
{
    [SerializeField] private SplineMainRoad mainRoad;
    private void Start()
    {
        var lastPoint = mainRoad.Computer.GetPointPosition(mainRoad.Computer.pointCount - 1);
        var lastSecondPoint = mainRoad.Computer.GetPointPosition(mainRoad.Computer.pointCount - 2);
        var rot = Quaternion.LookRotation(lastPoint - lastSecondPoint);
        transform.SetPositionAndRotation(lastPoint, rot);
    }
}
