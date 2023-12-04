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
    private LoadFromAnotherScene load;
    public void LoadGame()
    {
        LoadFromAnotherScene.NameOfSaveFile_S = txt.text;
        SceneManager.LoadScene(1);
    }
}
