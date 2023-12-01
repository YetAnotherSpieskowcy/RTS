using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Base;


[RequireComponent(typeof(Buildable))]
public class BuildingSpecifier : SerializationSpecifier
{
    public override void Load(List<Param> paramList)
    {
        GetComponent<Buildable>().OnPlaced();
        foreach (var param in paramList)
        {
            if (param.Key == "WORK")
            {
                GetComponent<Buildable>().workRequired = float.Parse(param.Value);
                GetComponent<Buildable>().CheckCompleted();
            }
        }

    }
    public override List<Param> Save()
    {
        Param p = new();
        p.Key = "WORK";
        p.Value = GetComponent<Buildable>().workRequired.ToString();
        List<Param> list = new();
        list.Add(p);
        return list;
    }
}
