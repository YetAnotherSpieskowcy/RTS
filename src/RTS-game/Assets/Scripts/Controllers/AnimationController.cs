using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController
{
    public Animator playerAnimator;

    private string runningAnimation;
    private bool animationRunning;
    private int attackVariants = 3;

    public AnimationController(Animator animator, string startAnimation)
    {
        playerAnimator = animator;
        playerAnimator.SetTrigger(startAnimation);
        runningAnimation = startAnimation;
        animationRunning = false;
    }

    public void Die()
    {
        SetAnimation("Death");
    }

    public void Hit()
    {
        if (AnimationRunning())
        {
            return;
        }
        SetAnimation("Hit");
        animationRunning = true;
    }

    public void Gather()
    {
        if (runningAnimation == "Gather")
            return;
        SetAnimation("Gather");
    }

    public void Attack()
    {
        if (runningAnimation.Contains("Attack"))
            return;
        SetAnimation("Attack" + Random.Range(1, attackVariants));
        animationRunning = true;
    }

    public bool CheckIfAttackRunning()
    {
        for (int i = 1; i <= attackVariants; i++)
        {
            if (playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Attack" + i))
            {
                return true;
            }
        }
        animationRunning = false;
        return false;
    }

    public void ChooseAnimation(bool running, float vertical, float horizontal)
    {
        string animation = "", speed, v, h, p = "";

        if (AnimationRunning())
        {
            return;
        }
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

        if (animation != runningAnimation)
            SetAnimation(animation);
    }

    public bool AnimationRunning()
    {
        return animationRunning;
    }

    private void SetAnimation(string startAnim)
    {
        playerAnimator.ResetTrigger(runningAnimation);
        playerAnimator.SetTrigger(startAnim);
        runningAnimation = startAnim;
    }
}
