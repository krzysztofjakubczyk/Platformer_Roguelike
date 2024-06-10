using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadTrigger : MonoBehaviour
{
    private GameObject _OutsideDoors;
    private GameObject transitionTrigger;
    private GameObject LoadRoomTrigger;
    private GameObject _InsideDoors;
    private SceneController controller;
    [SerializeField] private MapTranistion mapInstance;
    private BoundingShapeScripts boundingShapeScripts;

    private void Start()
    {
        controller = FindObjectOfType<SceneController>();
        mapInstance = FindObjectOfType<MapTranistion>();
        boundingShapeScripts = FindObjectOfType<BoundingShapeScripts>();
        FindNewGameObjectsOnScene();
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
                gameObject.SetActive(false);
            }
            else if (gameObject.name == "UnLoadRoomTrigger")
            {
                Debug.Log("Od³adowano scenê");
                controller.UnLoadScene();
                FindNewGameObjectsOnScene();
                _InsideDoors.SetActive(true);
                gameObject.SetActive(false);
            }
        }
    }
    private void FindNewGameObjectsOnScene()
    {
        mapInstance = FindObjectOfType<MapTranistion>();
        _OutsideDoors = GameObject.Find("OutDoors");
        if (mapInstance != null)
        {
            _InsideDoors = mapInstance.FindDisabledObjectByName<Transform>("InDoors")?.gameObject;
            transitionTrigger = mapInstance.FindDisabledObjectByName<Transform>("LoadCameraTrigger")?.gameObject;
        }
    }
}
