using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System.IO;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    public Text HighScoreText;
    public GameObject GameOverText;

    private bool m_Started = false;
    public int m_Points;

    private bool m_GameOver = false;
    private bool m_ScoreRank = false;

    public string playName;

    public static MainManager Instance;

    //public HighScoreData highScoreData;



    // Start is called before the first frame update
    void Start()
    {
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);

        int[] pointCountArray = new[] { 1, 1, 2, 2, 5, 5 };
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }

        GameObject go = GameObject.Find("DataManager");
        DataManager cs = go.GetComponent<DataManager>();
        cs.Load();
        string playName = cs.pName;
        ScoreText.text = $"{playName} Score : 0";

        GameObject hs = GameObject.Find("HighScoreData");
        HighScoreData hscs = hs.GetComponent<HighScoreData>();
        hscs.Load();

        string hsname = hscs.score.player[0];
        int hspoint = hscs.score.points[0];
        HighScoreText.text = $"Best Score : {hsname} : {hspoint}";

    }

    private void Update()
    {
        GameObject go = GameObject.Find("DataManager");
        DataManager cs = go.GetComponent<DataManager>();
        cs.Load();
        string playName = cs.pName;
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    void AddPoint(int point)
    {
        GameObject go = GameObject.Find("DataManager");
        DataManager cs = go.GetComponent<DataManager>();
        cs.Load();
        string playName = cs.pName;
        m_Points += point;
        ScoreText.text = $"{playName} Score : {m_Points}";
    }

    public void GameOver()
    {
        m_ScoreRank = true;
        m_GameOver = true;
        GameOverText.SetActive(true);
        StartCoroutine(GameOverBlink());
        GameObject hs = GameObject.Find("HighScoreData");
        HighScoreData cs = hs.GetComponent<HighScoreData>();
        cs.Load();
        
        GameObject go = GameObject.Find("DataManager");
        DataManager pn = go.GetComponent<DataManager>();
        pn.Load();
        string playName = pn.pName;
        if (m_ScoreRank)
        {
            for (int i = 0; i <= 4; i++)
            {
                if (cs.score.points[i] < m_Points)
                {
                    for (int j = 4; j > i; j--)
                    {
                        cs.score.player[j + 1] = cs.score.player[j];
                        cs.score.points[j + 1] = cs.score.points[j];
                    }
                    cs.score.player[i] = playName;
                    cs.score.points[i] = m_Points;
                    cs.Save();
                    m_ScoreRank = false;

                }
            }
        }
        
    }
    public void BackonManu()
    {
        SceneManager.LoadScene(0);
    }
    public void Exit()
    {

#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit(); // original code to quit Unity player
#endif
    }


    public void NoName()
    {
        //if no name display enter name text
    }

    private IEnumerator GameOverBlink()
    {
        while (m_GameOver)
        {
            if (GameOverText.activeInHierarchy)
            {
                GameOverText.SetActive(false);
            }
            else
            {
                GameOverText.SetActive(true);
            }
            yield return new WaitForSeconds(0.5f);

        }
        yield break;
    }
}
