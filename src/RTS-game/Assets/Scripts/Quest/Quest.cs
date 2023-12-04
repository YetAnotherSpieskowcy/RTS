using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

public class Quest : MonoBehaviour
{
    public GameObject questJournalObject;
    public List<QuestObjective> objectives;

    private bool isStarted = false;

    public GameObject GetQuestJournalObject()
    {
        return questJournalObject;
    }
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
