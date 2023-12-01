using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatherState : StateMachineBehaviour
{
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        GameObject.Find("Player").GetComponent<PlayerController>().SetMode(CharacterMode.NORMAL_MODE);
    }
}
