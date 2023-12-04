using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildTentObjective : QuestObjective
{
    VillageOverseer village;
    void Awake()
    {
        village = GetComponent<VillageOverseer>();
    }

    public override bool IsCompleted()
    {
        int n = village.GetNumberOfBuildingType("Tent");
        return n > 0;
    }
}
