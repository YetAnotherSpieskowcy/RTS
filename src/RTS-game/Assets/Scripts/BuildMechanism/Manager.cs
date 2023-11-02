using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Manager : MonoBehaviour
{
    private BoxCollider collider;

    private Building building = null;
    private int collides = 0;

    public void SetBuilding(Building building)
    {
        this.building = building;
        collider = building.GetCollider();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Terrain") return;
        collides++;
        CheckPlacement();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Terrain") return;
        collides--;
        CheckPlacement();
    }

    public bool CheckPlacement()
    {
        if (building == null) return false;
        if (building.IsPlaced()) return false;
        bool valid = HasValidPlacement();
        if (!valid)
        {
            building.SetState(Placement.INVALID);
        }
        else
        {
            building.SetState(Placement.VALID);
        }
        building.SetMaterials();
        return valid;
    }

    public bool HasValidPlacement()
    {
        return collides == 0;
    }
}
