using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Action
{
    WAIT,
    PREPARE,
    CANCEL,
    PLACE,
    PLACED,
    AVAILABLE,
    UNAVAILABLE,
    CANCEL_UNAVAILABLE
}

public class BuildMechanismMediator
{
    private BuildingsList buildings;
    private int buildingId;
    private Action action;
    private Storage storage;
    private string comment;

    public BuildMechanismMediator()
    {
        action = Action.AVAILABLE;
        buildingId = 0;
        buildings = new BuildingsList();
        storage = new Storage(10, 0, 0);
    }
    public void SetComment(string com)
    {
        this.comment = com;
    }
    public string GetComment()
    {
        return this.comment;
    }
    public void IncrementBuildingId()
    {
        buildingId++;
        if (buildingId == buildings.GetNumberOfBuildings()) buildingId = 0;
    }

    public void DecrementBuildingId()
    {
        buildingId--;
        if (buildingId < 0) buildingId = buildings.GetNumberOfBuildings() - 1;
    }

    public void InitializeBuildingId()
    {
        buildingId = 0;
    }

    public int GetBuildingId()
    {
        return buildingId;
    }

    public void LoadBuildings()
    {
        buildings.LoadBuildings();
    }

    public BuildingData GetBuildingData()
    {
        return buildings.GetBuildingData(buildingId);
    }

    public BuildingData GetBuildingData(int id)
    {
        return buildings.GetBuildingData(id);
    }

    public int GetNumberOfBuildings()
    {
        return buildings.GetNumberOfBuildings();
    }

    public void SetAction(Action action)
    {
        this.action = action;
    }

    public Action GetAction()
    {
        return action;
    }

    public Storage GetStorage()
    {
        return storage;
    }

    public bool CheckEnoughResources()
    {
        BuildingData data = GetBuildingData();
        return storage.EnoughResources(data.money, data.wood, data.stone);
    }
}
