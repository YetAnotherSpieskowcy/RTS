using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Overmind : MonoBehaviour
{
    public float idleRange;
    public float aggroRange;
    public float maxRange;

    [Range(0, 1)] public float idleMove = .5f;
    void OnValidate()
    {
        idleRange = idleRange > 0 ? idleRange : 0;
        aggroRange = aggroRange > 0 ? aggroRange : 0;
        maxRange = maxRange > 0 ? maxRange : 0;
        idleRange = aggroRange < idleRange ? aggroRange : idleRange;
        aggroRange = maxRange < aggroRange ? maxRange : aggroRange;
    }

    void DrawCircle(float radious, Color color)
    {
        float corners = 30;
        float size = radious;
        Vector3 origin = transform.position;
        Vector3 startRotation = transform.right * size;
        Vector3 lastPosition = origin + startRotation;
        float angle = 0;
        Gizmos.color = color;
        while (angle <= 360)
        {
            angle += 360 / corners;
            Vector3 nextPosition = origin + (Quaternion.Euler(0, angle, 0) * startRotation);
            Gizmos.DrawLine(lastPosition, nextPosition);

            lastPosition = nextPosition;
        }

    }

    void OnDrawGizmos()
    {
        DrawCircle(idleRange, Color.white);
        DrawCircle(aggroRange, Color.red);
        DrawCircle(maxRange, Color.yellow);
    }
    Transform player;

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        foreach (var enemy in enemies)
        {
            enemy.SetCenterLock(transform, maxRange);
        }
    }

    public List<EnemyAI> enemies = new();

    void Update()
    {
        if (Vector3.Distance(transform.position, player.position) < aggroRange)
        {

            foreach (var enemy in enemies)
            {
                enemy.Target(player);
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
