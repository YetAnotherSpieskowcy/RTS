using UnityEngine;

[RequireComponent(typeof(Unit))]
[RequireComponent(typeof(NPC))]
public class Mercenary : MonoBehaviour
{
    BuildMechanismMediator bm;
    Unit unit;
    NPC npc;
    public int price = 10;
    void Awake()
    {
        bm = GameObject.Find("Player").GetComponent<BuildMechanismController>().GetBuildMechanismMediator();
        unit = GetComponent<Unit>();
        npc = GetComponent<NPC>();
    }
    public void Hire()
    {
        if (CanHire())
        {
            bm.GetStorage().SubstractCost(price, 0, 0);
            unit.SetFriendly();
        }

    }
    public void UpdateHireFlag()
    {
        npc.SetPersistantFlag("CanHire", CanHire());
    }
    public void UpdateInteractability()
    {
        if (unit.IsFriendly)
        {
            if (GetComponent<Interactable>() != null)
            {
                Destroy(GetComponent<Interactable>());
            }
        }
    }
    public bool CanHire()
    {
        return bm.GetStorage().EnoughResources(price, 0, 0);
    }
}
