using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Diagnostics;

public class LoadGameController : MonoBehaviour
{
    public GameObject list;
    public GameObject buttonPrefab;
    public RectTransform contenTrabsform;

    public void ShowSavedGamesList()
    {
        list.SetActive(true);
        string basePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        string dir = "SuwakGame";
        string fullPath = Path.Combine(basePath, dir);
        bool exists = System.IO.Directory.Exists(fullPath);
        if (!exists)
        {
            System.IO.Directory.CreateDirectory(fullPath);
        }
        DirectoryInfo d = new DirectoryInfo(fullPath);

        foreach (var file in d.GetFiles())
        {
            GameObject b = Instantiate(buttonPrefab, contenTrabsform);
            b.GetComponentsInChildren<TMP_Text>()[0].text = file.Name;
        }
    }
    void Start()
    {
        list.SetActive(false);

    }
}
