using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine.UI;
using System.IO;

public class MenuUIHandler : MonoBehaviour
{
    public InputField playerName;

    public DataManager dataManager;
    // Start is called before the first frame update
    void Start()
    {
        dataManager.Load();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ChangeName(string text)
    {
        Debug.Log(playerName.text);
        dataManager.data.name = text;
    }
    public void ClickSave()
    {
        dataManager.Save();
    }
    public void BackonManu()
    {
        SceneManager.LoadScene(0);
    }




}
