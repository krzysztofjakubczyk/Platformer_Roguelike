using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "newMeleeAttackStateData", menuName = "Data/State Data/Melee Attack State Data")]

public class MeleeAttackStateData : ScriptableObject
{
    [Range(1, 50)]
    public float attackRadius = 0.5f;
    [Range(1, 50)]
    public float attackDamage = 10f;
    [Range(1,50)]
    public float pushForce = 10f;
    public Vector2 vectorPush;

    public LayerMask whatIsPlayer;
}
