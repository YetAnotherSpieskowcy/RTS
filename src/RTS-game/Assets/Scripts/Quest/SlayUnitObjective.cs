using UnityEngine;

[RequireComponent(typeof(Unit))]
public class SlayUnitObjective : QuestObjective
{
    Unit unit;
    void Awake()
    {
        unit = GetComponent<Unit>();
    }


    public override bool IsCompleted()
    {
        return !unit.IsAlive();
    }
}
