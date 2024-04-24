using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "newPlayerDetectedStateData", menuName = "Data/State Data/Back To Patrol State Data")]


public class BackToPatrolStateData : ScriptableObject
{
    public Vector3 patrolPoint;
    public float backSpeed;
}
