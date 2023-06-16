using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Manager : MonoBehaviour
{
    private BoxCollider collider;

    private Building building = null;
    private int collides = 0;

    // Start is called before the first frame update
    public void Initialize(Building building)
    {
        collider = GetComponent<BoxCollider>();
        building = building;
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
            building.SetMaterials(Placement.INVALID);
        }
        else
        {
            building.SetMaterials(Placement.VALID);
        }
        return valid;
    }

    public bool HasValidPlacement()
    {
        return collides == 0;
    }
}
