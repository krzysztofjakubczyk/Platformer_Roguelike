using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [SerializeField] private MapTranistion mapInstance;
    [SerializeField] private int shopSceneIndex;
    [SerializeField] private Vector3 moveAmount = new Vector3(38, 0, 0);
    [SerializeField] private List<int> floorSizes;

    private const int minFloorSize = 2;
    private const int maxFloorSize = 5;
    private int currentFloor = 0;
    private int loadedScenes = 1;
    private int indexOfSceneToSpawn;
    private Dictionary<int, List<int>> floorSceneIndexes = new Dictionary<int, List<int>>();

    void Start()
    {
        if (moveAmount == Vector3.zero)
        {
            moveAmount = new Vector3(38, 0, 0);
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
    }

    public void LoadScene()
    {
        if (loadedScenes < shopSceneIndex)
        {
            indexOfSceneToSpawn = floorSceneIndexes[currentFloor].First();
            floorSceneIndexes[currentFloor].Remove(indexOfSceneToSpawn);
            loadedScenes++;
        }
        else if (loadedScenes == shopSceneIndex)
        {
            indexOfSceneToSpawn = shopSceneIndex;
            loadedScenes = 0;
        }

        if (IsSceneAlreadyLoaded(indexOfSceneToSpawn))
        {
            return;
        }

        StartCoroutine(LoadSceneCoroutine());
    }

    private IEnumerator LoadSceneCoroutine()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(indexOfSceneToSpawn, LoadSceneMode.Additive);
        yield return asyncLoad;
        Scene scene = SceneManager.GetSceneByBuildIndex(indexOfSceneToSpawn);
        MoveScene(scene);
        SceneManager.SetActiveScene(scene);
    }

    public void UnLoadScene()
    {
        foreach (var scene in SceneManager.GetAllScenes())
        {
            if (scene != SceneManager.GetActiveScene())
            {
                SceneManager.UnloadSceneAsync(scene.buildIndex);
            }
        }
    }

    private bool IsSceneAlreadyLoaded(int sceneIndex)
    {
        Scene activeScene = SceneManager.GetActiveScene();
        return activeScene.buildIndex == sceneIndex;
    }

    private void MoveScene(Scene loadScene)
    {
        Debug.Log("Moved scene by: " + moveAmount);

        GameObject[] sceneObjects = loadScene.GetRootGameObjects();
        if (sceneObjects.Length == 0)
        {
            Debug.LogWarning("No root game objects found in the scene to move.");
            return;
        }

        foreach (GameObject obj in sceneObjects)
        {
            if (obj != null)
            {
                Debug.Log("Moving object: " + obj.name);
                obj.transform.position += moveAmount;
            }
            else
            {
                Debug.LogWarning("Encountered a null object in the scene.");
            }
        }
    }
}
    