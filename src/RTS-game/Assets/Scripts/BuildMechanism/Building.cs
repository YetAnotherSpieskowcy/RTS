using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum Placement
{
    INVALID,
    VALID,
    PLACED
}

public class Building
{
    private BuildingInfo info;
    private Manager manager;
    private Transform transform;
    private Placement placement;
    private List<Material> materials;
    Transform player;

    public Building(BuildingInfo info) //int resourceA, int resourceB
    {
        this.info = info;

        GameObject g = GameObject.Instantiate(
            Resources.Load($"Prefabs/Buildings/{info.GetName()}")
        ) as GameObject;
        transform = g.transform;
        UpdatePosition();

        materials = new List<Material>();
        foreach (Material material in transform.Find("Mesh").GetComponent<Renderer>().materials)
        {
            materials.Add(new Material(material));
        }

        placement = Placement.VALID;
        SetMaterials();

        manager = g.GetComponent<Manager>();
    }

    public string GetName()
    {
        return info.GetName();
    }

    public Transform GetTransform()
    {
        return transform;
    }

    public bool IsValid()
    {
        return placement == Placement.VALID;
    }

    public bool IsPlaced()
    {
        return placement == Placement.PLACED;
    }


    public void SetPosition(Vector3 position)
    {
        transform.position = position;
    }

    public void SetMaterials() 
    { 
        SetMaterials(placement); 
    }

    public void SetMaterials(Placement placement)
    {
        List<Material> mat;
        if (placement == Placement.VALID)
        {
            Material refMaterial = Resources.Load("Prefabs/Materials/Valid") as Material;
            mat = new List<Material>();
            for (int i = 0; i < materials.Count; i++)
            {
                mat.Add(refMaterial);
            }
        }
        else if (placement == Placement.INVALID)
        {
            Material refMaterial = Resources.Load("Prefabs/Materials/Invalid") as Material;
            mat = new List<Material>();
            for (int i = 0; i < materials.Count; i++)
            {
                mat.Add(refMaterial);
            }
        }
        else if (placement == Placement.PLACED)
        {
            mat = materials;
        }
        else
        {
            return;
        }
        transform.Find("Mesh").GetComponent<Renderer>().materials = mat.ToArray();
    }


    public void Place()
    {
        placement = Placement.PLACED;
        SetMaterials();
        transform.GetComponent<BoxCollider>().isTrigger = false;
    }

    public void CheckValid()
    {
        if (placement == Placement.PLACED) return;
        placement = manager.CheckPlacement() ? Placement.VALID : Placement.INVALID;
    }

    public void UpdatePosition()
    {
        player = GameObject.FindWithTag("Player").transform;
        transform.eulerAngles = new Vector3(0, player.eulerAngles.y, 0);
        float alpha = player.eulerAngles.y / 180 * (float)Math.PI;
        transform.position = player.position + new Vector3(5 * (float)Math.Sin(alpha), 0, 5 * (float)Math.Cos(alpha));
    }
}
