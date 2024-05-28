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
        // ZnajdŸ wy³¹czony obiekt LoadRoomTrigger
        _loadTrigger = FindDisabledObjectByName<SceneLoadTrigger>("LoadRoomTrigger");
    }

    void killAnEnemy()
    {
        getEnemies();
        if (howManyEnemies == 1)
        {
            moneyManager.AddMoney(howMoneyFromEnemy);
            if (_loadTrigger != null)
            {
                _loadTrigger.gameObject.SetActive(true);
            }
        }
    }

    private void getEnemies()
    {
        howManyEnemies = (GameObject.FindGameObjectsWithTag("Enemy").Length) / 2;
    }

    // Metoda do znajdowania wy³¹czonych obiektów w scenie
    public T FindDisabledObjectByName<T>(string name) where T : Component
    {
        foreach (T obj in Resources.FindObjectsOfTypeAll<T>())
        {
            if (obj.gameObject.name == name)
            {
                return obj;
            }
        }
        return null;
    }

}
