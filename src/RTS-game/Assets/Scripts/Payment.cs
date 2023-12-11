using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Payment : MonoBehaviour
{
    BuildMechanismMediator bm;
    public int payment;
    private PlayerStats stats;

    void Awake()
    {
        bm = GameObject.Find("Player").GetComponent<BuildMechanismController>().GetBuildMechanismMediator();
        stats = GameObject.Find("Player").GetComponent<PlayerStats>();
    }


    public void Pay()
    {
        bm.GetStorage().AddIncome(payment, 0, 0);
        GetComponent<NPC>().SetPersistantFlag("GotPayment");
        stats.charismaExpirience += 5.0f;
    }
}
