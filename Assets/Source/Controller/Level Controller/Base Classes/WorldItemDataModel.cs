using System;
using UnityEngine;

[Serializable]
public class SplineItemDataModel
{
    public int Id;
    public Vector2 offset;
    public double percentage;
}

public enum WorldItemType
{
    Enemy,
    Colletcable
}