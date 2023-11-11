using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogInfoMediator
{
    private GameObject dialoginfo;
    public DialogInfoMediator()
    {
        dialoginfo = GameObject.FindGameObjectsWithTag("DialogInfo")[0];
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