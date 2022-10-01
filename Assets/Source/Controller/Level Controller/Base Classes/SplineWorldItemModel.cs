using System;
using System.Linq;
using Dreamteck.Splines;
using UnityEditor;
using UnityEngine;

public abstract class SplineWorldItemModel : ItemModel
{
    

    [SerializeField] protected bool showSplineTracer;

    [SerializeField] protected SplineMainRoad mainRoad;

    [SerializeField] protected Vector2 offset;

    [Range(0, 1)] [SerializeField] protected double positionRange;

    public abstract void SetPositionAndOffset(double range, Vector2 offset);
    public abstract SplineItemDataModel GetData();
}