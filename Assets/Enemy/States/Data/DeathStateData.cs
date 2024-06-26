using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "newDeathStateData", menuName = "Data/State Data/Death State Data")]

public class DeathStateData :ScriptableObject
{
    public GameObject deathChunkParticle;
    public GameObject deathBloodParticle;
}
