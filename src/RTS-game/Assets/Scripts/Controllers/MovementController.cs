using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController
{
    public Transform playersTransform;

    private float walkSpeed = 5, runSpeed = 10;
    private bool walking = false, runing = false, hit = false;
    private float speed, vertical, horizontal;

    public MovementController(Transform playersTransform)
    {
        this.playersTransform = playersTransform;
    }

    // physics
    public void UpdatePhysics()  //fixed update
    {
        float h = Input.GetAxisRaw("Horizontal");      // if 'd' then +1, if 'a' then -1, if none then 0
        float v = Input.GetAxisRaw("Vertical");          // if 'w' then +1, if 's' then -1, if none then 0

        Vector3 direction = new Vector3(h, 0f, v) * speed * Time.deltaTime;
        playersTransform.Translate(direction, Space.Self);
    }

    // animations
    public void UpdateValues() //update
    {
        hit = false;
        horizontal = Input.GetAxisRaw("Horizontal");      // if 'd' then +1, if 'a' then -1, if none then 0
        vertical = Input.GetAxisRaw("Vertical");          // if 'w' then +1, if 's' then -1, if none then 0

        // TODO: fix this
        if (horizontal != 0 || vertical != 0)
        {
            walking = true;
        }
        else
        {
            walking = false;
            if (Input.GetMouseButtonDown(0))
            {
                hit = true;
            }
        }

        // run
        if (walking && Input.GetKey(KeyCode.LeftShift))
        {
            speed = runSpeed;
            runing = true;
        }
        else if (walking)
        {
            speed = walkSpeed;
            runing = false;
        }
    }

    public bool GetRunning()
    {
        return runing;
    }

    public float GetHorizontal()
    {
        return horizontal;
    }

    public float GetVertical()
    {
        return vertical;
    }

    public bool GetHit()
    {
        return hit;
    }
}
