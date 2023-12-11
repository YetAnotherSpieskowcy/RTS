using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatController
{
    private float distance;

    private PlayerStats stats;
    public CombatController()
    {
        distance = 2f;
        stats = GameObject.Find("Player").GetComponent<PlayerStats>();
    }

    public void CheckAttack(Vector3 position, Vector3 direction)
    {
        Ray ray = new Ray(position, direction);
        //Debug.DrawRay(position, direction * distance, Color.red, 1.0f, true);
        if (Physics.SphereCast(ray, 1, out RaycastHit hit, distance))
        {
            Unit unit = hit.transform.GetComponentInParent<Unit>();
            if (unit != null)
            {
                Debug.Log("Player hit");
                if (!unit.IsAlive())
                {
                    return;
                }

                unit.Hit(stats.strength);
                stats.strengthExpirience += .6f;
            }
            else
            {
                Debug.Log("Not a unit");
            }
        }
        else
        {
            Debug.Log("Nothing to hit");
        }
    }
}
