using UnityEngine;
using UnityEngine.Tilemaps;

public class SceneLoadTrigger : MonoBehaviour
{
    private GameObject _OutsideDoors;
    private GameObject transitionTrigger;
    private GameObject LoadRoomTrigger;
    private GameObject _InsideDoors;
    private SceneController controller;

    private void Start()
    {
        controller = FindObjectOfType<SceneController>();
        FindNewGameObjectsOnScene();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (gameObject.name == "LoadRoomTrigger")
            {
                LoadSceneElements();
            }
            else if (gameObject.name == "UnLoadRoomTrigger")
            {
                UnLoadSceneElements();
            }
        }
    }
    private void LoadSceneElements()
    {
        controller.LoadScene();
        Invoke("LoadActions", 3f);
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
    }

    private void UnLoadSceneElements()
    {
            controller.UnLoadScene();
            Invoke("UnloadActions", 3f);
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
    }
    private void UnloadActions()
    {
        FindNewGameObjectsOnScene();
        _InsideDoors.GetComponent<TilemapRenderer>().enabled = true;
        _InsideDoors.GetComponent<TilemapCollider2D>().enabled = true;
        _InsideDoors.GetComponent<PlatformEffector2D>().enabled = true;
        _InsideDoors.tag = "InDoors";
    }
    private void LoadActions()
    {
        FindNewGameObjectsOnScene();
        transitionTrigger.GetComponent<BoxCollider2D>().enabled = true;
        _OutsideDoors.SetActive(false);
        _OutsideDoors.tag = "OutDoors";
    }
    private void FindNewGameObjectsOnScene()
    {
        _OutsideDoors = GameObject.FindGameObjectWithTag("NewOutDoors");
        _InsideDoors = GameObject.FindGameObjectWithTag("NewInDoors");
        transitionTrigger = GameObject.FindGameObjectWithTag("LoadCameraTrigger");
    }
}
