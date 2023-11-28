using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController
{
    public Transform playersTransform;

    private float walkSpeed = 5, runSpeed = 10;
    private bool walking = false, runing = false, hit = false;
    private float speed, vertical, horizontal;

    private CombatController combatController;

    public MovementController(Transform playersTransform)
    {
        this.playersTransform = playersTransform;
        combatController = new CombatController();
    }

    // physics
    public void UpdatePhysics(bool punchRunning, bool commandRunning)  //fixed update
    {
        float h = 0f, v = 0f;
        if (Input.GetMouseButtonDown(0) && !punchRunning && !commandRunning)
        {
            hit = true;
            combatController.CheckAttack(playersTransform.position + new Vector3(0f, 1f, 0f), playersTransform.forward);
        }
        else if (!punchRunning)
        {
            hit = false;
            h = Input.GetAxisRaw("Horizontal");      // if 'd' then +1, if 'a' then -1, if none then 0
            v = Input.GetAxisRaw("Vertical");          // if 'w' then +1, if 's' then -1, if none then 0
            if (h != 0 && v != 0)
            {
                h /= Mathf.Sqrt(2);
                v /= Mathf.Sqrt(2);
            }
        }
        Vector3 direction = new Vector3(h, 0f, v) * speed * Time.deltaTime;
        playersTransform.Translate(direction, Space.Self);
    }

    // animations
    public void UpdateValues() //update
    {
        horizontal = Input.GetAxisRaw("Horizontal");      // if 'd' then +1, if 'a' then -1, if none then 0
        vertical = Input.GetAxisRaw("Vertical");          // if 'w' then +1, if 's' then -1, if none then 0

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
