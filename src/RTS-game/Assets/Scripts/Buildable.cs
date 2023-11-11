using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class Buildable : MonoBehaviour
{

    public float workRequired = 10.0f;
    public float buildingRadious = 1.0f;
    public UnityEvent onBuildingCompleted;
    public UnityEvent onBuildingPlaced;
    private IEnumerable<VillageOverseer> overseers;

    void OnValidate()
    {
        buildingRadious = buildingRadious > 0 ? buildingRadious : 0.0f;
    }
    void OnDrawGizmos()
    {
        GizmosGeometry.DrawCircle(buildingRadious, Color.white, transform.position, Vector3.up);
    }

    void Awake()
    {
        overseers = from it in GameObject.FindGameObjectsWithTag("Overseer") select it.GetComponent<VillageOverseer>();
    }

    public void OnPlaced()
    {
        NavMeshObstacle obstacle = GetComponent<NavMeshObstacle>();
        if (obstacle != null)
        {
            obstacle.enabled = true;
        }
        onBuildingPlaced.Invoke();
        foreach (var overseer in overseers)
        {
            overseer.NotifyPlaced(this);
        }
    }

    public void DoWork(float work)
    {
        workRequired -= work;
        if (workRequired <= 0)
        {
            onBuildingCompleted.Invoke();
            foreach (var overseer in overseers)
            {
                overseer.NotifyCompleted(this);
            }
        }
    }
}
