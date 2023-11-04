using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(BoxCollider))]
public class Manager : MonoBehaviour
{
    private BoxCollider collider;

    private Building building = null;
    private int collides = 0;
    private int terrainLayer = 1 << 9;
    private List<Vector3> directions = new List<Vector3>();


    public void SetBuilding(Building building)
    {
        this.building = building;
        collider = building.GetCollider();
        CalculateDirections();
    }

    private void CalculateDirections()
    {
        directions.Add(new Vector3(collider.size.x, 0, collider.size.z));
        directions.Add(new Vector3(collider.size.x, 0, -collider.size.z));
        directions.Add(new Vector3(-collider.size.x, 0, collider.size.z));
        directions.Add(new Vector3(-collider.size.x, 0, -collider.size.z));

        directions.Add(new Vector3(collider.size.x, 0, 0));
        directions.Add(new Vector3(-collider.size.x, 0, 0));
        directions.Add(new Vector3(0, 0, collider.size.z));
        directions.Add(new Vector3(0, 0, -collider.size.z));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 9) return;
        collides++;
        CheckPlacement();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 9) return;
        collides--;
        CheckPlacement();
    }

    public bool CheckPlacement()
    {
        if (building == null) return false;
        if (building.IsPlaced()) return false;
        bool valid = ValidateTrees(); //HasValidPlacement() & ValidateGround() & 
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

    public bool ValidateTrees()
    {
        Vector3 position = building.GetTransform().position;
        position.y += (collider.size.y / 2f);
        float raycastLength = Mathf.Sqrt(Mathf.Pow(collider.size.x, 2f) + Mathf.Pow(collider.size.z, 2f)) / 2f + .2f;

        RaycastHit hit;
        foreach (Vector3 direction in directions)
        {
            if (Physics.Raycast(position, direction, out hit, raycastLength, terrainLayer)) return false;
        }
        // TODO: add validation when tree is in the middle of phantom
        return true;
    }

    public bool ValidateCorner(Vector3 position)
    {
        Vector3 direction = new Vector3(0, -1, 0);
        RaycastHit hit;
        return Physics.Raycast(position, direction, out hit, .1f, terrainLayer);
    }

    public bool ValidateGround()
    {
        Vector3 buildingPosition = building.GetTransform().position;
        float y = buildingPosition.y + .01f;
        List<Vector3> corners = new List<Vector3>();
        corners.Add(new Vector3(buildingPosition.x - (collider.size.x / 2f), y, buildingPosition.z - (collider.size.z / 2f)));
        corners.Add(new Vector3(buildingPosition.x - (collider.size.x / 2f), y, buildingPosition.z + (collider.size.z / 2f)));
        corners.Add(new Vector3(buildingPosition.x + (collider.size.x / 2f), y, buildingPosition.z - (collider.size.z / 2f)));
        corners.Add(new Vector3(buildingPosition.x + (collider.size.x / 2f), y, buildingPosition.z + (collider.size.z / 2f)));

        int validCornerCnt = 0;
        
        foreach(Vector3 corner in corners)
        {
            if (ValidateCorner(corner)) validCornerCnt++;
        }

        return validCornerCnt > 2;
    }
}
