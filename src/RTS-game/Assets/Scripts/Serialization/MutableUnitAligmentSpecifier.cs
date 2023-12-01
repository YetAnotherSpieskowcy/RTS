using System.Collections.Generic;
using UnityEngine;
using Base;

[RequireComponent(typeof(Unit))]
public class MutableUnitAligmentSpecifier : SerializationSpecifier
{
    Unit unit;
    void Awake()
    {
        unit = GetComponent<Unit>();
    }
    public override void Load(List<Param> paramList)
    {
        paramList.ForEach(it =>
        {
            if (it.Key == "Team")
            {
                unit.team = (Unit.Team)int.Parse(it.Value);
            }
        });
    }

    public override List<Param> Save()
    {
        List<Param> list = new();
        Param p = new();
        p.Key = "Team";
        p.Value = ((int)unit.team).ToString();
        list.Add(p);
        return list;
    }
}
