using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

[Serializable]
public class DataModel
{
    protected virtual void Save(object data)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/" + GetType().Name + ".dat");
        bf.Serialize(file, data);
        file.Close();
    }

    public virtual void Delete()
    {
        if (File.Exists(Application.persistentDataPath + "/" + GetType().Name + ".dat"))
        {
            File.Delete(Application.persistentDataPath + "/" + GetType().Name + ".dat");
        }
    }

    public bool DataExists()
    {
        return File.Exists(Application.persistentDataPath + "/" + GetType().Name + ".dat");
    }

    protected object LoadData()
    {
        string path = Application.persistentDataPath + "/" + GetType().Name + ".dat";
        if (File.Exists(path))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(path, FileMode.Open);
            object data = bf.Deserialize(file);
            return data;
        }
        else
        {
            return null;
        }
    }
}