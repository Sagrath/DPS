using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine.UI;
using System.IO;

public class UI : MonoBehaviour
{
    public DataManager dataManager;

    public Text playerName;
    // Start is called before the first frame update
    void Start()
    {
        dataManager.Load();
        playerName.text = "Hello" + dataManager.data.name + "!";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void UserNameScene()
    {
        SceneManager.LoadScene(3);
    }
    public void HighScore()
    {
        SceneManager.LoadScene(2);
    }
    public void StartNew()
    {
        SceneManager.LoadScene(1);
    }
    public void Exit()
    {

#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit(); // original code to quit Unity player
#endif
    }

}
