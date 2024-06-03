using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class SceneLoadTrigger : MonoBehaviour
{
    [SerializeField] MapTranistion mapInstance;
    [SerializeField] int indexOfSceneLoadedNow;
    [SerializeField] int indexOfSceneToSpawn;
    [SerializeField] int indexOfSceneToUnload;
    [SerializeField] int howManyScenesOnBuild;
    [SerializeField] int indexOfShopAtFirstFloor;
    [SerializeField] int indexOfShopAtSecondFloor;
    [SerializeField] List<int> IndexesOfScenesAtFirstFloor;
    [SerializeField] List<int> IndexesOfScenesAtSecondFloor;
    [SerializeField] bool isFirstFloor;
    GameObject _OutsideDoors;
    GameObject transitionTrigger;
    GameObject _InsideDoors;
    private GameObject _player;
    public Vector3 moveAmount;
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _OutsideDoors = GameObject.Find("OutDoors");
        if (mapInstance != null)
        {
            _InsideDoors = mapInstance.FindDisabledObjectByName<Transform>("InDoors")?.gameObject;
            transitionTrigger = mapInstance.FindDisabledObjectByName<Transform>("LoadCameraTrigger")?.gameObject;
        }
        howManyScenesOnBuild = SceneManager.sceneCountInBuildSettings;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (gameObject.name == "LoadRoomTrigger")
            {
                indexOfSceneLoadedNow = SceneManager.GetActiveScene().buildIndex;
                if (isFirstFloor && IndexesOfScenesAtFirstFloor.Count != 0)
                {
                    indexOfSceneToSpawn = Random.Range(IndexesOfScenesAtFirstFloor.First(), IndexesOfScenesAtFirstFloor.Last());
                    IndexesOfScenesAtFirstFloor.Remove(indexOfSceneToSpawn);
                }
                else if (isFirstFloor && IndexesOfScenesAtFirstFloor.Count == 0)
                {
                    isFirstFloor = false;
                }
                else if (!isFirstFloor && IndexesOfScenesAtSecondFloor.Count != 0)
                {
                    indexOfSceneToSpawn = Random.Range(IndexesOfScenesAtSecondFloor.First(), IndexesOfScenesAtSecondFloor.Last());
                    IndexesOfScenesAtSecondFloor.Remove(indexOfSceneToSpawn);
                }
                else if (!isFirstFloor && IndexesOfScenesAtSecondFloor.Count == 0)
                {
                    indexOfSceneToSpawn = 0; //SCENA MENU KONCOWEGO
                }
                LoadScene(indexOfSceneLoadedNow, indexOfSceneToSpawn);
            }
            else if (gameObject.name == "UnLoadRoomTrigger")
            {
                indexOfSceneLoadedNow = SceneManager.GetActiveScene().buildIndex;
                foreach (var scene in SceneManager.GetAllScenes())//przejdz po wszystkich scenach i te ktore sa nie aktywne to sa do odladowania- oczywiscie beda max dwie sceny
                {
                    if (scene != SceneManager.GetActiveScene())
                    {
                        indexOfSceneToUnload = scene.buildIndex; //do pozniejszego odladowania
                    }
                    UnLoadScene(indexOfSceneLoadedNow, indexOfSceneToUnload);
                }
            }
        }
    }

    private void LoadScene(int indexOfSceneLoadedNow, int indexOfSceneToSpawn)
    {
        _OutsideDoors.SetActive(false); // wy³¹cz drzwi, ¿eby mo¿na by³o przejœæ dalej
        transitionTrigger.SetActive(true);
        StartCoroutine(LoadAndSetActiveScene(indexOfSceneToSpawn));
    }

    private IEnumerator LoadAndSetActiveScene(int indexOfSceneToSpawn)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(indexOfSceneToSpawn, LoadSceneMode.Additive);
        yield return new WaitUntil(() => asyncLoad.isDone);
        Scene loadedScene = SceneManager.GetSceneByBuildIndex(indexOfSceneToSpawn);
        SceneManager.SetActiveScene(loadedScene);         // Ustaw za³adowan¹ scenê jako aktywn¹
        MoveScene(loadedScene);
        Destroy(gameObject);
    }
    private void UnLoadScene(int indexOfSceneLoadedNow, int indexOfSceneToUnload)
    {
        if (_InsideDoors != null)
        {
            _InsideDoors.SetActive(true); // w³¹cz drzwi, ¿eby nie mo¿na by³o siê cofn¹æ
        }
        SceneManager.UnloadSceneAsync(indexOfSceneToUnload);
        Destroy(gameObject);
    }
    private void MoveScene(Scene loadScene)
    {
        moveAmount = new Vector3(40,0,0);
        GameObject[] sceneObjects = loadScene.GetRootGameObjects();
        foreach (GameObject obj in sceneObjects)
        {
            obj.transform.position = obj.transform.position + moveAmount;
        }
    }
}
