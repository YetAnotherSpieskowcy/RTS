using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogInfoController
{
    private GameObject dialoginfo;
    public DialogInfoController()
    {
        dialoginfo = GameObject.Find("InfoDialog");
        dialoginfo.SetActive(false);
    }
    public void SetDaialogInfoVisible()
    {
        dialoginfo.SetActive(true);
    }
    public void SetDaialogInfoInVisible()
    {
        dialoginfo.SetActive(false);
    }
}
