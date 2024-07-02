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
    List<int> loadedSceneIndexes = new List<int>();
    [SerializeField] private MapTranistion mapInstance;
    [SerializeField] private Dictionary<int, List<int>> floorSceneIndexes = new Dictionary<int, List<int>>();
    [SerializeField] private GameObject player;
    [SerializeField] private int indexOfDeathScene;
    [SerializeField] private int indexOfSecondFloorsScene;

    //public Action changeEnemyPos;

    private const int minFloorSize = 2;
    private const int maxFloorSize =3;
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
        loadedSceneIndexes.Add(1); //pierwsza za³adowana scena to 0
        StartCoroutine(InitializeFloorsCoroutine()); //zacznij losowanie indexow do konkretnych poziomow
    }

    private IEnumerator InitializeFloorsCoroutine()
    {
        for (int floor = 0; floor < floorSizes.Count; floor++)
        {
            Debug.Log("Zaczêto losowaæ piêtro: " + floor);
            yield return StartCoroutine(InitializeFloorScenesCoroutine(floor));
        }
    }

    private IEnumerator InitializeFloorScenesCoroutine(int floor)
    {
        int floorSize = UnityEngine.Random.Range(minFloorSize, maxFloorSize);
        floorSizes[floor] = floorSize;
        List<int> availableIndexes = new List<int> { 2,3,4}; // Dostêpne indeksy do wylosowania
        List<int> generatedIndexes = new List<int>();
        ShuffleList(availableIndexes);
        for (int i = 0; i < floorSize; i++)
        {
            generatedIndexes.Add(availableIndexes[i]);
        }
        // Dodanie sklepu na losowej pozycji, ale nie pierwszej ani ostatniej
        if (!shopInsertedFloors.Contains(floor))
        {
            int shopPosition = UnityEngine.Random.Range(1, floorSize - 1);
            generatedIndexes.Insert(shopPosition, shopSceneIndex);
            Debug.Log("Shop position: " + shopPosition);
            shopInsertedFloors.Add(floor);
        }

        generatedIndexes.Add(indexForBossScene[floor]);

        floorSceneIndexes[floor] = generatedIndexes;

        yield return null;
    }

    private void ShuffleList<T>(List<T> list)
    {
        int n = list.Count;
        System.Random rng = new System.Random();
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
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
        StartCoroutine(LoadSceneCoroutine(LoadSceneMode.Additive));
    }

    public void onPlayersDeath()
    {
        SceneManager.LoadScene(indexOfDeathScene);
    }
    private IEnumerator LoadSceneCoroutine(LoadSceneMode mode)
    {
        floorSceneIndexes[currentFloor].Remove(indexOfSceneToSpawn);
        loadedScenes++;
        loadedSceneIndexes.Add(indexOfSceneToSpawn);
        yield return SceneManager.LoadSceneAsync(indexOfSceneToSpawn, mode);
        Scene scene = SceneManager.GetSceneByBuildIndex(indexOfSceneToSpawn);
        MoveScene(scene);
        SceneManager.SetActiveScene(scene);
        //changeEnemyPos?.Invoke();
    }

    public void UnLoadScene()
    {
        int sceneIndexToUnload = loadedSceneIndexes.First(); // Pobierz pierwszy indeks sceny do wy³adowania
        Debug.Log("NUMER SCENY DO ODLADOWANIA: " + sceneIndexToUnload);
        if (SceneManager.GetSceneByBuildIndex(sceneIndexToUnload).IsValid()) // SprawdŸ poprawnoœæ sceny przed wywo³aniem UnloadSceneAsync
        {
           SceneManager.UnloadSceneAsync(sceneIndexToUnload);
            loadedSceneIndexes.Remove(sceneIndexToUnload);
            Debug.Log("LOADEDSCENEINDEXES WIELKOSC" + loadedSceneIndexes.Count());
        }
        else
        {
            Debug.LogWarning("Trying to unload an invalid scene: " + sceneIndexToUnload);
        }
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

            // Przesuniêcie obiektu w zale¿noœci od wczeœniejszych warunków
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

            // Ustawienie nowej pozycji obiektu
            sceneObject.transform.position = originalPosition;

            // Debugowanie pozycji
            Debug.Log("Nowa pozycja: " + originalPosition.x + ", " + originalPosition.y);

            // Ustawienie aktywnej sceny
            SceneManager.SetActiveScene(loadScene);

            // Ustawienie stanu ³adowania sklepu
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

    public IEnumerator AfterBossDeathAsync()
    {
        loadedSceneIndexes.Clear();
        currentFloor++;
        hasShopBeenLoaded = false;

        yield return StartCoroutine(LoadFirstSceneOfNextFloor());
    }
    private IEnumerator LoadFirstSceneOfNextFloor()
    {
        if (floorSceneIndexes.ContainsKey(currentFloor) && floorSceneIndexes[currentFloor].Count > 0)
        {
            floorSceneIndexes[currentFloor][0] = indexOfSecondFloorsScene;
            indexOfSceneToSpawn = indexOfSecondFloorsScene;
            yield return StartCoroutine(LoadSceneCoroutine(LoadSceneMode.Single));
        }

        yield return null;
    }
    public int GetLastIndexOfSceneSpawned()
    {
        return lastIndexOfSceneSpawned;
    }
}
