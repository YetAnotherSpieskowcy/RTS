using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingsList
{
    private List<BuildingInfo> buildings;

    public BuildingsList()
    {
        buildings = new List<BuildingInfo>();
        buildings.Add(new BuildingInfo("Building"));
    }

    public int GetBuildingId(string name)
    {
        for(int i = 0; i < buildings.Count; i++)
        {
            if(buildings[i].GetName() == name)
            {
                return i;
            }
        }

        return -1;
    }

    public BuildingInfo GetBuildingInfo(int id)
    {
        return buildings[id];
    }
}
