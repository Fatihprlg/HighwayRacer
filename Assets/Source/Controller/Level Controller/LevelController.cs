using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using Newtonsoft.Json;
using Object = UnityEngine.Object;
#if UNITY_EDITOR
using UnityEditor;
#endif


public class LevelController : ControllerBaseModel
{
    public bool initalizeOnAwake;

    public LevelModel ActiveLevel;
    [SerializeField] List<Object> levels;
    [SerializeField] public LevelAdapter levelAdapter;
    [SerializeField] private RoadManager roadManager;
    [SerializeField] private Action whenLevelLoad;

    private void Awake()
    {
        if (initalizeOnAwake)
            LoadLevel();
    }

    public override void Initialize()
    {
        if (!initalizeOnAwake)
            LoadLevel();
    }

    public void LoadLevel()
    {
        roadManager.LoadRoadWithIndex(PlayerDataModel.Data.LevelIndex,
            () => LoadLevelHelper(PlayerDataModel.Data.LevelIndex));
    }

    public void NextLevel()
    {
        PlayerDataModel.Data.Level++;
        PlayerDataModel.Data.LevelIndex = PlayerDataModel.Data.LevelIndex + 1 < levels.Count
            ? PlayerDataModel.Data.LevelIndex + 1
            : 0;
        PlayerDataModel.Data.Save();
    }

#if UNITY_EDITOR

    [EditorButton]
    public void E_SaveLevel()
    {
        roadManager.E_SaveRoad();
        LevelModel level = new LevelModel();

        level.name = "Level " + levels.Count.ToString();
        level.levelIndex = levels.Count;
        var path = $"Assets/GameAssets/Levels/{level.name}.json";

        SaveWorldItems(level, path);
        var asset = AssetDatabase.LoadAssetAtPath(path, typeof(Object));
        levels.Add(asset);
    }

    public void E_LoadLevel(int levelIndex)
    {
        if (ActiveLevel != null)
            E_ClearScene();
        roadManager.LoadRoadWithIndex(levelIndex, null);
        LoadLevelHelper(levelIndex);
    }

    public void E_OverrideLevel()
    {
        var path = $"Assets/GameAssets/Levels/Level {ActiveLevel.levelIndex}.json";
        var jsonString = File.ReadAllText(path);
        var level = JsonHelper.LoadJson<LevelModel>(jsonString);
        roadManager.E_OverrideRoad();
        SaveWorldItems(level, path);
    }


    public void E_ClearScene()
    {
        var items = FindObjectsOfType<ItemModel>();
        foreach (var item in items)
        {
            item.gameObject.SetActive(false);
        }

        roadManager.E_ResetRoad();

        ActiveLevel = null;
    }

#endif

    #region UTILS

#if UNITY_EDITOR
    private void SaveWorldItems(LevelModel level, string path)
    {
        var worldItemModels = FindObjectsOfType<ItemModel>().ToList();
        levelAdapter.SaveAll(level, worldItemModels);


        E_ClearScene();


        JsonHelper.SaveJson(level, path);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
#endif


    private void LoadLevelHelper(int levelIndex)
    {
        var level = JsonHelper.LoadJson<LevelModel>(levels[levelIndex].ToString());
        ActiveLevel = level;
        if (levels.Count <= levelIndex)
        {
            levelIndex = 0;
        }

        levelAdapter.LoadLevel(ActiveLevel);
        whenLevelLoad?.Invoke();
    }

    #endregion

#if UNITY_EDITOR

    protected void Reset()
    {
        base.Reset();
        levels = new List<Object>();
        levels.Clear();


        roadManager = gameObject.AddComponent<SplineRoadManager>();


        levelAdapter = gameObject.AddComponent<SplineLevelAdapter>();


        var path = "Assets/GameAssets/Levels";
        string[] roadDatas = Directory.GetFiles(path, "*.json", SearchOption.TopDirectoryOnly);
        foreach (string matFile in roadDatas)
        {
            string assetPath = matFile.Replace('\\', '/');  
            Object roadJson = AssetDatabase.LoadAssetAtPath(assetPath, typeof(Object));
            print(assetPath);
            levels.Add(roadJson);
        }
    }
#endif
}
#if UNITY_EDITOR
[CustomEditor(typeof(LevelController))]
public class LevelControllerEditor : Editor
{
    private LevelController levelController;
    private int editorLevelIndex;

    private Texture2D clear, save, load, cd;

    private GUIContent saveContent, loadContent, clearContent, overrideContent;

    private void OnEnable()
    {
        save = EditorGUIUtility.Load("Assets/Icons/save.png") as Texture2D;
        saveContent = new GUIContent();
        saveContent.image = save;
        saveContent.text = "Save Level";

        load = EditorGUIUtility.Load("Assets/Icons/load.png") as Texture2D;
        loadContent = new GUIContent();
        loadContent.image = load;
        loadContent.text = "Load Level";

        cd = EditorGUIUtility.Load("Assets/Icons/cd.png") as Texture2D;
        overrideContent = new GUIContent();
        overrideContent.image = cd;
        overrideContent.text = "Override Level";

        clear = EditorGUIUtility.Load("Assets/Icons/clear.png") as Texture2D;
        clearContent = new GUIContent();
        clearContent.image = clear;
        clearContent.text = "Clear Scene";
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        EditorUtils.DrawUILine(Color.white);

        levelController = target as LevelController;

        EditorGUILayout.BeginVertical();

        if (GUILayout.Button(saveContent))
        {
            levelController.E_SaveLevel();
        }

        if (GUILayout.Button(overrideContent))
        {
            levelController.E_OverrideLevel();
        }

        editorLevelIndex = EditorGUILayout.IntField("Loaded Level Index", editorLevelIndex);
        if (GUILayout.Button(loadContent))
        {
            levelController.E_LoadLevel(editorLevelIndex);
        }

        if (GUILayout.Button(clearContent))
        {
            levelController.E_ClearScene();
        }


        EditorGUILayout.EndVertical();
    }
}

#endif
