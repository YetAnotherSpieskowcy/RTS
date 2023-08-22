using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;



// ----- enums -----
public enum Mode
{
    NORMAL,
    COMBAT,
    BUILDING
}

// ----- structuress -----
// public struct Storage
// {
//     public int funds;
//     public int wood;
//     public int stones;
// }

// ----- mechanisms -----
public class UI_Mechanisms : MonoBehaviour
{
    public Mode gameMode = Mode.NORMAL;
    public TMP_Text textF;
    public TMP_Text textW;
    public TMP_Text textS;

    public TMP_Text date;
    private DateTime startDate;
    private bool isRunning;
    public bool clockTrigger;
    
    public Transform CameraTransform;
    public RectTransform compassBarTransform;
    public RectTransform northMarkrerTransform;
    public RectTransform southMarkrerTransform;
    public RectTransform eastMarkrerTransform;
    public RectTransform westMarkrerTransform;

    public RectTransform enemyOneMarkrerTransform;
    public RectTransform enemyTwoMarkrerTransform;
    public Transform enemyOneTransform; 
    public Transform enemyTwoTransform; 

    //DONE clock
    //TODO compass
    //DONE storage
    //TODO characters
    //TODO buildings
    //TODO key interrupts
    //TODO comments
    //TODO possible commands

    // ----- storage -----
    void IncreaseSource(TMP_Text sourceT, int value)
    {
        int newI = int.Parse(sourceT.text) + value;
        sourceT.text = newI.ToString();
    }
    void DectraseSource(TMP_Text sourceT, int value)
    {
        sourceT.text = (int.Parse(sourceT.text) + value).ToString();
    }

    // ----- date -----
    public void StartClock()
    {
        isRunning = true;
        clockTrigger = false;
        StartCoroutine(UpdateClock());
    }

    public void StopClock()
    {
        isRunning = false;
        clockTrigger = false;
        StopCoroutine(UpdateClock());
    }

    private System.Collections.IEnumerator UpdateClock()
    {
        while (isRunning)
        {
            startDate = startDate.Add(new System.TimeSpan(0, 0, 1, 0));
            date.text = startDate.ToString();

            yield return new WaitForSeconds(5f);
        }
    }

    // ----- compass -----
    void SetMarkerPosition(RectTransform markerTransform, Vector3 worldPosition)
    {
        Vector3 dirToTarget = worldPosition - CameraTransform.position;
        float angle = Vector2.SignedAngle(new Vector2(dirToTarget.x, dirToTarget.z), new Vector2(CameraTransform.transform.forward.x, CameraTransform.transform.forward.z));
        float compassPositionX = Mathf.Clamp(2 * angle / Camera.main.fieldOfView, -1, 1);
        if (compassPositionX == 1 || compassPositionX == (-1) )
        {
            markerTransform.anchoredPosition = new Vector2(0, 100);
        }
        else 
        {
            markerTransform.anchoredPosition = new Vector2(compassBarTransform.rect.width / 2 * compassPositionX, 0);
        }
    }

    void UpdateCompass()
    {
        SetMarkerPosition(northMarkrerTransform, new Vector3(4041, 0, 60000));
        SetMarkerPosition(southMarkrerTransform, new Vector3(4041, 0, -60000));
        SetMarkerPosition(eastMarkrerTransform, new Vector3(60000, 0, 2300));
        SetMarkerPosition(westMarkrerTransform, new Vector3(-60000, 0, 2300));

        SetMarkerPosition(enemyOneMarkrerTransform, enemyOneTransform.position);
        SetMarkerPosition(enemyTwoMarkrerTransform, enemyTwoTransform.position);
    }

    // ----- UI -----
    void Start()
    {
        startDate = new DateTime(1500, 1, 1, 8, 0, 0);
        date.text = startDate.ToString();
        isRunning = false;
        clockTrigger = false;
    }

    void Update()
    {
        if (clockTrigger)
        {
            if (isRunning)
            {
                StopClock();
                Debug.Log("Clock Stoped");
            }
            else
            {
                StartClock();
                Debug.Log("Clock Started");
            }
        }

        UpdateCompass();

    }
}
