using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(EnemyAI))]
public class AnimalAI : MonoBehaviour
{
    private EnemyAI ai;
    public float keptDistance = 20.0f;
    public float randomWanderChance = .08f;
    public float randomWalkDistance = 5.0f;
    void Awake()
    {
        ai = GetComponent<EnemyAI>();
    }
    void Update()
    {
        List<Unit> possibleTargets = BattleContext.Context.GetTargetsOfAligment(Unit.Team.Friendly, (it => Vector3.Distance(transform.position, it.transform.position) < keptDistance)).ToList();
        if (possibleTargets.Count == 0)
        {
            if (Random.value < randomWanderChance)
            {
                ai.Target(new Vector3((Random.value - .5f) * randomWalkDistance, 0, (Random.value - .5f) * randomWalkDistance) + transform.position);
            }
        }
        else
        {
            Vector3 dest = Vector3.zero;
            foreach (var target in possibleTargets)
            {
                dest -= target.transform.position - transform.position;
            }
            ai.Target(dest + transform.position);
        }
    }
}


