using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using System.IO;
using System.Diagnostics;

public class SaveGameController : MonoBehaviour
{
    public TMP_Text saveName;
    public GameObject saveAmeObject;
    private bool saveEnabled;


    // Start is called before the first frame update
    void Start()
    {
        saveAmeObject.SetActive(false);
        saveEnabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(InputSettings.Save))
        {
            if (saveEnabled)
            {
                string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                string dir = "SuwakGame";
                bool exists = System.IO.Directory.Exists(Path.Combine(path, dir));
                if (!exists)
                {
                    System.IO.Directory.CreateDirectory(Path.Combine(path, dir));
                }
                SaveManager.Save(Path.Combine(path, dir, saveName.text));
                saveAmeObject.SetActive(false);
                saveEnabled = false;
            }
            else
            {
                saveAmeObject.SetActive(true);
                string newName = DateTime.Now.ToString();
                newName = newName.Replace(":", "_");
                newName = newName.Replace(" ", "_");
                newName = newName.Replace(".", "_");
                UnityEngine.Debug.Log("newName " + newName);
                saveName.text = newName;
                saveEnabled = true;
            }
        }
    }
}
