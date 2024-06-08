using Cinemachine.Utility;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [SerializeField] private MapTranistion mapInstance;
    [SerializeField] private int shopSceneIndex;
    [SerializeField] private UnityEngine.Vector3 moveAmount = new UnityEngine.Vector3(38, 0, 0);
    [SerializeField] private List<int> floorSizes;

    private const int minFloorSize = 2;
    private const int maxFloorSize = 5;
    private int currentFloor = 0;
    private int loadedScenes;
    private int indexOfSceneToSpawn;
    private int indexForBossScene;
    private UnityEngine.Vector3 VectorOfYPostionFirstScene;
    private Dictionary<int, List<int>> floorSceneIndexes = new Dictionary<int, List<int>>();

    void Start()
    {
        VectorOfYPostionFirstScene = new UnityEngine.Vector3 (0,((int)(SceneManager.GetActiveScene().GetRootGameObjects()[0].transform.position.y)),0);
        if (moveAmount == UnityEngine.Vector3.zero)
        {
            moveAmount = new UnityEngine.Vector3(38, 0, 0);
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
        if (floorSceneIndexes.Count > 0)
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
        else if (floorSizes.Count == 0)
        {
            print("Nie ma narazie bosa");
        }
        if (IsSceneAlreadyLoaded(indexOfSceneToSpawn))
        {
            return;
        }

        LoadNewScene(SceneManager.GetSceneByBuildIndex(indexOfSceneToSpawn).name);
    }

    public void LoadNewScene(string sceneName)
    {
        StartCoroutine(LoadSceneCoroutine());
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        MoveScene(scene);
    }

    private IEnumerator LoadSceneCoroutine()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(indexOfSceneToSpawn, LoadSceneMode.Additive);
        yield return asyncLoad;
        Scene scene = SceneManager.GetSceneByBuildIndex(indexOfSceneToSpawn);
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
        GameObject[] sceneObjects = loadScene.GetRootGameObjects();
        GameObject sceneObject = sceneObjects[0];
        UnityEngine.Vector3 orginalPostion = sceneObject.transform.position;
        orginalPostion.x = moveAmount.x * loadedScenes;
        orginalPostion.y = VectorOfYPostionFirstScene.y;
        sceneObject.transform.position = orginalPostion;
    }




}