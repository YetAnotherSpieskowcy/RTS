using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Overmind : MonoBehaviour
{
    public float idleRange;
    public float aggroRange;
    public float maxRange;
    [SerializeField] private List<EnemyAI> enemies = new();
    [SerializeField]
    [Range(0.0f, 1.0f)]
    private float idleMove = .5f;
    private Transform target;

    void OnValidate()
    {
        idleRange = idleRange > 0 ? idleRange : 0;
        aggroRange = aggroRange > 0 ? aggroRange : 0;
        maxRange = maxRange > 0 ? maxRange : 0;
        idleRange = aggroRange < idleRange ? aggroRange : idleRange;
        aggroRange = maxRange < aggroRange ? maxRange : aggroRange;
        idleMove = idleMove > 0 ? idleMove : 0;
        idleMove = idleMove < 1.0f ? idleMove : 1.0f;
    }


    void OnDrawGizmos()
    {
        GizmosGeometry.DrawCircle(idleRange, Color.white, transform.position, Vector3.up);
        GizmosGeometry.DrawCircle(aggroRange, Color.red, transform.position, Vector3.up);
        GizmosGeometry.DrawCircle(maxRange, Color.yellow, transform.position, Vector3.up);
    }

    void Start()
    {
        BattleContext.Context.ReInit();
        foreach (var enemy in enemies)
        {
            enemy.SetCenterLock(transform, maxRange);
        }
    }


    void Update()
    {
        // TODO move this snipped to BattleContext
        List<Unit> possibleTargets = BattleContext.Context.GetTargetsOfAligment(Unit.Team.Friendly, (it => Vector3.Distance(transform.position, it.transform.position) < aggroRange)).ToList();
        if (possibleTargets.Count > 0)
        {
            foreach (var enemy in enemies)
            {
                // TODO resolve RangeAI targeting
                var allocated = (from n in possibleTargets where !n.CompareTag("Player") && n.GetComponent<EnemyAI>().target == enemy.transform orderby Vector3.Distance(enemy.transform.position, n.transform.position) select n);
                if (allocated.Count() == 0)
                {
                    Unit target = (from n in possibleTargets orderby Vector3.Distance(enemy.transform.position, n.transform.position) select n).First();
                    enemy.Target(target.transform);
                    target.Notify(enemy.transform);
                }
                else
                {
                    enemy.Target(allocated.First().transform);
                }
            }

        }

        foreach (var enemy in enemies)
        {
            if (!enemy.HasTarget() && Random.value < idleMove)
            {
                float r = idleRange * (float)System.Math.Sqrt(Random.value);
                float th = Random.value * 2 * (float)System.Math.PI;
                Vector3 dst = new Vector3((float)(transform.position.x + r * System.Math.Sin(th)), 15, (float)(transform.position.z + r * System.Math.Cos(th)));
                enemy.MoveTo(dst);
            }
        }


    }
}
