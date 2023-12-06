using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Payment : MonoBehaviour
{
    BuildMechanismMediator bm;
    public int payment;

    void Awake()
    {
        bm = GameObject.Find("Player").GetComponent<BuildMechanismController>().GetBuildMechanismMediator();
    }

    public void Pay()
    {
        bm.GetStorage().AddIncome(payment, 0, 0);
        GetComponent<NPC>().SetPersistantFlag("GotPayment");
    }
}
