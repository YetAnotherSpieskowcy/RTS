using System.Collections.Generic;
using UnityEngine;
using Base;
using Google.Protobuf;
using System.IO;
using System.Linq;

public class SaveManager
{
    public static void Load(string saveName)
    {
        Base.Save save;
        using (var file = File.OpenRead(saveName))
        {
            save = Base.Save.Parser.ParseFrom(file);
            foreach (var entity in save.Entities)
            {
                GameObject go = GameObject.Find(entity.Name);
                if (go == null)
                {
                    Object ob = Resources.Load(entity.Prefab);
                    go = (GameObject)GameObject.Instantiate(ob);
                }
                go.GetComponent<Saveable>().Load(entity);
            }
        }
    }

    public static void Save(string fileName)
    {
        Base.Save save = new();
        List<Saveable> savables = ((Saveable[])GameObject.FindObjectsOfType(typeof(Saveable))).ToList();
        savables.ForEach(it =>
        {
            save.Entities.Add(it.Serialize());
        });
        using (var file = File.Create(fileName))
        {
            save.WriteTo(file);
        }

    }

    public SaveableEntity GetInfoByName(string name)
    {
        return null;
    }
}
