using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerAI : MonoBehaviour
{
    public float workRate = 10.0f;
    private EnemyAI ai;
    private AIAnimation anim;
    void Awake()
    {
        ai = GetComponent<EnemyAI>();
        anim = GetComponent<AIAnimation>();
        if (anim == null)
        {
            anim = GetComponentInChildren<AIAnimation>();
        }
    }
    void Start()
    {
        ai.StoppingDistance = ai.Radius;
    }
    private float delay = 0;
    void Update()
    {
        if (ai.target != null && ai.IsStopped)
        {
            anim.Work();
            if (delay > workRate)
            {
                Buildable buildable = ai.target.GetComponent<Buildable>();
                if (buildable != null)
                {
                    buildable.DoWork(1);
                    if (anim != null)
                    {
                    }
                }
                delay = 0;
            }
            else
            {
                delay += Time.deltaTime;
            }
        }
        else
        {
            anim.StopWork();
        }
    }
}
