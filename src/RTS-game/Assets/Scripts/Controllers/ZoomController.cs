using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomController : MonoBehaviour
{
    public Transform cameraTransform, cameraAnchorTransform;
    public float zoomLevel = -5, sensitivity = 1, speed = 30, minZoom = -10, maxZoom = 0, autoZoomSensitivity = .2f;

    private float zoomPosition, prevZoomLevel;
    private bool zoomed = false;

    void CheckInTheWay()
    {
        Ray ray = new Ray(cameraAnchorTransform.position, -cameraTransform.forward);
        if (Physics.SphereCast(ray, 1, out RaycastHit hit, -minZoom))
        {
            if (hit.distance < Mathf.Abs(zoomLevel + autoZoomSensitivity))
            {
                if (!zoomed)
                {
                    zoomed = true;
                    prevZoomLevel = zoomLevel;
                }
                zoomLevel += autoZoomSensitivity;
            }
            else if (zoomed && hit.distance >= Mathf.Abs(zoomLevel - autoZoomSensitivity))
            {
                zoomLevel -= autoZoomSensitivity;
                if (zoomLevel <= prevZoomLevel)
                    zoomed = false;
            }
        }
    }

    void Update()
    {
        zoomLevel += Input.GetAxis("Mouse ScrollWheel") * sensitivity;
        CheckInTheWay();
        zoomLevel = Mathf.Clamp(zoomLevel, minZoom, maxZoom);
        zoomPosition = Mathf.MoveTowards(zoomPosition, zoomLevel, speed * Time.deltaTime);
        cameraTransform.position = cameraAnchorTransform.position + (cameraTransform.forward * zoomPosition);
    }
}
