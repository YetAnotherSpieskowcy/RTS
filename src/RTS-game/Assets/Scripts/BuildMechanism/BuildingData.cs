using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Prefabs/Buildings", order = 1)]
public class BuildingData : ScriptableObject
{
    [Header("Building info")]
    public string name;
    [Space(10)]
    [Header("Resources")]
    public int money, wood, stone;     // needed amount of resources
    [Space(10)]
    [Header("Construction")]
    public GameObject prefab;
    public Texture bts;
}

