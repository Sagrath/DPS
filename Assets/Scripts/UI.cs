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

    public GameObject canvas;
    public GameObject canvasUser;
    public GameObject canvasHigh;

    public InputField playerName;

    
    public Text playerNameHello;
    // Start is called before the first frame update
    void Start()
    {
        dataManager.Load();
        
        playerNameHello.text = "Hello" + dataManager.data.name + "!";   
        

    }

    // Update is called once per frame
    void Update()
    {
        dataManager.Load();
        
        playerNameHello.text = "Hello " + dataManager.data.name + "!";
    }
    public void UserNameScene()
    {
        canvas.SetActive(false);
        canvasUser.SetActive(true);
    }
    public void HighScore()
    {
        canvas.SetActive(false);        
        canvasHigh.SetActive(true);
    }

    public void BackonManu()
    {
        canvas.SetActive(true);
        canvasHigh.SetActive(false);
        canvasUser.SetActive(false);


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
    public void ChangeName(string text)
    {
        Debug.Log(playerName.text);
        dataManager.data.name = playerName.text;
    }
    public void ClickSave()
    {
        dataManager.Save();
    }

}
