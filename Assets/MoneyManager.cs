using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MoneyManager : MonoBehaviour
{
    [SerializeField] List<GameObject> coinList;
    public Action updateGUI;
    [SerializeField] int money;
    private void Start()
    {
        Coin.addMoney += AddMoney;
        DeathState.setPosition += moneyRoulette;
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
        return money; 
    }
    private void moneyRoulette(Vector3 pos)
    {
        // indeks 0 to zloty coin(5), id 1 to srebrny(2) i id 2 to br¹zowy(1)
        int goldCoins = UnityEngine.Random.Range(0, 2);
        int silverCoins = UnityEngine.Random.Range(0, 4);
        int bronzeCoins = UnityEngine.Random.Range(0, 6);
        List<int> instanceCounter = new List<int>();
        instanceCounter.Add(goldCoins);
        instanceCounter.Add(silverCoins);
        instanceCounter.Add(bronzeCoins);
        int j = 0;
        foreach (var item in instanceCounter) {
            if (item == 0) continue;
            for (int i = 0; i < item; i++)
            {
                print(item);
                Instantiate(coinList[j], pos, Quaternion.identity);
            }
        }
    
    }
}
