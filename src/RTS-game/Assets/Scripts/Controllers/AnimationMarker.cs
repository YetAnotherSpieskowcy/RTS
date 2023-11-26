using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationMarker : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    { }

    public void MarkAnimationEnded()
    {
        Debug.Log("reseting");
        PlayerController c = GameObject.Find("Player").GetComponent<PlayerController>();
        c.MarkAnimationEnded();
    }
}
