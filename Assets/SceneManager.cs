using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [SerializeField] MapTranistion mapInstance;
    [SerializeField] int indexOfSceneToSpawn;
    [SerializeField] int indexOfSceneToUnload;
    [SerializeField] int indexOfShopAtFirstFloor;
    [SerializeField] int indexOfShopAtSecondFloor;
    [SerializeField] int sizeOfFirstFloor;
    [SerializeField] int sizeOfSecondFloor;
    int loadedScenes = 1;
    [SerializeField] List<int> IndexesOfScenesAtFirstFloor;
    [SerializeField] List<int> IndexesAddedToFirstFloor;
    [SerializeField] List<int> IndexesOfScenesAtSecondFloor;
    [SerializeField] List<int> IndexesAddedToSecondFloor;
    public Vector3 moveAmount;

    void Start()
    {
        RandomNumbersForFloors();
    }

    private void RandomNumbersForFloors()
    {
        sizeOfFirstFloor = UnityEngine.Random.Range(2, 5);
        sizeOfSecondFloor = UnityEngine.Random.Range(2, 5);

        for (int i = 0; i < sizeOfFirstFloor; i++)
        {
            int randomIndex;
            do
            {
                randomIndex = UnityEngine.Random.Range(1, 5);
            }
            while (IndexesAddedToFirstFloor.Contains(randomIndex));

            IndexesOfScenesAtFirstFloor.Add(randomIndex);
            IndexesAddedToFirstFloor.Add(randomIndex);
        }
        for (int i = 0; i < sizeOfSecondFloor; i++)
        {
            int randomIndex;
            do
            {
                randomIndex = UnityEngine.Random.Range(1, 5);
            }
            while (IndexesAddedToFirstFloor.Contains(randomIndex));

            IndexesOfScenesAtSecondFloor.Add(randomIndex);
            IndexesAddedToSecondFloor.Add(randomIndex);
        }
    }
    public void LoadScene()
    {
        indexOfShopAtFirstFloor = IndexesOfScenesAtFirstFloor[Random.Range(0, IndexesOfScenesAtFirstFloor.Count)];
        if (IsFirstFloor())
        {
            if (loadedScenes < indexOfShopAtSecondFloor)
            {
                indexOfSceneToSpawn = IndexesOfScenesAtFirstFloor.First();
                IndexesOfScenesAtFirstFloor.Remove(indexOfSceneToSpawn);
                loadedScenes++;
            }
            else if (IsFirstFloor() && loadedScenes == indexOfShopAtFirstFloor)
            {
                indexOfSceneToSpawn = indexOfShopAtFirstFloor;
                loadedScenes = 0;
            }
        }
        else
        {
            indexOfShopAtSecondFloor = IndexesOfScenesAtSecondFloor[Random.Range(0, IndexesOfScenesAtSecondFloor.Count)];
            if (loadedScenes < indexOfShopAtSecondFloor)
            {
                indexOfSceneToSpawn = IndexesOfScenesAtSecondFloor.First();
                IndexesOfScenesAtSecondFloor.Remove(indexOfSceneToSpawn);
            }

            else if (loadedScenes == indexOfShopAtSecondFloor)
            {
                indexOfSceneToSpawn = indexOfShopAtSecondFloor;
                loadedScenes = 0;
            }
        }
        SceneManager.LoadSceneAsync(indexOfSceneToSpawn, LoadSceneMode.Additive);
        Scene scene = SceneManager.GetSceneByBuildIndex(indexOfSceneToSpawn);
        MoveScene(scene);
        SceneManager.SetActiveScene(scene);
    }
    public void UnLoadScene()
    {
        foreach (var scene in SceneManager.GetAllScenes()) // Unload inactive scenes
        {
            if (scene != SceneManager.GetActiveScene())
            {
                indexOfSceneToUnload = scene.buildIndex;
                SceneManager.UnloadSceneAsync(indexOfSceneToUnload);
            }
        }
    }
    private int GetCurrentLoadedScene()
    {
        return SceneManager.GetActiveScene().buildIndex;
    }
    private bool IsFirstFloor()
    {
        if (IndexesOfScenesAtFirstFloor.Count > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    private void MoveScene(Scene loadScene)
    {
        moveAmount = new Vector3(38, 0, 0);
        Debug.Log("Moved scene by: " + moveAmount);
        GameObject[] sceneObjects = loadScene.GetRootGameObjects();
        foreach (GameObject obj in sceneObjects)
        {
            obj.transform.position += moveAmount;
        }
    }
}
