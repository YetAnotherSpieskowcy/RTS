using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyAI))]
public class RangeAI : MonoBehaviour
{
    public float rangeDistance = 25.0f;
    public float meleeDistance = 10.0f;
    public float fireRate = 10.0f;
    private AIAnimation anim;
    private EnemyAI ai;
    private RangeAttack ra;
    private bool shootAnimRunning;
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
        anim = GetComponent<AIAnimation>();
        ai = GetComponent<EnemyAI>();
        ra = GetComponentInChildren<RangeAttack>();
    }
    void Start()
    {
        shootAnimRunning = false;
    }

    private float delay = 0.0f;
    void Update()
    {
        if (ai.target != null && Vector3.Distance(transform.position, ai.target.position) > meleeDistance)
        {
            if (GetComponent<Unit>().IsFriendly && ai.target.tag == "Player")
                return;
            ai.StoppingDistance = rangeDistance;
            if (!shootAnimRunning && ai.IsStopped && delay > fireRate)
            {
                ra.transform.LookAt(ai.target);
                ra.Shoot();
                if (anim != null)
                {
                    Debug.Log("shooting");
                    anim.Shoot();
                }
                delay = 0.0f;
            }
            else
            {
                delay += Time.deltaTime;
            }
        }
        else
        {
            ai.StoppingDistance = ai.Radius;
        }

    }
    public void SetShootAnimRunning(bool running)
    {
        shootAnimRunning = running;
    }
    public bool GetShootAnimRunning()
    {
        return shootAnimRunning;
    }
}
