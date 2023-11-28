using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quicksave : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(InputSettings.Quicksave))
        {
            SaveManager.Save("quicksave.pb");
        }
        if (Input.GetKeyDown(InputSettings.Quickload))
        {
            SaveManager.Load("quicksave.pb");
        }
    }
}