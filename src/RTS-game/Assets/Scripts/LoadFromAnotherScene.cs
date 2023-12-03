using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class LoadFromAnotherScene : MonoBehaviour
{
    public static string NameOfSaveFile_S;
    private bool canLoad;

    // Start is called before the first frame update
    void Start()
    {
        canLoad = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetSceneByBuildIndex(1).IsValid() && canLoad)
        {
            if (NameOfSaveFile_S != null)
            {
                string basePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                string dir = "SuwakGame";
                SaveManager.Load(Path.Combine(basePath, dir, NameOfSaveFile_S).ToString());
                canLoad = false;
            }
        }
    }
}
