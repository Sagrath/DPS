using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class HighScoreData : MonoBehaviour
{
    public HighScore score;
    

    public static HighScoreData Instance;

    public HighScoreData highScoreData;

    private string file = "score.txt";

    public Text HighScoreText;
    public Text HighScoreText1;
    public Text HighScoreText2;
    public Text HighScoreText3;
    public Text HighScoreText4;

    //public string pName;

    void Start()
    {
        GameObject hs = GameObject.Find("HighScoreData");
        HighScoreData hscs = hs.GetComponent<HighScoreData>();
        hscs.Load();
        string hsname = hscs.score.player[0];
        int hspoint = hscs.score.points[0];
        HighScoreText.text = $"* {hsname} : {hspoint}*";
        string hsname1 = hscs.score.player[1];
        int hspoint1 = hscs.score.points[1];
        HighScoreText1.text = $" {hsname1} : {hspoint1}";
        string hsname2 = hscs.score.player[2];
        int hspoint2 = hscs.score.points[2];
        HighScoreText2.text = $" {hsname2} : {hspoint2}";
        string hsname3 = hscs.score.player[3];
        int hspoint3 = hscs.score.points[3];
        HighScoreText3.text = $" {hsname3} : {hspoint3}";
        string hsname4 = hscs.score.player[4];
        int hspoint4 = hscs.score.points[4];
        HighScoreText4.text = $" {hsname4} : {hspoint4}";
    }
    private void Update()
    {
        //highScoreData.Load();
        //pName = dataManager.data.name;
    }


    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("score");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);


    }
    public void Save()
    {
        string json = JsonUtility.ToJson(score);
        WriteToFile(file, json);
        Debug.Log("data saved");
    }

    public void Load()
    {
        score = new HighScore();


        string json = ReadFromFile(file);
        JsonUtility.FromJsonOverwrite(json, score);
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
