using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataManager : MonoBehaviour
{
    public  UserName data;

    public static DataManager Instance;

    public  DataManager dataManager;

    private string file = "name.txt";

    public string pName;

    void Start()
    {
        
    }
    private void Update()
    {
        dataManager.Load();
        pName = dataManager.data.name;
    }


    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("data");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);


    }
    public void Save()
    {
        string json = JsonUtility.ToJson(data);
        WriteToFile(file, json);
        Debug.Log("data saved");
    }

    public void Load()
    {
        data = new UserName();
        string json = ReadFromFile(file);
        JsonUtility.FromJsonOverwrite(json, data);
    }
    private void WriteToFile(string fileName, string json)
    {
        string path = GetFilePath(fileName);
        FileStream fileStream = new FileStream(path, FileMode.Create);

        using (StreamWriter writer = new StreamWriter(fileStream))
        {
            writer.Write(json);

        }
    }
    private string ReadFromFile(string fileName)
    {
        string path = GetFilePath(fileName);
        if (File.Exists(path))
        {
            using (StreamReader reader = new StreamReader(path))
            {
                string json = reader.ReadToEnd();
                return json;
            }
        }
        else
            Debug.LogWarning("File not Found");
        return "";
    }
    private string GetFilePath(string fileName)
    {
        return Application.persistentDataPath + "/" + fileName;
    }
}
