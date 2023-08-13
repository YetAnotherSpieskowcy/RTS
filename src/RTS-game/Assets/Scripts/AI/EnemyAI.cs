using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyAI : MonoBehaviour
{
    private NavMeshAgent agent;
    [HideInInspector] public Transform target;

    private Transform centerLock;
    private float lockValue;
    public float StoppingDistance
    {
        get
        {
            return agent.stoppingDistance;
        }
        set
        {
            agent.stoppingDistance = value;
        }
    }
    public float Radius
    {
        get
        {
            return 4 * agent.radius;
        }
    }

    public bool IsStopped
    {
        get
        {
            return agent.desiredVelocity == Vector3.zero;
        }
    }
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

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        StoppingDistance = Radius;
    }

    void Update()
    {
        if (target != null)
        {
            agent.SetDestination(target.position);
            if (centerLock != null && Vector3.Distance(target.position, centerLock.position) > lockValue)
            {
                target = null;
            }
        }


    }
}
