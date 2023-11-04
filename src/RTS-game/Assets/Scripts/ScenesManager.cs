using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManager;

public class ScenesManager : MonoBehaviour
{
    public void ChangeScene()
    {
        SceneManager.LoadScene(1); // 1 --> SampleScene
    }
}
