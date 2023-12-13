using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAI : MonoBehaviour
{
    public float hitRate = 10.0f;
    public bool hasSword = false;
    private EnemyAI ai;
    private AIAnimation anim;
    private Unit unit;
    private PlayerStats stats;
    private bool attackAnimRunning;
    void Awake()
    {
        ai = GetComponent<EnemyAI>();
        unit = GetComponent<Unit>();
        anim = GetComponent<AIAnimation>();
    }
    void Start()
    {
        ai.StoppingDistance = ai.Radius;
        attackAnimRunning = false;
        stats = GameObject.Find("Player").GetComponent<PlayerStats>();
    }
    private float delay = 0;
    void Update()
    {
        if (!unit.IsAlive()) return;
        if (ai.target != null && ai.IsStopped && Vector3.Distance(ai.target.position, transform.position) <= ai.Radius)
        {
            if (this.unit.IsFriendly && ai.target.tag == "Player")
                return;
            if (!attackAnimRunning && delay > hitRate)
            {
                Unit unit = ai.target.GetComponentInParent<Unit>();
                if (unit != null)
                {
                    if (!unit.IsAlive())
                    {
                        ai.Target(null);
                        return;
                    }
                    unit.Hit(this.unit.IsFriendly ? stats.leadership : 1);

                    if (anim != null)
                    {
                        anim.Attack();
                    }
                    if (this.unit.IsFriendly)
                        stats.leadershipExpirience += .5f;
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
    public void SetAttackAnimRunning(bool running)
    {
        attackAnimRunning = running;
    }
    public bool GetAttackAnimRunning()
    {
        return attackAnimRunning;
    }
}
