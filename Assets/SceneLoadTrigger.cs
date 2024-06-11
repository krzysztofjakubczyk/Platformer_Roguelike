using UnityEngine;
using UnityEngine.Tilemaps;

public class SceneUnLoadTrigger : MonoBehaviour
{
    private GameObject _InsideDoors;
    private SceneController controller;
    private void Start()
    {
        controller = FindObjectOfType<SceneController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && gameObject.CompareTag("UnLoadRoomTrigger"))
        {
            UnLoadSceneElements();
            gameObject.SetActive(false);
        }
    }
    private void UnLoadSceneElements()
    {
        controller.UnLoadScene();
        Invoke(nameof(UnloadFunction), 2f);
    }
    private void UnloadFunction()
    {
        _InsideDoors = GameObject.FindGameObjectWithTag("NewInDoors");
        _InsideDoors.GetComponent<TilemapRenderer>().enabled = true;
        _InsideDoors.GetComponent<TilemapCollider2D>().enabled = true;
        _InsideDoors.tag = "InDoors";
    }
}
