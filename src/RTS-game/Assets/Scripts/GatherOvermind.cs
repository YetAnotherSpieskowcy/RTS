using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatherOvermind : MonoBehaviour
{
    public List<Pickable> objectsToGather;
    private GatherObjective gatherObjective;

    void Awake()
    {
        TryGetComponent<GatherObjective>(out gatherObjective);
    }

    public void NotifyPicked(Pickable pickedObject)
    {
        if (!objectsToGather.Contains(pickedObject))
            return;
        if (gatherObjective != null)
            gatherObjective.UpdateResources(pickedObject.wood, pickedObject.stone);
        objectsToGather.Remove(pickedObject);
    }
}
