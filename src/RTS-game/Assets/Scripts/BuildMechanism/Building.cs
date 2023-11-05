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
    private BuildingData buildingData;
    private GameObject obj;
    private Transform transform;
    private Placement placement;
    private List<Transform> children;
    private List<List<Material>> childrenMaterials;
    Transform player;

    public Building(BuildingData buildingData)
    {
        player = GameObject.FindWithTag("Player").transform;

        this.buildingData = buildingData;

        this.obj = GameObject.Instantiate(buildingData.prefab) as GameObject;

        CreateCollider();

        this.obj.AddComponent(typeof(Rigidbody));
        this.obj.GetComponent<Rigidbody>().isKinematic = true;

        this.obj.AddComponent(typeof(Manager));
        this.obj.GetComponent<Manager>().SetBuilding(this);

        transform = this.obj.transform;
        UpdatePosition();

        children = new List<Transform>();
        childrenMaterials = new List<List<Material>>();
        foreach (Transform child in transform)
        {
            children.Add(child);
            List<Material> childMat = new List<Material>();
            foreach (Material material in child.GetComponent<Renderer>().materials)
            {
                childMat.Add(new Material(material));
            }
            childrenMaterials.Add(childMat);
        }

        placement = Placement.VALID;
        SetMaterials();

    }

    private void CreateCollider()
    {
        obj.AddComponent(typeof(BoxCollider));
        obj.GetComponent<BoxCollider>().isTrigger = true;
        obj.GetComponent<BoxCollider>().center = new Vector3(obj.transform.position.x, obj.transform.position.y + buildingData.height / 2f, obj.transform.position.z);
        obj.GetComponent<BoxCollider>().size = new Vector3(buildingData.width, buildingData.height, buildingData.length);
    }

    public BoxCollider GetCollider()
    {
        return obj.GetComponent<BoxCollider>();
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

    public void SetState(Placement p)
    {
        placement = p;
    }

    private List<List<Material>> LoadMaterials(String matName)
    {
        List<List<Material>> mat;
        Material refMaterial = Resources.Load(matName) as Material;
        mat = new List<List<Material>>();
        for (int i = 0; i < childrenMaterials.Count; i++)
        {
            List<Material> tmp = new List<Material>();
            for (int j = 0; j < childrenMaterials[i].Count; j++)
            {
                tmp.Add(refMaterial);
            }
            mat.Add(tmp);
        }
        return mat;
    }

    public void SetMaterials()
    {
        List<List<Material>> mat;
        if (placement == Placement.VALID)
        {
            mat = LoadMaterials("Prefabs/Materials/Valid");
        }
        else if (placement == Placement.INVALID)
        {
            mat = LoadMaterials("Prefabs/Materials/Invalid");
        }
        else if (placement == Placement.PLACED)
        {
            mat = childrenMaterials;        
        }
        else
        {
            return;
        }
        int idx = 0;
        foreach (Transform child in children)
        {
            child.GetComponent<Renderer>().materials = mat[idx].ToArray();
            idx++;
        }
    }


    public void Place()
    {
        placement = Placement.PLACED;
        SetMaterials();
        transform.GetComponent<BoxCollider>().isTrigger = false;
    }

    public void CheckValid()
    {
        bool valid = this.obj.GetComponent<Manager>().CheckPlacement();
        if (placement == Placement.PLACED) return;
        placement = valid ? Placement.VALID : Placement.INVALID;
    }

    public void UpdatePosition()
    {
        Vector3 centre = new Vector3(player.position.x, 0, player.position.z);

        transform.eulerAngles = new Vector3(0, player.eulerAngles.y, 0);
        Vector3 buildingPosition = GetTransform().position + GetTransform().forward * (GetCollider().size.z / 2f);

        float alpha = player.eulerAngles.y / 180 * (float)Math.PI;
        int radius = 5;
        centre.y = Terrain.activeTerrain.SampleHeight(new Vector3(buildingPosition.x, 0, buildingPosition.z));
        Vector3 offset = new Vector3(radius * (float)Math.Sin(alpha), 0, radius * (float)Math.Cos(alpha));

        transform.position = centre + offset;
    }
}
