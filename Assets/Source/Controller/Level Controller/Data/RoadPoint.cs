using System;
using UnityEngine;

[Serializable]
public class RoadPoint
{
     public RoadPoint(Vector3 pos, Vector3 tan, Vector3 tan2, Vector3 nor, float s, Color col)
     {
         this.pos = pos;
         this.tan = tan;
         this.tan2 = tan2;
         this.nor = nor;
         this.s = s;
         this.col = col;
     }

    public Vector3 pos;
    public Vector3 tan;
    public Vector3 tan2;
    public Vector3 nor;
    public float s;
    public Color col;
}