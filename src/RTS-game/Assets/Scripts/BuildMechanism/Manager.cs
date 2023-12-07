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


    public void SetBuilding(Building building)
    {
        this.building = building;
        collider = building.GetCollider();
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
    private string PrepareComment(bool validTrees, bool validGround, bool validPlacement)
    {
        string comment = string.Empty;
        if (!validTrees || !validPlacement)
        {
            comment += "Building cannot collide with another object.\n";
        }
        if (!validGround)
        {
            comment += "Building can be only placed on flat ground.\n";
        }
        return comment;
    }
    public (bool, string) CheckPlacement()
    {
        if (building == null) return (false, string.Empty);
        if (building.IsPlaced()) return (false, string.Empty);
        bool validTrees = ValidateTrees();
        bool validGround = ValidateGround();
        bool validPlacement = HasValidPlacement();
        bool valid = validTrees && validGround && validPlacement;
        string comment = PrepareComment(validTrees, validGround, validPlacement);
        return (valid, comment);
    }

    public bool HasValidPlacement()
    {
        return collides == 0;
    }

    public bool ValidateTrees()
    {
        Vector3 right = Quaternion.Euler(0, 90f, 0) * building.GetTransform().forward;
        Vector3 position1 = building.GetTransform().position + right * (collider.size.x / 2f) - building.GetTransform().forward * (collider.size.z / 2f);
        position1.y += (collider.size.y / 2f);
        Vector3 position2 = position1 + building.GetTransform().forward * collider.size.z;

        float raycastLength = collider.size.z;
        int maxI = (int)(collider.size.x / .1f);
        RaycastHit hit;
        for (int i = 0; i < maxI; i++)
        {
            //Debug.DrawRay(position1, building.GetTransform().forward * raycastLength, Color.white, 5.0f, true);
            if (Physics.Raycast(position1, building.GetTransform().forward, out hit, raycastLength, terrainLayer)) return false;
            if (Physics.Raycast(position2, -building.GetTransform().forward, out hit, raycastLength, terrainLayer)) return false;
            position1 -= right * .1f;
            position2 -= right * .1f;
        }
        return true;
    }

    public bool ValidateCorner(Vector3 position)
    {
        Vector3 direction = new Vector3(0, -1, 0);
        RaycastHit hit;
        //Debug.DrawRay(position, direction, Color.white, 5.0f, false);
        return Physics.Raycast(position, direction, out hit, 2f, terrainLayer);
    }

    public bool ValidateGround()
    {
        float radius = Mathf.Sqrt(Mathf.Pow(collider.size.z, 2) + Mathf.Pow(collider.size.x, 2)) / 2f;
        float sinBeta = (collider.size.x / 2f) / radius;
        float alpha = building.GetTransform().eulerAngles.y / 180 * (float)Math.PI;
        float gamma1 = alpha - Mathf.Asin(sinBeta);
        float gamma2 = alpha + Mathf.Asin(sinBeta);

        Vector3 buildingPosition = building.GetTransform().position + building.GetTransform().forward * (collider.size.z / 2f);

        float y = buildingPosition.y + 0.01f;
        float deltaX1 = radius * Mathf.Sin(gamma1);
        float deltaZ1 = radius * Mathf.Cos(gamma1);
        float deltaX2 = radius * Mathf.Sin(gamma2);
        float deltaZ2 = radius * Mathf.Cos(gamma2);

        List<Vector3> corners = new List<Vector3>();
        corners.Add(new Vector3(buildingPosition.x - deltaX1, y, buildingPosition.z - deltaZ1));
        corners.Add(new Vector3(buildingPosition.x + deltaX2, y, buildingPosition.z + deltaZ2));
        corners.Add(new Vector3(buildingPosition.x - deltaX2, y, buildingPosition.z - deltaZ2));
        corners.Add(new Vector3(buildingPosition.x + deltaX1, y, buildingPosition.z + deltaZ1));

        int validCornerCnt = 0;

        foreach (Vector3 corner in corners)
        {
            if (ValidateCorner(corner)) validCornerCnt++;
        }
        return validCornerCnt > 2;
    }
}
