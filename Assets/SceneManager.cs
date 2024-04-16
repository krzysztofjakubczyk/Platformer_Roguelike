using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    Scene currentLoadedScene;
    void Start()
    {
        currentLoadedScene = getCurrentLoadedScene();
    }

    public void onClickedStartButton()
    {
        bool isThisMainMenuScene = currentLoadedScene.buildIndex == 0;
        if (isThisMainMenuScene)
        {
            SceneManager.LoadScene(1, LoadSceneMode.Single);
        }
        else { Debug.Log("There is no first scene in game"); }
    }

    void onPlayersDeath()
    {
        SceneManager.LoadScene(/* ktor¹œ do pauzy*/ 1, LoadSceneMode.Single);
    }
    Scene getCurrentLoadedScene()
    {
        return SceneManager.GetActiveScene();
    }
}
