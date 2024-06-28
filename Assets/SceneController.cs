using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [SerializeField] private int shopSceneIndex;
    [SerializeField] private int lastIndexOfSceneSpawned = 0;
    [SerializeField] private Vector3 moveAmount = new Vector3(37, 0, 0);
    [SerializeField] private List<int> floorSizes;
    [SerializeField] private List<int> indexForBossScene;
    [SerializeField] private List<int> loadedSceneIndexes = new List<int>();
    [SerializeField] private MapTranistion mapInstance;
    [SerializeField] private Dictionary<int, List<int>> floorSceneIndexes = new Dictionary<int, List<int>>();
    [SerializeField] private GameObject player;
    [SerializeField] private int indexOfDeathScene;
    //public Action changeEnemyPos;

    private const int minFloorSize = 2;
    private const int maxFloorSize = 5;
    public int currentFloor = 0;
    public int loadedScenes;
    private int indexOfSceneToSpawn;
    private Vector3 VectorOfYPostionFirstScene;
    private List<int> shopInsertedFloors = new List<int>();
    [SerializeField] private bool isShopLoaded = false;
    [SerializeField] private bool hasShopBeenLoaded = false;
    float lastPosition = 0f;
    void Start()
    {
        loadedSceneIndexes.Add(0);
        if (moveAmount == Vector3.zero)
        {
            moveAmount = new Vector3(37, 0, 0);
        }
        InitializeFloors();

    }

    private void InitializeFloors()
    {
        for (int floor = 0; floor < floorSizes.Count; floor++)
        {
            InitializeFloorScenes(floor);
        }
    }

    private void InitializeFloorScenes(int floor)
    {
        int floorSize = UnityEngine.Random.Range(minFloorSize, maxFloorSize);
        floorSizes[floor] = floorSize;
        var sceneIndexes = new HashSet<int>();

        for (int i = 0; i < floorSize; i++)
        {
            int randomIndex;
            do
            {
                randomIndex = UnityEngine.Random.Range(1, 5);
            }
            while (sceneIndexes.Contains(randomIndex));

            if (!floorSceneIndexes.ContainsKey(floor))
            {
                floorSceneIndexes[floor] = new List<int>();
            }

            floorSceneIndexes[floor].Add(randomIndex);
            sceneIndexes.Add(randomIndex);
        }

        // Dodanie sklepu na losowej pozycji, ale nie pierwszej ani ostatniej
        if (!shopInsertedFloors.Contains(floor))
        {
            int shopPosition = UnityEngine.Random.Range(1, floorSize - 1); // Zapewnia, ¿e sklep nie bêdzie ani pierwszy, ani ostatni
            floorSceneIndexes[floor].Insert(shopPosition, shopSceneIndex);
            shopInsertedFloors.Add(floor);
        }

        floorSceneIndexes[floor].Add(indexForBossScene[floor]);
        Debug.Log("BOSS SCENE NUMBER " + floorSceneIndexes[floor].Last());
    }

    public void LoadScene()
    {
        indexOfSceneToSpawn = floorSceneIndexes[currentFloor].First();
        if (IsSceneAlreadyLoaded(indexOfSceneToSpawn))
        {
            return;
        }
        if (indexOfSceneToSpawn == shopSceneIndex)
        {
            StartCoroutine(LoadSceneWithDelayCoroutine(5f));
        }
        StartCoroutine(LoadSceneCoroutine());
    }

    public void onPlayersDeath()
    {
        SceneManager.LoadScene(indexOfDeathScene);
    }
    private IEnumerator LoadSceneCoroutine()
    {
        floorSceneIndexes[currentFloor].Remove(indexOfSceneToSpawn);
        loadedScenes++;
        loadedSceneIndexes.Add(indexOfSceneToSpawn);
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(indexOfSceneToSpawn, LoadSceneMode.Additive);
        yield return asyncLoad;
        Scene scene = SceneManager.GetSceneByBuildIndex(indexOfSceneToSpawn);
        MoveScene(scene);
        SceneManager.SetActiveScene(scene);
        //changeEnemyPos?.Invoke();
    }

    public void UnLoadScene()
    {
        int sceneIndexToUnload = loadedSceneIndexes.First();
        Debug.Log(sceneIndexToUnload + "UNLOADSCENE");
        SceneManager.UnloadSceneAsync(sceneIndexToUnload);
        loadedSceneIndexes.Remove(sceneIndexToUnload);
    }

    private bool IsSceneAlreadyLoaded(int sceneIndex)
    {
        foreach (var scene in SceneManager.GetAllScenes())
        {
            if (scene.buildIndex == sceneIndex)
            {
                return true;
            }
        }
        return false;
    }

    private void MoveScene(Scene loadScene)
    {

        GameObject[] sceneObjects = loadScene.GetRootGameObjects();
        if (sceneObjects.Length > 0)
        {

            GameObject sceneObject = sceneObjects[0];
            Vector3 originalPosition = sceneObject.transform.position;
            // Jeœli w³aœnie za³adowano sklep, ustaw moveAmount na 23 dla nastêpnej sceny
            Debug.Log("MOVE SCENE + IS SHOP LOADED" + isShopLoaded);
            if (isShopLoaded)
            {
                hasShopBeenLoaded = true;
                lastPosition = (moveAmount.x * (loadedScenes - 1)) + 25.5f;
                originalPosition.x = lastPosition;
            }
            else if (!isShopLoaded && !hasShopBeenLoaded)
            {
                originalPosition.x += moveAmount.x * loadedScenes;
            }
            else if (!isShopLoaded && hasShopBeenLoaded)
            {
                lastPosition += moveAmount.x;
                originalPosition.x = lastPosition;
            }
            VectorOfYPostionFirstScene = new Vector3(0, (int)(SceneManager.GetActiveScene().GetRootGameObjects()[0].transform.position.y), 0);
            originalPosition.y = VectorOfYPostionFirstScene.y;
            sceneObject.transform.position = originalPosition;
            Debug.Log("Nowa pozycja: " + originalPosition.x + "y: " + originalPosition.y);

            if (loadScene.buildIndex == shopSceneIndex)
            {
                isShopLoaded = true;
            }
            else
            {
                isShopLoaded = false;
            }

        }
    }

    private IEnumerator LoadSceneWithDelayCoroutine(float delay)
    {
        yield return new WaitForSeconds(delay);
        LoadScene();
    }
    private IEnumerator SceneFadeOut()
    {
        SceneFadeManager._instance.StartFadeOut();
        while (SceneFadeManager._instance.isFadingOut)
        {
            yield return null;
        }
    }

    private IEnumerator SceneFadeIn()
    {
        SceneFadeManager._instance.StartFadeIn();
        while (SceneFadeManager._instance.isFadingIn)
        {
            yield return null;
        }
    }

    public void AfterBossDeath()
    {
        StartCoroutine(HandleAfterBossDeath());
    }

    private IEnumerator HandleAfterBossDeath()
    {
        yield return SceneFadeOut(); // Fade-out przed za³adowaniem nowej sceny
        currentFloor++;
        LoadScene();
        hasShopBeenLoaded = false;
        yield return SceneFadeIn(); // Fade-in po za³adowaniu nowej sceny

    }
    public int GetLastIndexOfSceneSpawned()
    {
        return lastIndexOfSceneSpawned;
    }
}
