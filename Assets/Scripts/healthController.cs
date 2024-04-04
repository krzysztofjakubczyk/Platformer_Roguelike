using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthController : MonoBehaviour
{

    int m_health = 10;
    [SerializeField]
    int health;
    private void Start()
    {
        health = m_health;
    }
    public void MinusHP(int howToDiscard)
    {
        health -= howToDiscard;
    }
    public int GetHealth() { return health; }
    public void RestartHealth() { health = m_health; }
}
