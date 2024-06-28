using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    [SerializeField] int money;
    private void Start()
    {
        Coin.addMoney += AddMoney;
    }
    public void AddMoney(int Add) 
    { 
        money += Add; 
    }

    public void SubMoney(int Substract) { 
        money -= Substract; 
    }

    public int GetMoney() { return money; }
}
