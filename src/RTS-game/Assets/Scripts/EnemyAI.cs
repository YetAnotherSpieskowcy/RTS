using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyAI : MonoBehaviour
{
    private NavMeshAgent agent;
    public Transform target;

    Transform centerLock;
    float lockValue;
    public void SetCenterLock(Transform transform, float lockValue)
    {
        centerLock = transform;
        this.lockValue = lockValue;
    }
    public bool HasTarget()
    {
        return target != null;
    }

    public void MoveTo(Vector3 position)
    {
        agent.destination = position;
    }

    public void Target(Transform target)
    {
        this.target = target;
        agent.SetDestination(target.position);
    }

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (target != null)
        {
            agent.SetDestination(target.position);
            if (Vector3.Distance(target.position, centerLock.position) > lockValue)
            {
                target = null;
            }
        }


    }
}
