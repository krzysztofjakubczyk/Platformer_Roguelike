using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadTrigger : MonoBehaviour
{
    [Header("Name of scenes to make visible or not")]
    [SerializeField] string[] _scenesToLoad;
    [SerializeField] string[] _scenesToUnLoad;

    private GameObject _player;

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            LoadScene();
            UnLoadScene();
        }
    }

    private void UnLoadScene()
    {
        for (int i = 0; i < _scenesToUnLoad.Length; i++)
        {
            for (int j = 0; j < SceneManager.sceneCount; j++)
            {
                Scene loadScene = SceneManager.GetSceneAt(j);
                if (loadScene.name == _scenesToUnLoad[i])
                {
                    SceneManager.UnloadSceneAsync(_scenesToLoad[i]);
                }
            }
        }
    }

    private void LoadScene()
    {
        for (int i = 0; i < _scenesToLoad.Length; i++)
        {
            bool isSceneLoaded = false;
            for (int j = 0; j < SceneManager.sceneCount; j++)
            {
                Scene loadScene = SceneManager.GetSceneAt(j);
                if (loadScene.name == _scenesToLoad[i])
                {
                    isSceneLoaded = true;
                    break;
                }
            }
            if (!isSceneLoaded)
            {
                SceneManager.LoadSceneAsync(_scenesToLoad[i], LoadSceneMode.Additive);
            }   
        }
    }
}
