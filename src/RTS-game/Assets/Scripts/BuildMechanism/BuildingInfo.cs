using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingInfo
{

    private string name;
    //private int resourceA, resourceB;     // needed amount of resources

    public BuildingInfo(string name) //int resourceA, int resourceB
    {
        this.name = name;
        //this->resourceA = resourceA;
        //this->resourceB = resourceB;
    }

    public string GetName()
    {
        return name;
    }
}
