using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{

    public Rigidbody playersRigidbody;
    public Transform playersTransform;
    public float walk_speed = 100, run_speed = 200, rotation_speed = .1f;

    private bool walking;
    private float speed;
    private float turnSmooth;
    private string runningAnimation;

    // physics
    void FixedUpdate()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        float alpha = playersTransform.eulerAngles.y * Mathf.PI / 180;
        float vel_x = 0, vel_z = 0;

        Debug.Log(alpha);

        if(horizontal != 0 && vertical != 0)
        {
            vel_x = horizontal * speed * Mathf.Sin(alpha + Mathf.PI / 4) * Time.deltaTime;
            vel_z = vertical * speed * Mathf.Cos(alpha + horizontal * Mathf.PI / 4) * Time.deltaTime;

            switch (alpha)
            {
                case < Mathf.PI / 2:
                    break;
                case < Mathf.PI:
                    vel_z *= -1;
                    break;
                case < 3 * Mathf.PI / 2:
                    vel_z *= -1;
                    vel_x *= -1;
                    break;
                case < 2 * Mathf.PI:
                    vel_x *= -1;
                    break;
            }
        }
        else if (horizontal != 0)
        {
            vel_x = horizontal * speed * Mathf.Sin(alpha + Mathf.PI / 2) * Time.deltaTime;
            vel_z = horizontal * speed * Mathf.Cos(alpha + Mathf.PI / 2) * Time.deltaTime;
        }
        else if (vertical != 0)
        {
            vel_x = vertical * speed * Mathf.Sin(alpha) * Time.deltaTime;
            vel_z = vertical * speed * Mathf.Cos(alpha) * Time.deltaTime;
            Debug.Log(Mathf.Sin(alpha));
        }

        playersTransform.Rotate(new Vector3(0, Input.GetAxis("Mouse X") * sensitivity, 0));
        playersRigidbody.velocity = new Vector3(vel_x, 0, vel_z);
    }

    public float sensitivity = 10f;

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
            speed = run_speed;
        }
        else if (walking)
        {
            // stop animation run
            speed = walk_speed;
        }
    }

    private void SetAnimation(string startAnim)
    {
        // playerAnim.ResetTrigger(runningAnimation);
        // playerAnimation.SetTrigger(startAnim);
        runningAnimation = startAnim;
    }
}
