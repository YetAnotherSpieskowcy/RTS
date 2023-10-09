using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Linq;



// ----- structures -----
public struct buildingOnUI
{
    public GameObject inactive;
    public GameObject selected;
    public String costM;
    public String costW;
    public String costS;
}

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

    public TMP_Text textM;
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
    public List<Texture> buildingTextureSources;
    public GameObject buildingPrefab;
    public GameObject selectedBuildingPrefab;
    private List<buildingOnUI> buildingsOnUI;

    //private RectTransform[] buildingsOnUI;
    private RectTransform[] buildingsSelectedOnUI;
    public RectTransform building1SelectedTransform;
    public RectTransform building1Transform;
    public RectTransform building2SelectedTransform;
    public RectTransform building2Transform;
    public RectTransform building3SelectedTransform;
    public RectTransform building3Transform;

    // ----- storage -----
    void IncreaseSource(TMP_Text sourceT, String value)
    {
        sourceT.text = (int.Parse(sourceT.text) + int.Parse(value)).ToString();
    }
    void DecreaseSource(TMP_Text sourceT, String value)
    {
        sourceT.text = (int.Parse(sourceT.text) - int.Parse(value)).ToString();
    }
    void PrepareStorage()
    {
        textM.text = "100";
        textW.text = "100";
        textS.text = "100";
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
    void PrepareBuildingsInfo()
    {
        this.buildingsOnUI = new List<buildingOnUI>();
        //here i will probably change buildingTextureSources to list of ScriptableObjects for buildiings?
        foreach(Texture bts in this.buildingTextureSources )
        {
            //here will probably be estracting texture from ScriptableObjects? Texture bts = scriptObj.texture
            buildingOnUI new_bou = new buildingOnUI();
            //here will probably be estracting info from ScriptableObject? new_bou.costX = scriptObj.costX
            new_bou.costM = "30";
            new_bou.costW = "20";
            new_bou.costS = "10";

            GameObject b = Instantiate(buildingPrefab, this.buildingBackgroundTransform);
            b.GetComponentInChildren<RawImage>().texture = bts;
            b.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 1000);
            new_bou.inactive = b;

            GameObject s = Instantiate(selectedBuildingPrefab, this.buildingBackgroundTransform);
            s.GetComponentInChildren<RawImage>().texture = bts;
            s.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 1000);
            s.GetComponentsInChildren<TMP_Text>()[0].text = new_bou.costM;
            s.GetComponentsInChildren<TMP_Text>()[1].text = new_bou.costW;
            s.GetComponentsInChildren<TMP_Text>()[2].text = new_bou.costS;
            new_bou.selected = s;

            buildingsOnUI.Add(new_bou);
        }
        this.numOfBuildings = buildingsOnUI.Count;
        this.selectedBuilding = numOfBuildings;
    }
    void ClearUIAfterBuildingMode()
    {
        Vector2 unvisible = new Vector2(0, 1000);
        for(int i = 0; i < buildingsOnUI.Count; i++)
        {
            buildingsOnUI[i].inactive.GetComponent<RectTransform>().anchoredPosition = unvisible;
            buildingsOnUI[i].selected.GetComponent<RectTransform>().anchoredPosition = unvisible;
        }
    }
    bool EnoughSources(buildingOnUI bou)
    {
        bool enough = true;
        if(int.Parse(bou.costM) > int.Parse(textM.text))
        {
            enough = false;
        }
        else if(int.Parse(bou.costW) > int.Parse(textW.text))
        {
            enough = false;
        }
        else if(int.Parse(bou.costS) > int.Parse(textS.text))
        {
            enough = false;
        }
        return enough;
    }
    void BuyBuilding()
    {
        buildingOnUI bou = buildingsOnUI[selectedBuilding-1];
        if(EnoughSources(bou))
        {
            DecreaseSource(textM, bou.costM);
            DecreaseSource(textW, bou.costW);
            DecreaseSource(textS, bou.costS);
        }
        else
        {
            Debug.Log("ur too poor sry");
        }
    }
    void UpdateBuildingMode()
    {
        int startX = 150;
        int spacing = 150;
        Vector2 unvisible = new Vector2(0, 1000);
        for(int i = 0; i < buildingsOnUI.Count; i++)
        {
            buildingOnUI bou = buildingsOnUI[i];
            if(i == selectedBuilding - 1)
            {
                bou.inactive.GetComponent<RectTransform>().anchoredPosition = unvisible;
                bou.selected.GetComponent<RectTransform>().anchoredPosition = new Vector2(startX + i * spacing, 0);
            }
            else
            {
                bou.inactive.GetComponent<RectTransform>().anchoredPosition = new Vector2(startX + i * spacing, 0);
                bou.selected.GetComponent<RectTransform>().anchoredPosition = unvisible;
            }
        }
        if (Input.GetKeyDown(InputSettings.Confirm))
        {
            BuyBuilding();
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
        PrepareStorage();
        StartClock();
        InstantiateEnemies();
        PrepareBuildingsInfo();
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
