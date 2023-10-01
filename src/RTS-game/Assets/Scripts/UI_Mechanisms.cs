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

    private GameObject[] enemiesOnUI;
    private GameObject[] enemiesOnMap;
    public GameObject enemiesPrefab;

    private int numOfBuildings;
    private int selectedBuilding;
    public RectTransform buildingBackgroundTransform;

    private RectTransform[] buildingsOnUI;
    private RectTransform[] buildingsSelectedOnUI;
    public RectTransform building1SelectedTransform;
    public RectTransform building1Transform;
    public RectTransform building2SelectedTransform;
    public RectTransform building2Transform;

    // ----- storage -----
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
            markerTransform.anchoredPosition = new Vector2 (this.compassBarTransform.rect.width / 2 * compassPositionX, 0);
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
    // ----- building mode -----
    void UpdateBuildingMode()
    {
        int startX = 40;
        int spacing = 100;
        Vector2 unvisible = new Vector2(0, 1000);
        for(int i = 0; i < buildingsOnUI.Length; i++)
        {
            if(i == selectedBuilding - 1)
            {
                buildingsOnUI[i].anchoredPosition = unvisible;
                buildingsSelectedOnUI[i].anchoredPosition = new Vector2(startX + i * spacing, 0);
            }
            else
            {
                buildingsOnUI[i].anchoredPosition = new Vector2(startX + i * spacing, 0);
                buildingsSelectedOnUI[i].anchoredPosition = unvisible;
            }
        }
    }
    void PrepareBuildingsInfo()
    {
        List<RectTransform> tmpBuild = new List<RectTransform>();
        tmpBuild.Add(building1Transform);
        tmpBuild.Add(building2Transform);
        this.buildingsOnUI = tmpBuild.ToArray();
        tmpBuild.Clear();
        tmpBuild.Add(building1SelectedTransform);
        tmpBuild.Add(building2SelectedTransform);
        this.buildingsSelectedOnUI = tmpBuild.ToArray();
        this.numOfBuildings = buildingsSelectedOnUI.Length;
        this.selectedBuilding = numOfBuildings;
    }
    void ClearUIAfterBuildingMode()
    {
        Vector2 unvisible = new Vector2(0, 1000);
        for(int i = 0; i < buildingsOnUI.Length; i++)
        {
        buildingsOnUI[i].anchoredPosition = unvisible;
        buildingsSelectedOnUI[i].anchoredPosition = unvisible;
        }
    }
    // ----- modes -----
    void UpdateMode()
    {
        if (Input.GetKeyDown(InputSettings.BuildingModeController))
        {
            if(this.gameMode == Mode.NORMAL)
            {
                this.selectedBuilding = numOfBuildings;
            }

            this.selectedBuilding++;
            if(this.selectedBuilding > numOfBuildings)
            {
                this.selectedBuilding = 1;
            }

            this.gameMode = Mode.BUILDING;
        }
        else if (Input.GetKeyDown(InputSettings.ExitBuildingMode))
        {
            if(this.gameMode == Mode.BUILDING)
            {
                ClearUIAfterBuildingMode();
            }
            this.gameMode = Mode.NORMAL;
        }
    }
    // ----- UI -----
    void Start()
    {
        StartClock()
        InstantiateEnemies();
        PrepareBuildingsInfo()
    }
    void Update()
    {
        UpdateCompass();
        UpdateMode();
        if(this.gameMode == Mode.BUILDING)
        {
            UpdateBuildingMode();
        }
    }
}
