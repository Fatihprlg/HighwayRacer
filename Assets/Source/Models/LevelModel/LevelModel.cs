using System.Collections;
using System.Collections.Generic;
using Dreamteck.Splines;
using UnityEngine;

[System.Serializable]
public class LevelModel
{
    public string name;
    public int levelIndex;

    public List<SplineItemDataModel> Enemies = new List<SplineItemDataModel>();
    public List<SplineItemDataModel> Collectables = new List<SplineItemDataModel>();
}
