using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] private int health = 100;
    public void Hit(int damage)
    {
        health -= damage;
        Debug.Log("Bonk!");
    }
}
