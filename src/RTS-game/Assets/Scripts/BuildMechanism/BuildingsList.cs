using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingsList
{
    private List<BuildingData> buildings = new List<BuildingData>();

    public void LoadBuildings()
    {
        BuildingData[] data = Resources.LoadAll<BuildingData>("Prefabs/Buildings/");
        buildings.AddRange(data);

    }

    public int GetBuildingId(string name)
    {
        for(int i = 0; i < buildings.Count; i++)
        {
            if(buildings[i].name == name)
            {
                return i;
            }
        }

        return -1;
    }

    public BuildingData GetBuildingData(int id)
    {
        return buildings[id];
    }
}
