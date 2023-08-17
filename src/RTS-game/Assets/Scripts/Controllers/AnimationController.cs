using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController
{
    public Animator playerAnimator;

    private string runningAnimation;

    public AnimationController(Animator animator)
    {
        playerAnimator = animator;
    }

    public void InitializeAnimation(string animation)
    {
        playerAnimator.SetTrigger(animation);
        runningAnimation = animation;
    }

    public void ChooseAnimation(bool running, float vertical, float horizontal)
    {
        string animation, speed, v, h;

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

    private void SetAnimation(string startAnim)
    {
        playerAnimator.ResetTrigger(runningAnimation);
        playerAnimator.SetTrigger(startAnim);
        runningAnimation = startAnim;
    }
}
