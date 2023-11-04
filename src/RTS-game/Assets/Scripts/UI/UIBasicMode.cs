using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Linq;

// ----- enums -----
public enum Mode
{
    NORMAL,
    COMBAT,
    BUILDING
}

// ----- mechanisms -----
public class UIBasicMode : MonoBehaviour
{
    public static Mode gameMode;

    public static TMP_Text textM;
    public static TMP_Text textW;
    public static TMP_Text textS;

    public TMP_Text date;
    private DateTime startDate;
    public Transform CameraTransform;
    public RectTransform compassBarTransform;
    public RectTransform northMarkrerTransform;
    public RectTransform southMarkrerTransform;
    public RectTransform eastMarkrerTransform;
    public RectTransform westMarkrerTransform;

    private GameObject[] enemiesOnUI;
    private GameObject[] enemiesOnMap;
    public GameObject enemiesPrefab;

    // ----- storage -----
    public static void IncreaseSource(TMP_Text sourceT, String value)
    {
        sourceT.text = (int.Parse(sourceT.text) + int.Parse(value)).ToString();
    }
    public static void DecreaseSource(TMP_Text sourceT, String value)
    {
        sourceT.text = (int.Parse(sourceT.text) - int.Parse(value)).ToString();
    }
    public static void SubstractCost(String valueM, String valueW, String valueS)
    {
        DecreaseSource(textM, valueM);
        DecreaseSource(textW, valueW);
        DecreaseSource(textS, valueS);
    }
    public static bool EnoughSources(String valueM, String valueW, String valueS)
    {
        bool enough = true;
        if (int.Parse(valueM) > int.Parse(textM.text))
        {
            enough = false;
        }
        else if (int.Parse(valueW) > int.Parse(textW.text))
        {
            enough = false;
        }
        else if (int.Parse(valueS) > int.Parse(textS.text))
        {
            enough = false;
        }
        return enough;
    }
    void PrepareStorage()
    {
        textM = GameObject.FindGameObjectsWithTag("Storage")[2].GetComponentInChildren<TMP_Text>();
        textW = GameObject.FindGameObjectsWithTag("Storage")[1].GetComponentInChildren<TMP_Text>();
        textS = GameObject.FindGameObjectsWithTag("Storage")[0].GetComponentInChildren<TMP_Text>();
        textM.text = "101";
        textW.text = "102";
        textS.text = "103";
    }
    // ----- date -----
    private System.Collections.IEnumerator UpdateClock()
    {
        while (true)
        {
            this.startDate = this.startDate.Add(new System.TimeSpan(0, 0, 1, 0));
            this.date.text = this.startDate.ToString();

            yield return new WaitForSeconds(5f);
        }
    }
    void StartClock()
    {
        this.startDate = new DateTime(1500, 1, 1, 8, 0, 0);
        this.date.text = this.startDate.ToString();
        StartCoroutine(UpdateClock());
    }
    // ----- compass -----
    void SetMarkerPositionOnCompass(RectTransform markerTransform, Vector3 worldPosition)
    {
        Vector3 dirToTarget = worldPosition - this.CameraTransform.position;
        float angle = Vector2.SignedAngle(new Vector2(dirToTarget.x, dirToTarget.z), new Vector2(this.CameraTransform.transform.forward.x, this.CameraTransform.transform.forward.z));
        float compassPositionX = Mathf.Clamp(2 * angle / Camera.main.fieldOfView, -1, 1);
        if (compassPositionX == 1 || compassPositionX == (-1))
        {
            markerTransform.anchoredPosition = new Vector2(0, 100);
        }
        else
        {
            markerTransform.anchoredPosition = new Vector2(this.compassBarTransform.rect.width / 2 * compassPositionX, 0);
        }
    }
    void SetPositionOfEnemies()
    {
        foreach (var e in this.enemiesOnMap.Zip(this.enemiesOnUI, (x, y) => new { enemyOnMap = x, enemyOnUI = y }))
        {
            SetMarkerPositionOnCompass(e.enemyOnUI.GetComponent<RectTransform>(), e.enemyOnMap.transform.position);
        }
    }
    void UpdateCompass()
    {
        int farFarAway = 60000;
        int halfOfMapLength = 2300;
        int halfOfMapWidth = 4041;

        SetMarkerPositionOnCompass(this.northMarkrerTransform, new Vector3(halfOfMapLength, 0, farFarAway));
        SetMarkerPositionOnCompass(this.southMarkrerTransform, new Vector3(halfOfMapLength, 0, -farFarAway));
        SetMarkerPositionOnCompass(this.eastMarkrerTransform, new Vector3(farFarAway, 0, halfOfMapLength));
        SetMarkerPositionOnCompass(this.westMarkrerTransform, new Vector3(-farFarAway, 0, halfOfMapLength));
        SetPositionOfEnemies();
    }
    void InstantiateEnemies()
    {
        this.enemiesOnMap = GameObject.FindGameObjectsWithTag("Enemy");
        List<GameObject> tmpEnemy = new List<GameObject>();
        foreach (GameObject enemy in this.enemiesOnMap)
        {
            GameObject e = Instantiate(enemiesPrefab, this.compassBarTransform);
            tmpEnemy.Add(e);
        }
        this.enemiesOnUI = tmpEnemy.ToArray();
    }
    // ----- UI -----
    void Start()
    {
        gameMode = Mode.NORMAL;
        PrepareStorage();
        StartClock();
        InstantiateEnemies();
    }
    void Update()
    {
        UpdateCompass();
    }
}
