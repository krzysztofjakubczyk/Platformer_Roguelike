using UnityEngine;

public class MapTranistion : MonoBehaviour
{
    [SerializeField] int howManyEnemies;
    private GameObject transitionTrigger;
    private GameObject _OutsideDoors;
    private SceneController controller;
    private MoneyManager moneyManager;
    int howMoneyFromEnemy; // do wziecia z enemy dane


    private void Start()
    {
        Entity.OnEnemyDeath += WhenEnemyDead;
        controller = FindObjectOfType<SceneController>();
        moneyManager = FindAnyObjectByType<MoneyManager>();
        getEnemies();
    }
    void WhenEnemyDead()
    {
        getEnemies();
        if (howManyEnemies == 1)
        {
            controller.LoadScene();
            Invoke(nameof(LoadSceneElements), 2f);
        }
    }
    
    private void LoadSceneElements()
    {
        _OutsideDoors = GameObject.FindGameObjectWithTag("NewOutDoors");
        transitionTrigger = GameObject.FindGameObjectWithTag("LoadCameraTrigger");
        moneyManager.AddMoney(howMoneyFromEnemy);
        transitionTrigger.GetComponent<BoxCollider2D>().enabled = true;
        _OutsideDoors.SetActive(false);
        _OutsideDoors.tag = "OutDoors";
    }
    private void getEnemies()
    {
        howManyEnemies = (GameObject.FindGameObjectsWithTag("Enemy").Length) / 2;
    }
    
}
