using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using System;
using UnityEngine.SceneManagement;
using System.Diagnostics;

public class LoadOnclick : MonoBehaviour
{
    public TMP_Text txt;
    public void LoadGame()
    {
        string saveName = txt.text; 
        string basePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        string dir = "SuwakGame";
        SceneManager.LoadScene(1);
        SaveManager.Load(Path.Combine(basePath, dir, saveName).ToString());
    }
}
