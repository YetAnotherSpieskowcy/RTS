using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Base;

public abstract class SerializationSpecifier : MonoBehaviour
{
    public abstract void Load(List<Param> paramList);
    public abstract List<Param> Save();
}
