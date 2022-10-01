using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class DataHandler : HandlerBaseModel
{
    public PlayerDataModel Player;
    public override void Initialize()
    {
        base.Initialize();

        Player = new PlayerDataModel().Load();
    }

    [EditorButton()]
    public void ClearAllData()
    {
        string[] files = Directory.GetFiles(Application.persistentDataPath, "*.dat");
        for (int i = 0; i < files.Length; i++)
        {
            File.Delete(files[i]);
        }

        PlayerPrefs.DeleteAll();

        if (Directory.GetFiles(Application.persistentDataPath, "*.dat").Length == 0)
        {
            Debug.Log("Data Clear Successed");
        }
    }

#if UNITY_EDITOR

    [EditorButton()]
    public void E_CreateNewDataModel(string DataName)
    {
        var regexItem = new Regex("^[a-zA-Z0-9 ]*$");

        if (DataName != null && !char.IsNumber(DataName.ToCharArray().ElementAt(0)) && regexItem.IsMatch(DataName))
        {
            DataName = DataName.Replace(" ", "");
            string targetPath = Application.dataPath + "/Source/Models/DataModels/" + DataName + ".cs";
            string sampleDataModelPath = Application.dataPath + "/Source/Models/DataModels/SampleDataModel.cs";
            string sampleDataModelText = File.ReadAllText(sampleDataModelPath);
            sampleDataModelText = sampleDataModelText.Replace("SampleDataModel", DataName);

            if (File.Exists(targetPath) == false)
            {
                Debug.Log("Creating DataModel: " + targetPath);
                using StreamWriter outfile =
                    new StreamWriter(targetPath);
                outfile.Write(sampleDataModelText);
            }
            else
                Debug.LogError("There is a data model with the same name!");
            AssetDatabase.Refresh();
        }
        else
        {
            Debug.LogError("Check Data Name!");
        }

    }
#endif

}

public class Data
{
    public List<BaseData> Datas;
}

public class BaseData
{

}