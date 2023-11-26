using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationController : MonoBehaviour
{
    public Transform playersTransform, cameraAnchorTransform;
    public float rotationSpeed = 10f;

    private float mouseX, mouseY;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void LateUpdate()
    {
        if (!GetComponent<Unit>().IsAlive())
            return;
        mouseX += Input.GetAxis("Mouse X");
        mouseY -= Input.GetAxis("Mouse Y");
        mouseY = Mathf.Clamp(mouseY, -15, 60);   // prevent camera from making flips

        playersTransform.rotation = Quaternion.Euler(0f, mouseX * rotationSpeed, 0f);
        cameraAnchorTransform.rotation = Quaternion.Euler(mouseY, mouseX * rotationSpeed, 0f);
    }
}
