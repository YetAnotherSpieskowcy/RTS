using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class QuestInfoController : MonoBehaviour
{
    public GameObject questJournal;
    public GameObject questJournalContent;

    private List<Quest> questsList;
    private GameObject questActive;
    private bool isVisible;

    void Start()
    {
        questsList = ((Quest[])GameObject.FindObjectsOfType(typeof(Quest))).ToList();
        questActive = GameObject.Find("QuestActive");
        questActive.SetActive(false);
        HideObjects();
        isVisible = true;
    }
    void Update()
    {
        if (Input.GetKeyDown(InputSettings.Journal))
        {
            Debug.Log("Journal");
            if (isVisible)
            {
                ShowQuestJournal();
                isVisible = false;
            }
            else
            {
                HideQuestJournal();
                isVisible = true;
            }
        }
        UpdateQuestMarker();
    }

    private void ShowQuestJournal()
    {
        questJournal.SetActive(true);
        Cursor.lockState = CursorLockMode.Confined;
        foreach (var q in questsList)
        {
            GameObject qJournalObject = q.GetQuestJournalObject();
            GameObject notCompleted = GetAlertObject(qJournalObject.transform);

            if (q.IsStarted())
            {
                qJournalObject.SetActive(true);
                qJournalObject.transform.SetParent(questJournalContent.transform);
            }

            if (!q.IsCompleted())
            {
                notCompleted.SetActive(true);
            }
            else
            {
                notCompleted.SetActive(false);
            }
        }

    }

    private void HideObjects()
    {
        foreach (var q in questsList)
        {
            GameObject qJournalObject = q.GetQuestJournalObject();
            GameObject notCompleted = GetAlertObject(qJournalObject.transform);

            qJournalObject.transform.parent = null;
            notCompleted.SetActive(false);
            qJournalObject.SetActive(false);
        }

    }

    private void HideQuestJournal()
    {
        HideObjects();
        questJournal.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;

    }

    private void UpdateQuestMarker()
    {
        bool anyIsStarted = false;
        foreach (var q in questsList)
        {
            if (q.IsStarted())
            {
                anyIsStarted = true;
            }
        }

        if (anyIsStarted)
        {
            bool allStartedAreCompleted = true;
            foreach (var q in questsList)
            {
                if (q.IsStarted())
                {
                    if (!q.IsCompleted())
                    {
                        allStartedAreCompleted = false;
                    }
                }
            }

            if (!allStartedAreCompleted)
            {
                questActive.SetActive(true);
            }
            else
            {
                questActive.SetActive(false);
            }
        }
        else
        {
            questActive.SetActive(false);
        }
    }

    private GameObject GetAlertObject(Transform b)
    {
        foreach (Transform child in b)
        {
            if (child.tag == "Alert") return child.gameObject;
        }
        return null;
    }
}
