using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Pickable : MonoBehaviour
{
    public int wood, stone;
    private Storage storage;
    private IEnumerable<GatherOvermind> overminds;

    public void Pick()
    {
        storage.AddIncome(0, wood, stone);
        GameObject.Find("Player").GetComponent<PlayerController>().SetMode(CharacterMode.GATHER_MODE);
        foreach (var overmind in overminds)
        {
            overmind.NotifyPicked(this);
        }
        Destroy(GetComponent<Transform>().gameObject);
    }

    void Awake()
    {
        storage = GameObject.Find("Player").GetComponent<BuildMechanismController>().GetBuildMechanismMediator().GetStorage();
        overminds = from it in GameObject.FindGameObjectsWithTag("Gather") select it.GetComponent<GatherOvermind>();
    }
}
