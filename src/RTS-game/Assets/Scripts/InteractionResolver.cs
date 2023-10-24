using UnityEngine;

public class InteractionResolver : MonoBehaviour
{
    public float interactionDistance = 1.0f;
    void Update()
    {
        RaycastHit hit;
        LayerMask mask;
        mask = LayerMask.GetMask("Player") ^ int.MaxValue;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, interactionDistance, mask))
        {
            Interactable inter;
            if (hit.collider.TryGetComponent<Interactable>(out inter))
            {
                if (Input.GetKeyDown(InputSettings.Interact))
                {
                    inter.Interact();
                }
                // TODO Draw tooltip
                Debug.Log(inter.GetTooltipInfo());
            }

        }

    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + transform.TransformDirection(Vector3.forward) * interactionDistance);
    }
}