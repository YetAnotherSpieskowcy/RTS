using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Storage
{
    private int money, wood, stone;

    public Storage(int money, int wood, int stone)
    {
        this.money = money;
        this.wood = wood;
        this.stone = stone;
        Debug.Log(money);
    }

    public void UpdateStorage()
    {
        TMP_Text textM = GameObject.FindGameObjectsWithTag("Storage")[2].GetComponentInChildren<TMP_Text>();
        TMP_Text textW = GameObject.FindGameObjectsWithTag("Storage")[1].GetComponentInChildren<TMP_Text>();
        TMP_Text textS = GameObject.FindGameObjectsWithTag("Storage")[0].GetComponentInChildren<TMP_Text>();

        textM.text = this.money.ToString();
        textW.text = this.wood.ToString();
        textS.text = this.stone.ToString();
    }

    public bool EnoughResources(int money, int wood, int stone)
    {
        bool enough = true;
        if (money > this.money)
        {
            enough = false;
        }
        else if (wood > this.wood)
        {
            enough = false;
        }
        else if (stone > this.stone)
        {
            enough = false;
        }
        return enough;
    }

    public void SubstractCost(int money, int wood, int stone)
    {
        this.money -= money;
        this.wood -= wood;
        this.stone -= stone;
        UpdateStorage();
    }

    public void AddIncome(int money, int wood, int stone)
    {
        this.money += money;
        this.wood += wood;
        this.stone += stone;
        UpdateStorage();
    }
}
