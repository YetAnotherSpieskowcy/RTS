using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatController
{
    private float distance;

    public CombatController()
    {
        distance = 2f;
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
                unit.Hit(10);
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
