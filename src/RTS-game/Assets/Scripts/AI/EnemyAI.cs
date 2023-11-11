using UnityEngine;
using UnityEngine.AI;

// TODO rename
[RequireComponent(typeof(NavMeshAgent))]
public class EnemyAI : MonoBehaviour
{
    private NavMeshAgent agent;
    private AIAnimation anim;
    public Transform target;

    private Transform centerLock;
    private float lockValue;
    private bool isDead = false;
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
        return !(target == null || target.CompareTag("Player"));
    }

    public void MoveTo(Vector3 position)
    {
        agent.SetDestination(position);
    }

    public void Target(Vector3 target)
    {
        agent.SetDestination(target);
        agent.isStopped = false;
    }

    public void Target(Transform target)
    {
        this.target = target;
        if (target != null)
        {
            agent.SetDestination(target.position);
            agent.isStopped = false;
        }
        else
        {
            agent.isStopped = true;
        }
    }

    public bool IsAlive()
    {
        return !isDead;
    }

    public void Die()
    {
        isDead = true;
        Target(null);
        if (anim != null)
        {
            anim.Die();
        }
    }

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<AIAnimation>();
        if (anim == null)
        {
            anim = GetComponentInChildren<AIAnimation>();
        }
        StoppingDistance = Radius;
    }

    void Update()
    {
        if (isDead)
        {
            return;
        }
        if (anim != null)
        {
            anim.Move(agent.desiredVelocity);
        }
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
