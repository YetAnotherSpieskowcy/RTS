using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    private const float jumpTime = .9f;

    public Transform playersTransform;
    public Animator playerAnimator;
    public float walkSpeed = 5, runSpeed = 7;

    private AnimationController animationController;
    private bool walking = false, jumping = false;
    private float speed, jumpDuration;

    void Start()
    {
        animationController = new AnimationController(playerAnimator);
        animationController.InitializeAnimation("Idle");
    }

    // physics
    void FixedUpdate()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");      // if 'd' then +1, if 'a' then -1, if none then 0
        float vertical = Input.GetAxisRaw("Vertical");          // if 'w' then +1, if 's' then -1, if none then 0
        float jump = Input.GetAxisRaw("Jump");

        /*if (jump == 1 && !jumping)
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
        }*/

        Vector3 direction = new Vector3(horizontal, (jumping ? 1f : 0f), vertical) * speed * Time.deltaTime;
        playersTransform.Translate(direction, Space.Self);
    }

    // animations
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");      // if 'd' then +1, if 'a' then -1, if none then 0
        float vertical = Input.GetAxisRaw("Vertical");          // if 'w' then +1, if 's' then -1, if none then 0

        // TODO: fix this
        if (horizontal != 0 || vertical != 0)
        {
            walking = true;
        }
        else
        {
            walking = false;
        }

        // run
        if (walking && Input.GetKey(KeyCode.LeftShift))
        {
            speed = runSpeed;
        }
        else if (walking)
        {
            speed = walkSpeed;
        }

        animationController.ChooseAnimation((speed == runSpeed), vertical, horizontal);
    }
}
