using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Manager : MonoBehaviour
{
    private BoxCollider collider;

    private Building building = null;
    private int collides = 0;
    private int terrainLayer = 1 << 9;

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
        bool valid = HasValidPlacement() & ValidateGround();
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

    public bool ValidateCorner(Vector3 position)
    {
        Vector3 direction = new Vector3(0, -1, 0);
        RaycastHit hit;
        if (Physics.Raycast(position, direction, out hit, 2f, terrainLayer))
        {
            return true;
        }
        return false;
    }

    public bool ValidateGround()
    {
        Vector3 buildingPosition = building.GetTransform().position;
        float y = buildingPosition.y;
        Debug.Log(buildingPosition.x - (collider.size / 2f).x);
        List<Vector3> corners = new List<Vector3>();
        corners.Add(new Vector3(buildingPosition.x - (collider.size / 2f).x, y, buildingPosition.z - (collider.size / 2f).z));
        corners.Add(new Vector3(buildingPosition.x - (collider.size / 2f).x, y, buildingPosition.z + (collider.size / 2f).z));
        corners.Add(new Vector3(buildingPosition.x + (collider.size / 2f).x, y, buildingPosition.z - (collider.size / 2f).z));
        corners.Add(new Vector3(buildingPosition.x + (collider.size / 2f).x, y, buildingPosition.z + (collider.size / 2f).z));

        int collisionCnt = 0;
        
        foreach(Vector3 corner in corners)
        {
            if (!ValidateCorner(corner)) collisionCnt++;
        }

        return collisionCnt < 3;
    }
}
