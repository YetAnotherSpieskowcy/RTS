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
    private bool canLoad = false;
    public void LoadGame()
    {
        SceneManager.LoadScene(1);
        canLoad = true;
    }
    void Update()
    {
        if (SceneManager.GetSceneByBuildIndex(1).IsValid() && canLoad)
        {
            string saveName = txt.text;
            string basePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string dir = "SuwakGame";
            saveName = txt.text; saveName = txt.text; saveName = txt.text; saveName = txt.text; saveName = txt.text;
            SaveManager.Load(Path.Combine(basePath, dir, saveName).ToString());
            canLoad = false;
            UnityEngine.Debug.Log("spos");
        }
    }
}
