using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStats", menuName = "ScriptableObjects/PlayerStats", order = 1)]

public class PlayerStatsFin : ScriptableObject
{
    [System.Serializable]
    public class Stat
    {
        public PlayerStatEnum statName;
        public float value;
    }

    public Stat[] stats;
}

public enum PlayerStatEnum
{
    hpMax, hpCurrent, hpRecoverTime,
    manaMax, manaCurrent, manaRecoverTime,
    damage, attackSpeed,
    fireballDmg, fireballSpeed,
    lightningDmg, lightningSpeed,
    grabDmg, grabSpeed,
    laserDmg, laserSpeed,
    grenadeDmg, grenadeSpeed,
    jumpHeight, moveSpeed,
    dashPower
}