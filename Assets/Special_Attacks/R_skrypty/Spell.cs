using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Spell : MonoBehaviour
{
    [HideInInspector]public GameObject player; // przypisywany w spellManager przed rzuceniem spella
    public float cost;
    public float damage;
    public float stunDamage;
    public float reachargeTime;
    [HideInInspector] public Vector2 castDirection;
    [HideInInspector] public Rigidbody2D rb;
    [SerializeField] public Items spellData;
    [SerializeField] protected ParticleSystem effect;
    protected void Start()
    {
        Instantiate(effect, transform);
    }
    public abstract void Attack();
}
