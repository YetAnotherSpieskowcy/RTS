using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Modal : MonoBehaviour
{
    public GameObject ui;
    public TMP_Text text;
    void Awake()
    {
        ui.SetActive(false);
    }
    IEnumerator DisplayModal()
    {
        ui.SetActive(true);
        yield return new WaitForSeconds(5);
        ui.SetActive(false);

    }

    public void Show(string msg)
    {
        text.text = msg;
        StartCoroutine(DisplayModal());
    }
}
