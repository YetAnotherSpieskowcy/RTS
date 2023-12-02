using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogInfoController
{
    private GameObject dialoginfo;
    public DialogInfoController()
    {
        dialoginfo = GameObject.Find("InfoDialog");
        dialoginfo.SetActive(false);
    }
    public void SetDaialogInfoVisible(bool converstion)
    {
        TMP_Text txt = dialoginfo.GetComponentInChildren<TMP_Text>();
        if (converstion)
        {
            txt.text = "Press E to start conversation.";
        }
        else
        {
            txt.text = "Press E to pick up resource.";
        }
        dialoginfo.SetActive(true);
    }
    public void SetDaialogInfoInVisible()
    {
        dialoginfo.SetActive(false);
    }
}
