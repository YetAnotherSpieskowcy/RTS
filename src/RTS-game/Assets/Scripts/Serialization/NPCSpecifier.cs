using System.Collections.Generic;
using Base;
using UnityEngine;

[RequireComponent(typeof(NPC))]
public class NPCSpecifier : SerializationSpecifier
{
    NPC npc;
    void Awake()
    {
        npc = GetComponent<NPC>();
    }
    public override void Load(List<Param> paramList)
    {
        paramList.ForEach(it =>
        {
            npc.SetPersistantFlag(it.Key);
        });
    }

    public override List<Param> Save()
    {
        List<Param> list = new();
        npc.GetFlags().ForEach(it =>
        {
            Param p = new();
            p.Key = it;
            p.Key = "TRUE";
            list.Add(p);
        });
        return list;
    }
}
