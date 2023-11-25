using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController
{
    public Animator playerAnimator;

    private string runningAnimation;
    private bool attackRunning;
    private int attackVariants = 3;

    public AnimationController(Animator animator, string startAnimation)
    {
        playerAnimator = animator;
        playerAnimator.SetTrigger(startAnimation);
        runningAnimation = startAnimation;
        attackRunning = false;
    }

    public void ChooseAnimation(bool fightMode, bool running, float vertical, float horizontal)
    {
        string animation = "", speed, v, h, p = "";

        if (AttackAnimationRunning())
        {
            return;
        }

        if (fightMode)
        {
            animation = "Attack" + Random.Range(1, attackVariants);
            attackRunning = true;
        }
        else
        {
            if (running)
                speed = "Run";
            else
                speed = "Walk";
            if (vertical != 0 || horizontal != 0)
            {
                if (vertical == 1)
                    v = "Forward";
                else if (vertical == -1)
                    v = "Backward";
                else
                    v = "";

                if (horizontal == 1)
                    h = "Right";
                else if (horizontal == -1)
                    h = "Left";
                else
                    h = "";

                animation = speed + v + h;
            }
            else
            {
                animation = "Idle";
            }
        }

        if (animation != runningAnimation)
            SetAnimation(animation);
    }

    public bool AttackAnimationRunning()
    {
        return attackRunning;
    }

    private void SetAnimation(string startAnim)
    {
        Debug.Log(startAnim);
        playerAnimator.ResetTrigger(runningAnimation);
        playerAnimator.SetTrigger(startAnim);
        runningAnimation = startAnim;
    }

    public void MarkAttackEnded()
    {
        //Debug.Log("stop running");
        attackRunning = false;
    }
}
