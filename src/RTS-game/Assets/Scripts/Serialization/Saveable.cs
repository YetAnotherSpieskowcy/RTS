using Base;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

public class Saveable : MonoBehaviour
{
    public UnityEvent preSaveHook;
    public void SetPrefabName(string name)
    {
        prefabName = name;
    }

    private string prefabName = "na";
    public SaveableEntity Serialize()
    {
        preSaveHook.Invoke();
        SaveableEntity entity = new();
        entity.Name = gameObject.name;
        Unit unit = GetComponent<Unit>();
        entity.Hp = unit != null ? unit.GetHealth() : 0;
        Location location = new();
        location.Position = new();
        location.Position.X = transform.position.x;
        location.Position.Y = transform.position.y;
        location.Position.Z = transform.position.z;
        location.Rotation = new();
        location.Rotation.X = transform.rotation.eulerAngles.x;
        location.Rotation.Y = transform.rotation.eulerAngles.y;
        location.Rotation.Z = transform.rotation.eulerAngles.z;
        entity.Transform = location;
        entity.Prefab = prefabName;
        SerializationSpecifier specifier;
        specifier = GetComponent<SerializationSpecifier>();
        if (specifier != null)
        {
            entity.Params.AddRange(specifier.Save());
        }
        return entity;
    }
    public void Load(SaveableEntity entity)
    {
        Unit unit = GetComponent<Unit>();
        if (unit != null)
            entity.Hp = entity.Hp;
        prefabName = entity.Prefab;
        Vector3 position = new();
        Vector3 rotation = new();
        position.x = entity.Transform.Position.X;
        position.y = entity.Transform.Position.Y;
        position.z = entity.Transform.Position.Z;
        rotation.x = entity.Transform.Rotation.X;
        rotation.y = entity.Transform.Rotation.Y;
        rotation.z = entity.Transform.Rotation.Z;
        transform.SetPositionAndRotation(position, Quaternion.Euler(rotation));
        SerializationSpecifier specifier;
        specifier = GetComponent<SerializationSpecifier>();
        if (specifier != null)
        {
            specifier.Load(entity.Params.ToList());
        }
    }
}
