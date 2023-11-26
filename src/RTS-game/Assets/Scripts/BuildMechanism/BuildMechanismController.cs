using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildMechanismController : MonoBehaviour
{
    private Building toPlace = null;
    private BuildMechanismMediator buildMediator = new BuildMechanismMediator();

    private RaycastHit raycastHit;
    private Vector3 lastPlace;
    private static int terrainLayer = 1 << 9;

    void Start()
    {
        buildMediator.LoadBuildings();
    }

    void Update()
    {
        Action a = buildMediator.GetAction();
        if (toPlace != null)
        {
            if (a == Action.CANCEL || a == Action.CANCEL_UNAVAILABLE)
            {
                Cancel(a);
                return;
            }
            else if (a == Action.PREPARE)
            {
                Prepare();
            }

            toPlace.UpdatePosition();

            if (Physics.Raycast(toPlace.GetTransform().position, toPlace.GetTransform().forward, out raycastHit, 1000f, terrainLayer))
            {
                if (lastPlace != raycastHit.point)
                {
                    string comment = toPlace.CheckValid(buildMediator.CheckEnoughResources());
                    buildMediator.SetComment(comment);
                }
                lastPlace = raycastHit.point;
            }

            if (toPlace.IsValid() && a == Action.PLACE)
            {
                Place();
            }
            else if ((!toPlace.IsValid()) && a == Action.PLACE)
            {
                buildMediator.SetAction(Action.WAIT);
            }
        }
        else if (a == Action.PREPARE)
        {
            Prepare();
        }
    }

    public BuildMechanismMediator GetBuildMechanismMediator()
    {
        return buildMediator;
    }

    private void Prepare()
    {
        if (toPlace != null && !toPlace.IsPlaced())
        {
            Destroy(toPlace.GetTransform().gameObject);
        }

        Building building = new Building(buildMediator.GetBuildingData());
        toPlace = building;
        lastPlace = Vector3.zero;
        buildMediator.SetAction(Action.WAIT);
    }

    private void Cancel(Action a)
    {
        Destroy(toPlace.GetTransform().gameObject);
        toPlace = null;
        buildMediator.SetAction(a == Action.CANCEL ? Action.AVAILABLE : Action.UNAVAILABLE);
    }

    private void Place()
    {
        toPlace.Place();
        toPlace = null;
        buildMediator.SetAction(Action.PLACED);
    }
}
