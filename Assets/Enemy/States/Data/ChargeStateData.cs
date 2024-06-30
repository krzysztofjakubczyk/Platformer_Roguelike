using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newChargeStateData", menuName = "Data/State Data/Charge State Data")]
public class ChargeStateData : ScriptableObject
{
    public float chargeSpeed = 6f;

    public float chargeTime = 2.5f;

    public float distanceToStop = 0.5f;
}
