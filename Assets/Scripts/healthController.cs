using System;
using UnityEngine;

public class healthController : MonoBehaviour
{
    public static Action DiscardHP;
    int m_health = 10;
    [SerializeField] int health;
    private void Start()
    {
        health = m_health;
    }
    public void MinusHP(int howToDiscard)
    {
        DiscardHP();
        health -= howToDiscard;
    }
    public int GetHealth() { return health; }
    public int GetMaxHealth() { return m_health; }
    public void RestartHealth() { health = m_health; }
}
