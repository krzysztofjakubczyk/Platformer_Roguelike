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
    private int loadedScenes;
    private int indexOfSceneToSpawn;
    private int indexForBossScene;
    private Vector3 VectorOfYPostionFirstScene;
    private Vector3 originalMoveAmount;
    private Dictionary<int, List<int>> floorSceneIndexes = new Dictionary<int, List<int>>();
    private List<int> shopInsertedFloors = new List<int>();
    private bool shopJustLoaded = false;  // Flaga oznaczaj¹ca, ¿e sklep zosta³ w³aœnie za³adowany

    void Start()
    {
        originalMoveAmount = new Vector3 (38,0,0);
        VectorOfYPostionFirstScene = new Vector3(0, (int)(SceneManager.GetActiveScene().GetRootGameObjects()[0].transform.position.y), 0);
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
        int floorSize = Random.Range(minFloorSize, maxFloorSize);
        floorSizes[floor] = floorSize;
        var sceneIndexes = new HashSet<int>();

        for (int i = 0; i < floorSize; i++)
        {
            int randomIndex;
            do
            {
                randomIndex = Random.Range(1, 5);
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
            int shopPosition = Random.Range(1, floorSize - 1); // Zapewnia, ¿e sklep nie bêdzie ani pierwszy, ani ostatni
            floorSceneIndexes[floor].Insert(shopPosition, shopSceneIndex);
            shopInsertedFloors.Add(floor);
        }

        floorSceneIndexes[floor].Add(indexForBossScene);
    }

    public void LoadScene()
    {
        if (floorSceneIndexes.Count > 0)
        {
            indexOfSceneToSpawn = floorSceneIndexes[currentFloor].First();
            floorSceneIndexes[currentFloor].Remove(indexOfSceneToSpawn);
            loadedScenes++;
        }
        if (IsSceneAlreadyLoaded(indexOfSceneToSpawn))
        {
            return;
        }

        StartCoroutine(LoadSceneCoroutine());
    }

    private IEnumerator LoadSceneCoroutine()
    {
        // Jeœli w³aœnie za³adowano sklep, ustaw moveAmount na 24 dla nastêpnej sceny
        if (shopJustLoaded)
        {
            moveAmount = new Vector3(23, moveAmount.y, moveAmount.z);
        }
        else
        {
            moveAmount = originalMoveAmount;
        }

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(indexOfSceneToSpawn, LoadSceneMode.Additive);
        yield return asyncLoad;
        Scene scene = SceneManager.GetSceneByBuildIndex(indexOfSceneToSpawn);
        SceneManager.SetActiveScene(scene);

        MoveScene(scene);

        // Po za³adowaniu sceny sprawdŸ, czy to jest sklep, i ustaw flagê
        if (indexOfSceneToSpawn == shopSceneIndex)
        {
            shopJustLoaded = true;
        }
        else
        {
            shopJustLoaded = false;
        }
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
        Vector3 originalPosition = sceneObject.transform.position;
        originalPosition.x += moveAmount.x * loadedScenes;
        originalPosition.y = VectorOfYPostionFirstScene.y;
        sceneObject.transform.position = originalPosition;
        Debug.Log(originalPosition.x + "Move amount: "+ moveAmount.x);
        // Jeœli za³adowano sklep, ustaw flagê shopJustLoaded na true
        if (indexOfSceneToSpawn == shopSceneIndex)
        {
            shopJustLoaded = true;
        }
    }

}
 