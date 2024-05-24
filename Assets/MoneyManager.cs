using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    private int money;

    public void AddMoney(int howAdd) 
    { 
        money += howAdd; 
    }

    public void SubMoney(int howSubstract) { 
        money -= howSubstract; 
    }

    public int GetMoney() { return money; }
}
