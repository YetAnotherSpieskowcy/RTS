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
}

// ----- mechanisms -----
public class UIBuildingMode : MonoBehaviour
{
    public RectTransform buildingBackgroundTransform;
    public GameObject buildingPrefab;
    public GameObject selectedBuildingPrefab;
    private List<buildingOnUI> buildingsOnUI;
    private BuildMechanismMediator buildMediator = new BuildMechanismMediator();
    private bool isPlaced = false;

    // ----- building mode -----
    void PrepareBuildingsInfo()
    {
        int startX = 150;
        int spacing = 150;

        int numberOfBuildings = buildMediator.GetNumberOfBuildings();

        this.buildingsOnUI = new List<buildingOnUI>();

        for (int i = 0; i < numberOfBuildings; i++)
        {
            buildingOnUI newBuilding = new buildingOnUI();

            BuildingData data = buildMediator.GetBuildingData(i);
            GameObject s = Instantiate(selectedBuildingPrefab, this.buildingBackgroundTransform);
            s.GetComponentInChildren<RawImage>().texture = data.bts;
            s.GetComponent<RectTransform>().anchoredPosition = new Vector2(startX + i * spacing, 0);
            s.GetComponentsInChildren<TMP_Text>()[0].text = data.money.ToString();
            s.GetComponentsInChildren<TMP_Text>()[1].text = data.wood.ToString();
            s.GetComponentsInChildren<TMP_Text>()[2].text = data.stone.ToString();
            newBuilding.selected = s;
            newBuilding.selected.SetActive(false);

            GameObject b = Instantiate(buildingPrefab, this.buildingBackgroundTransform);
            b.GetComponentInChildren<RawImage>().texture = data.bts;
            b.GetComponent<RectTransform>().anchoredPosition = new Vector2(startX + i * spacing, 0);
            newBuilding.inactive = b;
            newBuilding.inactive.SetActive(false);

            this.buildingsOnUI.Add(newBuilding);
        }
    }
    void ClearUIAfterBuildingMode()
    {
        for (int i = 0; i < buildMediator.GetNumberOfBuildings(); i++)
        {
            this.buildingsOnUI[i].selected.SetActive(false);
            this.buildingsOnUI[i].inactive.SetActive(false);

            GameObject alert = GetAlertObject(this.buildingsOnUI[i].selected.transform);
            if (alert != null)
                alert.SetActive(false);
        }
    }
    void BuyBuilding()
    {
        if (buildMediator.CheckEnoughResources())
        {
            buildMediator.SetAction(Action.PLACE);
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

        int numberOfBuildings = buildMediator.GetNumberOfBuildings();

        for (int i = 0; i < numberOfBuildings; i++)
        {
            if (i == buildMediator.GetBuildingId())
            {
                BuildingData data = buildMediator.GetBuildingData(i);
                this.buildingsOnUI[i].selected.SetActive(true);
                this.buildingsOnUI[i].inactive.SetActive(false);

                GameObject alert = GetAlertObject(this.buildingsOnUI[i].selected.transform);

                if (alert != null)
                { 
                    if (buildMediator.GetStorage().EnoughResources(data.money, data.wood, data.stone))
                        alert.SetActive(false);
                    else
                        alert.SetActive(true);
                }
            }
            else
            {
                this.buildingsOnUI[i].selected.SetActive(false);
                this.buildingsOnUI[i].inactive.SetActive(true);

                GameObject alert = GetAlertObject(this.buildingsOnUI[i].inactive.transform);
                if (alert != null)
                    alert.SetActive(false);
            }
        }
    }
    void UpdateSelectedBuilding()
    {
        if (Input.GetKeyDown(InputSettings.Next))
        {
            buildMediator.IncrementBuildingId();
            buildMediator.SetAction(Action.PREPARE);
        }
        else if (Input.GetKeyDown(InputSettings.Previous))
        {
            buildMediator.DecrementBuildingId();
            buildMediator.SetAction(Action.PREPARE);
        }

        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(InputSettings.Confirm))
        {
            BuyBuilding();
        }
    }
    // ----- modes -----
    void UpdateMode()
    {
        if (Input.GetKeyDown(InputSettings.ChangeToBUildingMode) && !isPlaced)
        {
            UIBasicMode.gameMode = Mode.BUILDING;
            buildMediator.InitializeBuildingId();
            buildMediator.SetAction(Action.PREPARE);
        }
        else if (Input.GetKeyDown(InputSettings.ExitBuildingMode) || isPlaced)
        {
            if (UIBasicMode.gameMode == Mode.BUILDING)
            {
                ClearUIAfterBuildingMode();
                if (!isPlaced)
                    buildMediator.SetAction(Action.CANCEL);
                else
                    buildMediator.SetAction(Action.WAIT);
            }
            UIBasicMode.gameMode = Mode.NORMAL;
            isPlaced = false;
        }
        else if (buildMediator.GetAction() == Action.PLACED)
        {
            BuildingData data = buildMediator.GetBuildingData();
            isPlaced = true;
            buildMediator.GetStorage().SubstractCost(data.money, data.wood, data.stone);
            buildMediator.SetAction(Action.WAIT);
        }
    }
    // ----- UI -----
    void Start()
    {
        buildMediator = GameObject.Find("BuildMechanism").GetComponent<BuildMechanismController>().GetBuildMechanismMediator();
        PrepareBuildingsInfo();
        buildMediator.GetStorage().UpdateStorage();
    }
    void Update()
    {
        UpdateMode();
        if (UIBasicMode.gameMode == Mode.BUILDING)
        {
            UpdateBuildingMode();
        }
    }

    private GameObject GetAlertObject(Transform b)
    {
        foreach (Transform child in b)
        {
            if (child.tag == "Alert") return child.gameObject;
        }
        return null;
    }
}