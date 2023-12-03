using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.Generic;
using Base;
using UnityEngine;

public class HPSpecifier : SerializationSpecifier
{
    public override void Load(List<Param> paramList)
    {
        Unit unit = GetComponent<Unit>();
        UnityEngine.Debug.Log("bear health: "+ unit.GetHealth());
        if (unit.GetHealth() < 1)
        {
            unit.Hit(1000);
        }
    }

    public override List<Param> Save()
    {
        return new List<Param>();
    }
}
