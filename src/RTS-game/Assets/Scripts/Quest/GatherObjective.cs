using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatherObjective : QuestObjective
{
    public int requiredWood;
    public int requiredStone;
    public NPC npc;

    public override bool IsCompleted()
    {
        bool condition = requiredStone <= 0 && requiredWood <= 0;
        if (condition)
            npc.SetPersistantFlag("ResourcesGathered");
        return condition;
    }

    public void UpdateResources(int wood, int stone)
    {
        requiredStone -= stone;
        requiredWood -= wood;
    }
}
