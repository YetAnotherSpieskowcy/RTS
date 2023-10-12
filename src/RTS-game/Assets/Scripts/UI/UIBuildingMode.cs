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

// ----- mechanisms -----
public class UIBuildingMode : MonoBehaviour
{
    private int numOfBuildings;
    private int selectedBuilding;
    public RectTransform buildingBackgroundTransform;
    public List<Texture> buildingTextureSources;
    public GameObject buildingPrefab;
    public GameObject selectedBuildingPrefab;
    private List<buildingOnUI> buildingsOnUI;

    // ----- building mode -----
    void PrepareBuildingsInfo()
    {
        this.buildingsOnUI = new List<buildingOnUI>();
        //here i will probably change buildingTextureSources to list of ScriptableObjects for buildiings?
        foreach (Texture bts in this.buildingTextureSources)
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
        this.selectedBuilding = 1;
    }
    void ClearUIAfterBuildingMode()
    {
        Vector2 invisible = new Vector2(0, 1000);
        for (int i = 0; i < buildingsOnUI.Count; i++)
        {
            buildingsOnUI[i].inactive.GetComponent<RectTransform>().anchoredPosition = invisible;
            buildingsOnUI[i].selected.GetComponent<RectTransform>().anchoredPosition = invisible;
        }
    }
    void BuyBuilding()
    {
        buildingOnUI bou = buildingsOnUI[selectedBuilding - 1];
        if (UIBasicMode.EnoughSources(bou.costM, bou.costS, bou.costW))
        {
            UIBasicMode.SubstractCost(bou.costM, bou.costW, bou.costS);
        }
        else
        {
            Debug.Log("ur too poor sry");
        }
    }
    void UpdateBuildingMode()
    {
        UpdateSelectedBuilding();

        int startX = 150;
        int spacing = 150;
        Vector2 invisible = new Vector2(0, 1000);
        for (int i = 0; i < buildingsOnUI.Count; i++)
        {
            buildingOnUI bou = buildingsOnUI[i];
            GameObject[] alerts = GameObject.FindGameObjectsWithTag("Alert");
            if (i == selectedBuilding - 1)
            {
                bou.inactive.GetComponent<RectTransform>().anchoredPosition = invisible;
                bou.selected.GetComponent<RectTransform>().anchoredPosition = new Vector2(startX + i * spacing, 0);
                if (UIBasicMode.EnoughSources(bou.costM, bou.costS, bou.costW))
                {
                    alerts[i].GetComponent<RectTransform>().anchoredPosition = invisible;
                }
                else
                {
                    alerts[i].GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -20);
                }
            }
            else
            {
                bou.inactive.GetComponent<RectTransform>().anchoredPosition = new Vector2(startX + i * spacing, 0);
                bou.selected.GetComponent<RectTransform>().anchoredPosition = invisible;
                alerts[i].GetComponent<RectTransform>().anchoredPosition = invisible;
            }
        }
        if (Input.GetKeyDown(InputSettings.Confirm))
        {
            BuyBuilding();
        }
    }
    void UpdateSelectedBuilding()
    {
        if (Input.GetKeyDown(InputSettings.Next))
        {
            this.selectedBuilding++;
            if (this.selectedBuilding > numOfBuildings)
            {
                this.selectedBuilding = 1;
            }
        }
        else if (Input.GetKeyDown(InputSettings.Previous))
        {
            this.selectedBuilding--;
            if (this.selectedBuilding == 0)
            {
                this.selectedBuilding = numOfBuildings;
            }
        }
    }
    // ----- modes -----
    void UpdateMode()
    {
        if (Input.GetKeyDown(InputSettings.ChangeToBUildingMode))
        {
            this.selectedBuilding = 1;
            UIBasicMode.gameMode = Mode.BUILDING;
        }
        else if (Input.GetKeyDown(InputSettings.ExitBuildingMode))
        {
            if (UIBasicMode.gameMode == Mode.BUILDING)
            {
                ClearUIAfterBuildingMode();
            }
            UIBasicMode.gameMode = Mode.NORMAL;
        }
    }
    // ----- UI -----
    void Start()
    {
        PrepareBuildingsInfo();
    }
    void Update()
    {
        UpdateMode();
        if (UIBasicMode.gameMode == Mode.BUILDING)
        {
            UpdateBuildingMode();
        }
    }
}