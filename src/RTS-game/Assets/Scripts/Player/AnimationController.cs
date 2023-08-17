using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    public Animator playerAnimator;

    private string runningAnimation;

    // Start is called before the first frame update
    void Start()
    {
        playerAnimator.SetTrigger("Idle");
        runningAnimation = "Idle";
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            CheckKeyReleased("Run");
            CheckKeyPressed("Run");
            Debug.Log("Run");
        }
        else
        {
            CheckKeyReleased("Walk");
            CheckKeyPressed("Walk");
        }
    }

    private void CheckKeyReleased(string speed)
    {
        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S))
        {
            if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
                SetAnimation(speed + "Right");
            else if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
                SetAnimation(speed + "Left");
            else
                SetAnimation("Idle");
        }
        else if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            if (Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S))
                SetAnimation(speed + "Forward");
            else if (Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W))
                SetAnimation(speed + "Backward");
            else
                SetAnimation("Idle");
        }
    }

    private void CheckKeyPressed(string speed)
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (Input.GetKey(KeyCode.D))
            {
                if (!Input.GetKey(KeyCode.A))
                    SetAnimation(speed + "ForwardRight");
                else
                    SetAnimation(speed + "Forward");
            }
            else if (Input.GetKey(KeyCode.A))
            {
                if (!Input.GetKey(KeyCode.D))
                    SetAnimation(speed + "ForwardLeft");
                else
                    SetAnimation(speed + "Forward");
            }
            else if (Input.GetKey(KeyCode.S))
                SetAnimation("Idle");
            else {
                Debug.Log(speed);
                SetAnimation(speed + "Forward");
            }
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            if (Input.GetKey(KeyCode.D))
            {
                if (!Input.GetKey(KeyCode.A))
                    SetAnimation(speed + "BackwardRight");
                else
                    SetAnimation(speed + "Backward");
            }
            else if (Input.GetKey(KeyCode.A))
            {
                if (!Input.GetKey(KeyCode.D))
                    SetAnimation(speed + "BackwardLeft");
                else
                    SetAnimation(speed + "Backward");
            }
            else if (Input.GetKey(KeyCode.W))
                SetAnimation("Idle");
            else
                SetAnimation(speed + "Backward");
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            if (Input.GetKey(KeyCode.W))
            {
                if (!Input.GetKey(KeyCode.S))
                    SetAnimation(speed + "ForwardRight");
                else
                    SetAnimation(speed + "Forward");
            }
            else if (Input.GetKey(KeyCode.S))
            {
                if (!Input.GetKey(KeyCode.W))
                    SetAnimation(speed + "BackwardRight");
                else
                    SetAnimation(speed + "Backward");
                SetAnimation(speed + "BackwardRight");
            }
            else if (Input.GetKey(KeyCode.A))
                SetAnimation("Idle");
            else
                SetAnimation(speed + "Right");
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            if (Input.GetKey(KeyCode.W))
            {
                if (!Input.GetKey(KeyCode.S))
                    SetAnimation(speed + "ForwardLeft");
                else
                    SetAnimation(speed + "Forward");
            }
            else if (Input.GetKey(KeyCode.S))
            {
                if (!Input.GetKey(KeyCode.W))
                    SetAnimation(speed + "BackwardLeft");
                else
                    SetAnimation(speed + "Backward");
            }
            else if (Input.GetKey(KeyCode.D))
                SetAnimation("Idle");
            else
                SetAnimation(speed + "Left");
        }
    }

    private void SetAnimation(string startAnim)
    {
        playerAnimator.ResetTrigger(runningAnimation);
        playerAnimator.SetTrigger(startAnim);
        runningAnimation = startAnim;
    }
}
