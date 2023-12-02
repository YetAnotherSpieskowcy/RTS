using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickable : MonoBehaviour
{
    public int wood, stone;
    private Storage storage;

    public void Pick()
    {
        storage.AddIncome(0, wood, stone);
        GameObject.Find("Player").GetComponent<PlayerController>().SetMode(CharacterMode.GATHER_MODE);
        Destroy(GetComponent<Transform>().gameObject);
    }

    void Awake()
    {
        storage = GameObject.Find("Player").GetComponent<BuildMechanismController>().GetBuildMechanismMediator().GetStorage();
    }
}
