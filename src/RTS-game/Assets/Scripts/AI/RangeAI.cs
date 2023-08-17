using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyAI))]
public class RangeAI : MonoBehaviour
{
    public float rangeDistance = 25.0f;
    public float meleeDistance = 10.0f;
    public float fireRate = 10.0f;
    private EnemyAI ai;
    private RangeAttack ra;
    void OnValidate()
    {
        rangeDistance = rangeDistance > 0 ? rangeDistance : 0;
        rangeDistance = rangeDistance > meleeDistance ? rangeDistance : meleeDistance;
        meleeDistance = meleeDistance > 0 ? meleeDistance : 0;
        fireRate = fireRate > 0 ? fireRate : 0;
    }
    void OnDrawGizmos()
    {
        GizmosGeometry.DrawCircle(rangeDistance, Color.red, transform.position, Vector3.up);
        GizmosGeometry.DrawCircle(meleeDistance, Color.white, transform.position, Vector3.up);
    }
    void Awake()
    {
        ai = GetComponent<EnemyAI>();
        ra = GetComponentInChildren<RangeAttack>();
    }

    private float delay = 0.0f;
    void Update()
    {
        if (ai.target != null && Vector3.Distance(transform.position, ai.target.position) > meleeDistance)
        {
            ai.StoppingDistance = rangeDistance;
            if (ai.IsStopped && delay > fireRate)
            {
                ra.transform.LookAt(ai.target);
                ra.Shoot();
                delay = 0.0f;
            }
            else
            {
                delay += Time.deltaTime;
            }
        }
        else //TODO if has MeleeAI else flee
        {
            ai.StoppingDistance = ai.Radius;
        }

    }
}
