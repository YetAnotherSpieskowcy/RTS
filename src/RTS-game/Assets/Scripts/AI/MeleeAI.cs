using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAI : MonoBehaviour
{
    public float hitRate = 10.0f;
    private EnemyAI ai;
    private AIAnimation anim;
    private Unit unit;
    void Awake()
    {
        ai = GetComponent<EnemyAI>();
        unit = GetComponent<Unit>();
        anim = GetComponent<AIAnimation>();
    }
    void Start()
    {
        ai.StoppingDistance = ai.Radius;
    }
    private float delay = 0;
    void Update()
    {
        if (!unit.IsAlive()) return;
        if (ai.target != null && ai.IsStopped && Vector3.Distance(ai.target.position, transform.position) <= ai.Radius)
        {
            if (delay > hitRate)
            {
                Unit unit = ai.target.GetComponentInParent<Unit>();
                if (unit != null)
                {
                    if (!unit.IsAlive())
                    {
                        ai.Target(null);
                        return;
                    }
                    unit.Hit(1);

                    if (anim != null)
                    {
                        anim.Attack();
                    }
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