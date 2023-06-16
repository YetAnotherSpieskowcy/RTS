using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Placer : MonoBehaviour
{
    private Building toPlace = null;
    private BuildingsList buildings = new BuildingsList();

    private RaycastHit raycastHit;
    private Vector3 lastPlace;
    private static int terrainLayer = 1 << 8;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (toPlace != null)
        {
            if (Input.GetKeyUp(KeyCode.Escape))
            {
                Cancel();
                return;
            }

            toPlace.UpdatePosition();


            if (Physics.Raycast(toPlace.GetTransform().position, toPlace.GetTransform().forward, out raycastHit, 1000f, terrainLayer))
            {
                if (lastPlace != raycastHit.point)
                {
                    toPlace.CheckValid();
                }
                lastPlace = raycastHit.point;
            }

            if (toPlace.IsValid() && Input.GetMouseButtonDown(0))
            {
                Place();
            }

        }
        else
        {
            if (Input.GetKeyUp(KeyCode.Tab))
            {
                Prepare();
            }
        }
    }

    private void Prepare()      // TODO: choose building
    {
        if (toPlace != null && !toPlace.IsPlaced())
        {
            Destroy(toPlace.GetTransform().gameObject);
        }

        Building building = new Building(buildings.GetBuildingInfo(0));
        building.GetTransform().GetComponent<Manager>().Initialize(building);
        toPlace = building;
        lastPlace = Vector3.zero;
    }

    private void Cancel()
    {
        Destroy(toPlace.GetTransform().gameObject);
        toPlace = null;
    }

    private void Place()
    {
        toPlace.Place();
        toPlace = null;
    }
}
