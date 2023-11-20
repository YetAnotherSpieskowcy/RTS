using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandController
{
    private RectTransform commandsBackground;
    private GameObject commandPrefab;
    private GameObject[] chooseGroup;
    private GameObject[] chooseCommand;
    
    CommandController(RectTransform commandsBackground, GameObject commandPrefab)
    {
        this.commandsBackground = commandsBackground;
        this.commandPrefab = commandPrefab;
        List<string> groupNames= new List<string>{"1: Everyone","2: Melee","3: Ranged","0: Cancel"};
        List<string> commands= new List<string>{"1: Follow","2: Halt","3: Attack","4: Go here","5: Retreat","0: Cancel"};
        InstantiateOption(groupNames, this.chooseGroup);
        InstantiateOption(commands, this.chooseCommand);
    }
    void InstantiateOption(List<string> options, GameObject[] GameObjectStorage)
    {
        int startY = 0;
        int spacing = 200;
        List<GameObject> tmpGroups = new List<GameObject>();
        foreach (string option in options)
        {
            GameObject o = Instantiate(commandPrefab, commandsBackground);
            o.GetComponentsInChildren<TMP_Text>()[0].text = option;
            o.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, startY +  spacing);
            startY += spacing;
            tmpGroups.Add(o);
            o.SetActive(false);
        }
        GameObjectStorage = tmpGroups.ToArray();
    }
    void ActivateGroupChoice()
    {
        foreach (var command in chooseCommand)
        {
            command.SetActive(false);
        }
        foreach (var group in chooseGroup)
        {
            group.SetActive(true);
        }
    }
    void ActivateCommandChoice()
    {
        foreach (var command in chooseCommand)
        {
            command.SetActive(true);
        }
        foreach (var group in chooseGroup)
        {
            group.SetActive(false);
        }
    }
    void SetAllUnvisible()
    {
        
        foreach (var command in chooseCommand)
        {
            command.SetActive(false);
        }
        foreach (var group in chooseGroup)
        {
            group.SetActive(false);
        }
    }
}
