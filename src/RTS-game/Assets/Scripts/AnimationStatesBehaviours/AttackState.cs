using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : StateMachineBehaviour
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        MeleeAI meleeAI;
        RangeAI rangeAI;
        if (animator.gameObject.TryGetComponent<RangeAI>(out rangeAI))
        {
            rangeAI.SetShootAnimRunning(true);
            animator.gameObject.TryGetComponent<MeleeAI>(out meleeAI);
            if (meleeAI != null)
                meleeAI.SetAttackAnimRunning(true);
        }
        else if (animator.gameObject.TryGetComponent<MeleeAI>(out meleeAI))
        {
            meleeAI.SetAttackAnimRunning(true);
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        MeleeAI meleeAI;
        RangeAI rangeAI;
        if (animator.gameObject.TryGetComponent<RangeAI>(out rangeAI))
        {
            Debug.Log("abc");
            rangeAI.SetShootAnimRunning(false);
            animator.gameObject.TryGetComponent<MeleeAI>(out meleeAI);
            if (meleeAI != null) 
                meleeAI.SetAttackAnimRunning(false);
        }
        else if (animator.gameObject.TryGetComponent<MeleeAI>(out meleeAI))
        {
            meleeAI.SetAttackAnimRunning(false);
        }
        else
        {
            GameObject.Find("Player").GetComponent<PlayerController>().SetMode(CharacterMode.NORMAL_MODE);
        }
    }
}
