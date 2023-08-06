using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAI : MonoBehaviour
{
    public float hitRate = 10.0f;
    private EnemyAI ai;
    void Awake()
    {
        ai = GetComponent<EnemyAI>();
    }
    void Start()
    {
        ai.StoppingDistance = ai.Radius;
    }
    private float delay = 0;
    void Update()
    {
        if (ai.target != null && ai.IsStopped && Vector3.Distance(ai.target.position, transform.position) <= ai.Radius)
        {
            if (delay > hitRate)
            {
                Unit unit = ai.target.GetComponentInParent<Unit>();
                Debug.Log("Hit attempt");
                if (unit != null)
                {
                    unit.Hit(1);
                    Debug.Log("Hit");
                }
                delay = 0.0f;
            }
            else
            {
                delay += Time.deltaTime;
            }

        }
    }
}
