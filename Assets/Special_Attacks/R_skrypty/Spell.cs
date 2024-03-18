using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Spell : MonoBehaviour
{

    [HideInInspector]public GameObject player;
    public float cost;
    public float damage;
    [HideInInspector] public Vector2 castDirection;
    [HideInInspector] public Rigidbody2D rb;

    public abstract void Attack();
}
