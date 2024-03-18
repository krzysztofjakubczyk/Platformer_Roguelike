using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : ScriptableObject
{
    public string name;
    public float cost;
    public float damage;

    public virtual void Activate(GameObject parent) { }
}
