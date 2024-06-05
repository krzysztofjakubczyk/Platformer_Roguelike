using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadTrigger : MonoBehaviour
{
    private GameObject _OutsideDoors;
    private GameObject transitionTrigger;
    private GameObject LoadRoomTriiger;
    private GameObject _InsideDoors;
    SceneController controller;
    [SerializeField] MapTranistion mapInstance;
    private void Start()
    {
        controller = FindObjectOfType<SceneController>();
        mapInstance = FindObjectOfType<MapTranistion>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (gameObject.name == "LoadRoomTrigger")
            {
                controller.LoadScene();
                FindNewGameObjectsOnScene();
                transitionTrigger.SetActive(true);
                _OutsideDoors.SetActive(false);
                Destroy(gameObject);
            }
            else if (gameObject.name == "UnLoadRoomTrigger")
            {
                controller.UnLoadScene();
                FindNewGameObjectsOnScene();
                _InsideDoors.SetActive(true);
                Destroy(gameObject);
            }
        }
    }
    private void FindNewGameObjectsOnScene()
    {
        _OutsideDoors = GameObject.Find("OutDoors");
        if (mapInstance != null)
        {
            _InsideDoors = mapInstance.FindDisabledObjectByName<Transform>("InDoors")?.gameObject;
            transitionTrigger = mapInstance.FindDisabledObjectByName<Transform>("LoadCameraTrigger")?.gameObject;
        }
    }
}
