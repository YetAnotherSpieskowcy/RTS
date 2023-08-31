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
public class UI_Mechanisms : MonoBehaviour
{
    public Mode gameMode = Mode.NORMAL;

    public TMP_Text textF;
    public TMP_Text textW;
    public TMP_Text textS;

    public TMP_Text date;
    private DateTime startDate;
    
    public Transform CameraTransform;
    public RectTransform compassBarTransform;
    public RectTransform northMarkrerTransform;
    public RectTransform southMarkrerTransform;
    public RectTransform eastMarkrerTransform;
    public RectTransform westMarkrerTransform;

    private GameObject[]  enemiesOnUI;
    private GameObject[]  enemiesOnMap;
    public GameObject enemiesPrefab;

    /*
    DONE clock
    TODO compass
    DONE storage
    TODO characters
    TODO buildings
    TODO key interrupts
    TODO comments
    TODO possible commands
    // ----- storage -----*/
    void IncreaseSource(TMP_Text sourceT, int value)
    {
        int newI = int.Parse(sourceT.text) + value;
        sourceT.text = newI.ToString();
    }
    void DecreaseSource(TMP_Text sourceT, int value)
    {
        sourceT.text = (int.Parse(sourceT.text) + value).ToString();
    }

    // ----- date -----
    private System.Collections.IEnumerator UpdateClock()
    {
        while (true)
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
        if (compassPositionX == 1 || compassPositionX == (-1))
        {
            markerTransform.anchoredPosition = new Vector2(0, 100);
        }
        else
        {
            markerTransform.anchoredPosition = new Vector2(compassBarTransform.rect.width / 2 * compassPositionX, 0);
        }
    }
    void SetPositionOfEnemies()
    {
        foreach(var e in enemiesOnMap.Zip(enemiesOnUI, (x, y) => new { enemyOnMap = x, enemyOnUI = y }))
        {
            SetMarkerPosition(e.enemyOnUI.GetComponent<RectTransform>(), e.enemyOnMap.transform.position);
        }
    }

    void UpdateCompass()
    {
        int farFarAway = 60000;
        int halfOfMapLength = 2300;
        int halfOfMapWidth = 4041;

        SetMarkerPosition(northMarkrerTransform, new Vector3(halfOfMapLength, 0, farFarAway));
        SetMarkerPosition(southMarkrerTransform, new Vector3(halfOfMapLength, 0, -farFarAway));
        SetMarkerPosition(eastMarkrerTransform, new Vector3(farFarAway, 0, halfOfMapLength));
        SetMarkerPosition(westMarkrerTransform, new Vector3(-farFarAway, 0, halfOfMapLength));
        
        SetPositionOfEnemies();
    }

    // ----- UI -----
    void Start()
    {
        startDate = new DateTime(1500, 1, 1, 8, 0, 0);
        date.text = startDate.ToString();
        StartCoroutine(UpdateClock());

        enemiesOnMap = GameObject.FindGameObjectsWithTag("Enemy");
        List<GameObject> tmp =  new List<GameObject>();
        foreach (GameObject enemy in enemiesOnMap)
        {
            GameObject e = Instantiate(enemiesPrefab, compassBarTransform);
            tmp.Add(e);
        }
        enemiesOnUI = tmp.ToArray();
    }

    void Update()
    {
        UpdateCompass();
    }
}
