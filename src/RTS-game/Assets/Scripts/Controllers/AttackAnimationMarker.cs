using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAnimationMarker : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {}

    public void MarkAnimationEnded()
    {
        PlayerController c = GameObject.Find("Player").GetComponent<PlayerController>();
        c.MarkAttackAnimationEnded();
    }
}
