using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

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
    [SerializeField] bool isThisFirstFloor;
    GameObject _OutsideDoors;
    GameObject _InsideDoors;
    private GameObject _player;

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _OutsideDoors = GameObject.Find("OutDoors");
        if (mapInstance != null)
        {
            _InsideDoors = mapInstance.FindDisabledObjectByName<Transform>("InDoors")?.gameObject;
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
                if (isThisFirstFloor)
                {
                    indexOfSceneToSpawn = Random.Range(IndexesOfScenesAtFirstFloor.First(), IndexesOfScenesAtFirstFloor.Last());
                    IndexesOfScenesAtFirstFloor.Remove(indexOfSceneToSpawn);
                }
                else
                {
                    indexOfSceneToSpawn = Random.Range(IndexesOfScenesAtSecondFloor.First(), IndexesOfScenesAtSecondFloor.Last());
                    IndexesOfScenesAtFirstFloor.Remove(indexOfSceneToSpawn);
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
        StartCoroutine(LoadAndSetActiveScene(indexOfSceneToSpawn));
    }

    private IEnumerator LoadAndSetActiveScene(int indexOfSceneToSpawn)
    {
        // SprawdŸ, czy scena jest ju¿ za³adowana
        if (!IsSceneLoaded(indexOfSceneToSpawn))
        {
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(indexOfSceneToSpawn, LoadSceneMode.Additive);
            yield return new WaitUntil(() => asyncLoad.isDone);
        }
        Debug.Log("Ustaw aktywna");
        // Ustaw za³adowan¹ scenê jako aktywn¹
        Scene loadedScene = SceneManager.GetSceneByBuildIndex(indexOfSceneToSpawn);
        SceneManager.SetActiveScene(loadedScene);
        Debug.Log("Active Scene: " + SceneManager.GetActiveScene().name);
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
    private bool IsSceneLoaded(int buildIndex)
    {
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            Scene scene = SceneManager.GetSceneAt(i);
            if (scene.buildIndex == buildIndex)
            {
                return true;
            }
        }
        return false;
    }
}
