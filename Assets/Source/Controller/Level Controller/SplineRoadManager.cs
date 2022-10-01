using System;
using System.Collections;
using System.IO;
using System.Linq;
using Dreamteck.Splines;

#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using Object = UnityEngine.Object;


public class SplineRoadManager : RoadManager
{
    public SplineComputer SplineComputer => splineComputer;
    [SerializeField] private SplineComputer splineComputer;
    [SerializeField] private RoadData currentRoadData;


    public override void E_SaveRoad()
    {
        RoadData roadData = new RoadData();
        roadData.roadIndex = roadLevels.Count - 1;
        var points = splineComputer.GetPoints();
        roadData.roadPoints = points
            .Select(r => new RoadPoint(r.position, r.tangent, r.tangent2, r.normal, r.size, r.color)).ToArray();

        var path = $"Assets/GameAssets/Roads/Road {roadLevels.Count}.json";
        JsonHelper.SaveJson(roadData, path);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        var asset = AssetDatabase.LoadAssetAtPath(path, typeof(Object));
        roadLevels.Add(asset);
    }

    public override void E_OverrideRoad()
    {
        var points = splineComputer.GetPoints();
        currentRoadData.roadPoints = points
            .Select(r => new RoadPoint(r.position, r.tangent, r.tangent2, r.normal, r.size, r.color)).ToArray();

        var path = $"Assets/GameAssets/Roads/Road {currentRoadData.roadIndex}.json";
        JsonHelper.SaveJson(currentRoadData, path);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

    public override void E_ResetRoad()
    {
        splineComputer.SetPoints(new SplinePoint[0]);
    }

    public override void LoadRoadWithIndex(int index, Action onRebuild)
    {
        var level = JsonHelper.LoadJson<RoadData>(roadLevels[index].ToString());
        splineComputer.SetPoints(level.roadPoints.Select(s => new SplinePoint(s.pos, s.tan, s.tan2, s.nor, s.s, s.col))
            .ToArray());
        if (onRebuild != null)
            splineComputer.onRebuild += () => onRebuild?.Invoke();
        splineComputer.Rebuild(true);
        currentRoadData = level;
    }

#if UNITY_EDITOR
    private void Reset()
    {
        roadLevels = new System.Collections.Generic.List<Object>();
        roadLevels.Clear();
        splineComputer = GameObject.FindObjectOfType<SplineMainRoad>().Computer;


        var path = "Assets/GameAssets/Roads";

        string[] roadDatas = Directory.GetFiles(path, "*.json", SearchOption.TopDirectoryOnly);
        foreach (string matFile in roadDatas)
        {
            string assetPath = matFile.Replace('\\', '/');
            Object roadJson = AssetDatabase.LoadAssetAtPath(assetPath, typeof(Object));
            print(assetPath);
            roadLevels.Add(roadJson);
        }
    }
#endif
}