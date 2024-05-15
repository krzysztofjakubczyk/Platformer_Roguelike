using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "newEntityData",menuName = "Data/Entity Data/Base Data")]
public class EntityData : ScriptableObject
{
    public float maxHealth = 30f;

    public float damageHopSpeed = 10f;

    public float wallCheckDistance = 0.2f;
    public float LedgeCheckDistance= 1f;
    public float groundCheckRadius = 0.3f;

    public float minAgroDistance = 7f;
    public float minEnemyAgroDistance = 3f;
    public float maxAgroDistance = 8f;

    public float stunResistance = 3f;
    public float stunRecoveryTime = 2f;
    public float closeRangeActionDistance = 1f;

    public GameObject hitParticle;

    public LayerMask WhatIsGround;
    public LayerMask WhatIsPlayer;
    public LayerMask WhatIsEnemy;
}
