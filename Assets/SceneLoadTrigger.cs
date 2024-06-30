using UnityEngine;
using UnityEngine.Tilemaps;

public class SceneUnLoadTrigger : MonoBehaviour
{
    [SerializeField] private GameObject _InsideDoors;
    private SceneController controller;
    private void Start()
    {
        controller = FindObjectOfType<SceneController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
        {
            return;
        }
        if (gameObject.CompareTag("UnLoadRoomTrigger"))
        {
            UnLoadSceneElements();
            Destroy(gameObject);
        }
    }
    private void UnLoadSceneElements()
    {
        _InsideDoors.GetComponent<Tilemap>().enabled = true;
        _InsideDoors.GetComponent<TilemapRenderer>().enabled = true;
        _InsideDoors.GetComponent<TilemapCollider2D>().enabled = true;
        _InsideDoors.GetComponent<PlatformEffector2D>().enabled = true;
       controller.UnLoadScene();
    }
}
