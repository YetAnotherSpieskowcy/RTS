using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    public void ChangeScene()
    {
        SceneManager.LoadScene(1); // 1 --> SampleScene
    }
    public void Exit()
    {
        Application.Quit();
    }
}
