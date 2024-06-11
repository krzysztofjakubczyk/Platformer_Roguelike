using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTranistion : MonoBehaviour
{
    SceneLoadTrigger _loadTrigger;
    MoneyManager moneyManager;
    int howMoneyFromEnemy;
    [SerializeField] int howManyEnemies;

    private void Start()
    {
        Entity.OnEnemyDeath += killAnEnemy;
        moneyManager = FindAnyObjectByType<MoneyManager>();
        getEnemies();
        // Znajdü wy≥πczony obiekt LoadRoomTrigger
        findLoadTrigger();
    }

    private void findLoadTrigger()
    {
        _loadTrigger = GameObject.FindGameObjectWithTag("LoadRoomTrigger").GetComponent<SceneLoadTrigger>();
        print(_loadTrigger.name);

    }
    void killAnEnemy()
    {
        getEnemies();
        if (howManyEnemies == 1)
        {
            findLoadTrigger();
            moneyManager.AddMoney(howMoneyFromEnemy);
            _loadTrigger.GetComponent<BoxCollider2D>().enabled = true;
        }
    }

    private void getEnemies()
    {
        howManyEnemies = (GameObject.FindGameObjectsWithTag("Enemy").Length) / 2;
    }
}
