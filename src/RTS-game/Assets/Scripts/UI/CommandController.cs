using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CommandController : MonoBehaviour
{
    public RectTransform commandsBackground;
    public GameObject commandPrefab;
    public GameObject[] chooseGroup;
    public GameObject[] chooseCommand;
    void Start()
    {
        InstantiateGroup();
        InstantiateOption();
        SetAllInvisible();
    }

    GameObject[] CreateObjects(List<string> options)
    {
        int startY = 0;
        int spacing = -45;
        List<GameObject> tmpCommands = new List<GameObject>();
        foreach (string option in options)
        {
            GameObject o = Instantiate(commandPrefab, commandsBackground);
            o.GetComponentsInChildren<TMP_Text>()[0].text = option;
            o.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, startY + spacing);
            startY += spacing;
            tmpCommands.Add(o);
        }
        return tmpCommands.ToArray();
    }
    void InstantiateGroup()
    {
        List<string> options = new List<string> { "1: Everyone", "2: Melee", "3: Ranged", "0: Cancel" };
        this.chooseGroup = CreateObjects(options);
    }
    void InstantiateOption()
    {
        List<string> options = new List<string> { "1: Follow", "2: Halt", "3: Attack", "4: Go here", "5: Retreat", "0: Cancel" };
        this.chooseCommand = CreateObjects(options);
    }
    public void ActivateGroupChoice()
    {
        foreach (var command in this.chooseCommand)
        {
            command.SetActive(false);
        }
        foreach (var group in this.chooseGroup)
        {
            group.SetActive(true);
        }
    }
    public void ActivateCommandChoice()
    {
        foreach (var command in this.chooseCommand)
        {
            command.SetActive(true);
        }
        foreach (var group in this.chooseGroup)
        {
            group.SetActive(false);
        }
    }
    public void SetAllInvisible()
    {
        foreach (var command in this.chooseCommand)
        {
            command.SetActive(false);
        }
        foreach (var group in this.chooseGroup)
        {
            group.SetActive(false);
        }
    }
}