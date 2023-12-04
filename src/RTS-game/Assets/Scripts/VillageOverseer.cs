using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class VillageOverseer : MonoBehaviour
{
    private Dictionary<Buildable, List<EnemyAI>> buildables = new();
    private Dictionary<string, int> buildingsCount = new();
    public float buildingRadious = 10.0f;
    public List<EnemyAI> workers;
    void OnValidate()
    {
        buildingRadious = buildingRadious > 0 ? buildingRadious : 0.0f;
    }

    void OnDrawGizmos()
    {
        GizmosGeometry.DrawCircle(buildingRadious, Color.white, transform.position, Vector3.up);
    }

    public void NotifyPlaced(Buildable buildable)
    {
        if (Vector3.Distance(buildable.transform.position, transform.position) > buildingRadious) return;
        List<EnemyAI> assigned;
        if (buildables.Count == 0)
        {
            assigned = new List<EnemyAI>(workers);
        }
        else
        {
            assigned = new();
            int ac = workers.Count / (buildables.Count + 1);
            for (int i = 0; i < ac; i++)
            {
                Buildable b = (from it in buildables orderby it.Value.Count descending select it.Key).First();
                assigned.Add(buildables[b].First());
                buildables[b].RemoveAt(0);
            }
        }
        assigned.ForEach(it =>
        {
            it.Target(buildable.transform);
            it.StoppingDistance = buildable.buildingRadious + 1.0f;
        });
        buildables.Add(buildable, assigned);
    }

    public void NotifyCompleted(Buildable buildable)
    {
        if (!buildables.ContainsKey(buildable))
            return;
        List<EnemyAI> assigned = new();
        buildables[buildable].ForEach(it =>
        {
            it.Target(null);
            assigned.Add(it);
        });
        if (buildingsCount.ContainsKey(buildable.typeName))
        {
            buildingsCount[buildable.typeName]++;
        }
        else
        {
            buildingsCount.Add(buildable.typeName, 1);
        }
        buildables.Remove(buildable);
        if (buildables.Count > 0)
        {
            while (assigned.Count > 0)
            {
                var b = (from it in buildables orderby it.Value.Count descending select it).First();
                assigned.First().Target(b.Key.transform);
                b.Value.Add(assigned.First());
                assigned.RemoveAt(0);
            }
        }
    }
    public int GetNumberOfBuildingType(string type)
    {
        if (buildingsCount.ContainsKey(type))
        {
            return buildingsCount[type];
        }
        else
        {
            return 0;
        }
    }
}
