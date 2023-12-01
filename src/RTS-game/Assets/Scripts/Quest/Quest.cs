using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Quest : MonoBehaviour
{
    public List<QuestObjective> objectives;

    private bool isStarted = false;
    public void StartQuest()
    {
        isStarted = true;
    }
    public bool IsStarted()
    {
        return isStarted;
    }
    public bool IsCompleted()
    {
        return objectives.Count(it => !it.IsCompleted()) == 0;
    }
}
