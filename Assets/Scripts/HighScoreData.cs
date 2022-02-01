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

    public string HighScore;
    public string HighScore1;
    public string HighScore2;
    public string HighScore3;
    public string HighScore4;

    //public string pName;

    void Start()
    {
        GameObject hs = GameObject.Find("HighScoreData");
        HighScoreData hscs = hs.GetComponent<HighScoreData>();
        hscs.Load();
        string hsname = hscs.score.player[0];
        int hspoint = hscs.score.points[0];
        HighScore = $"* {hsname} : {hspoint}*";
        HighScoreText.text = HighScore;       
        string hsname1 = hscs.score.player[1];
        int hspoint1 = hscs.score.points[1];
        HighScore1 = $"* {hsname1} : {hspoint1}*";
        HighScoreText1.text = HighScore1;
        string hsname2 = hscs.score.player[2];
        int hspoint2 = hscs.score.points[2];
        HighScore2 = $"* {hsname2} : {hspoint2}*";
        HighScoreText2.text = HighScore2;
        string hsname3 = hscs.score.player[3];
        int hspoint3 = hscs.score.points[3];
        HighScore3 = $"* {hsname3} : {hspoint3}*";
        HighScoreText3.text = HighScore3;
        string hsname4 = hscs.score.player[4];
        int hspoint4 = hscs.score.points[4];
        HighScore4 = $"* {hsname4} : {hspoint4}*";
        HighScoreText4.text = HighScore4;
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
