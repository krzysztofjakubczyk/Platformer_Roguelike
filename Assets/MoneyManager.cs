using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MoneyManager : MonoBehaviour
{
    public Action updateGUI;
    [SerializeField] int money;
    private void Start()
    {
        Coin.addMoney += AddMoney;
    }
    public void AddMoney(int Add) 
    { 
        money += Add;
        updateGUI?.Invoke();
    }

    public void SubMoney(int Substract) { 
        money -= Substract; 
    }

    public int GetMoney() {
        //updateGUI?.Invoke();
        return money; }
}
