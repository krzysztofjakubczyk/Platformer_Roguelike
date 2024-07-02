using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapTranistion : MonoBehaviour
{
    [SerializeField] int howManyEnemies;
    private GameObject transitionTrigger;
    private GameObject _OutsideDoors;
    [SerializeField] private SceneController controller;
    private MoneyManager moneyManager;
    [SerializeField]private GameObject UiCanvas;
    int howMoneyFromEnemy; // do wziecia z enemy dane
    private void Start()
    {
        moneyManager = FindAnyObjectByType<MoneyManager>();
        getEnemies();
    }
    public void WhenEnemyDead(bool isSecondBossKilled)
    {
        getEnemies();
        if (howManyEnemies == 1 && isSecondBossKilled == false)
        {
            controller.LoadScene();
            Invoke(nameof(LoadSceneElements), 1f);
        }
        else if (isSecondBossKilled)
        {
            SceneManager.LoadScene(11);
            UiCanvas.SetActive(false);
        }
    }
    private void LoadSceneElements()
    {
        _OutsideDoors = GameObject.FindGameObjectWithTag("NewOutDoors");
        transitionTrigger = GameObject.FindGameObjectWithTag("LoadCameraTrigger");
        moneyManager.AddMoney(howMoneyFromEnemy);
        transitionTrigger.GetComponent<BoxCollider2D>().enabled = true;
        Debug.Log(transitionTrigger.GetComponent<BoxCollider2D>());
        _OutsideDoors.SetActive(false);
        _OutsideDoors.tag = "OutDoors";
    }
    private void getEnemies()
    {
        howManyEnemies = (GameObject.FindGameObjectsWithTag("Enemy").Length) / 2;
    }
    
}
