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
    //public Storage storage;
    public TMP_Text textF;
    public TMP_Text textW;
    public TMP_Text textS;

    public TMP_Text date;
    private DateTime startDate;
    private bool isRunning;
    public bool clockTrigger;

    public Image compassArrow;
    public float angleRadians;
    

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

            //Debug.Log("date: " + startDate.ToString());

            yield return new WaitForSeconds(5f);
        }
    }
    // ----- compass -----
    void UpdateCompass()
    {
        float pi = (float)Math.PI;
        if (angleRadians >= 0 && angleRadians < pi)
        {
            float position = 245 + (245 * angleRadians) / pi;
            compassArrow.rectTransform.anchoredPosition = new Vector2(position, 0);
        }
        else if(angleRadians >= pi && angleRadians < 2 * pi)
        {
            float position = (245 * (angleRadians - pi)) / pi;
            compassArrow.rectTransform.anchoredPosition = new Vector2(position, 0);
        }
    }
    // ----- UI -----
    void Start()
    {
        startDate = new DateTime(1500, 1, 1, 8, 0, 0);
        date.text = startDate.ToString();
        isRunning = false;
        clockTrigger = false;
        angleRadians = 0;
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
        //IncreaseFuands();
        //textW.text = storage.funds.ToString();

        UpdateCompass();
    }
}
