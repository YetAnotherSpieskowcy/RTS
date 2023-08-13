using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    private const float jumpTime = 1f;

    public Transform playersTransform;
    public float walkSpeed = 5, runSpeed = 7;

    private bool walking = false, jumping = false;
    private float speed, jumpDuration;
    private string runningAnimation;

    // physics
    void FixedUpdate()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");      // if 'd' then +1, if 'a' then -1, if none then 0
        float vertical = Input.GetAxisRaw("Vertical");          // if 'w' then +1, if 's' then -1, if none then 0
        float jump = Input.GetAxisRaw("Jump");

        if(jump == 1 && !jumping)
        {
            jumping = true;
            jumpDuration = 0f;
        }
        else if (jumping && jumpDuration < jumpTime)
        {
            jumpDuration += Time.deltaTime;
        }
        else
        {
            jumping = false;
        }

        Vector3 direction = new Vector3(horizontal, (jumping ? 1f : 0f), vertical) * speed * Time.deltaTime;
        playersTransform.Translate(direction, Space.Self);
    }

    // animations
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            walking = true;
        }
        else if (Input.GetKeyUp(KeyCode.W))
        {
            walking = false;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            walking = true;
        }
        else if (Input.GetKeyUp(KeyCode.S))
        {
            walking = false;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            walking = true;
        }
        else if (Input.GetKeyUp(KeyCode.A))
        {
            walking = false;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            walking = true;
        }
        else if (Input.GetKeyUp(KeyCode.D))
        {
            walking = false;
        }
        // run
        if (walking && Input.GetKey(KeyCode.LeftShift))
        {
            // animation run
            speed = runSpeed;
        }
        else if (walking)
        {
            // stop animation run
            speed = walkSpeed;
        }
    }

    private void SetAnimation(string startAnim)
    {
        // playerAnim.ResetTrigger(runningAnimation);
        // playerAnimation.SetTrigger(startAnim);
        runningAnimation = startAnim;
    }
}
