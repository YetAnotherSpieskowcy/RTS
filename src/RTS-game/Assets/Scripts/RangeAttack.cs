using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeAttack : MonoBehaviour
{
    public float hit50 = .08f;
    public float hit100 = .25f;
    public float maxRange = 300.0f;
    [SerializeField] private GameObject arrow;
    void OnValidate()
    {
        hit50 = hit50 > 0 ? hit50 : 0;
        hit50 = hit50 > hit100 ? hit100 : hit50;
        hit100 = hit100 > 0 ? hit100 : 0;
    }
    void OnDrawGizmos()
    {
        GizmosGeometry.DrawCircle(hit50, Color.red, transform.position + transform.forward, transform.forward);
        GizmosGeometry.DrawCircle(hit100, Color.yellow, transform.position + transform.forward, transform.forward);
    }

    public void Shoot()
    {
        float r = (Random.value < .5f ? hit50 : hit100) * (float)System.Math.Sqrt(Random.value);
        float th = Random.value * 2 * (float)System.Math.PI;
        Vector3 dst = new Vector3((float)(r * System.Math.Sin(th)), (float)(r * System.Math.Cos(th)), 0);
        // TODO Play animation
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward + transform.TransformDirection(dst), out hit, maxRange))
        {
            GameObject go = Instantiate(arrow, hit.point, Quaternion.identity);
            go.transform.LookAt(transform.position, Vector3.up);
            go.transform.parent = hit.transform.root; // FIXME will break in case of animations
            hit.transform.localScale = new Vector3(1/go.transform.localScale.x,1/go.transform.localScale.y,1/go.transform.localScale.z);
            Unit unit = hit.transform.GetComponentInParent<Unit>();
            if (unit != null)
            {
                unit.Hit(1);
            }
        }
    }
}
