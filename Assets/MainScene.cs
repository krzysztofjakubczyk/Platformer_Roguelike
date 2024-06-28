using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainScene : MainMenuSceneController
{
    [SerializeField] private int firstStaticScene;
    [SerializeField] private GameObject panelToSceneLoader;
    [SerializeField] private Slider loadingSlider;
    public override void StartGameButton()
    {
        StartCoroutine(LoadSceneLoaderScene(firstStaticScene));
    }
    private IEnumerator LoadSceneLoaderScene(int levelToLoad)
    {
        panelToSceneLoader.SetActive(true);

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(levelToLoad);
        while (!asyncLoad.isDone)
        {
            float progressValue = Mathf.Clamp01(asyncLoad.progress / 0.9f);
            loadingSlider.value = progressValue;
            yield return null;
        }
        panelToSceneLoader.SetActive(false);
    }
    public override void EndGameButton()
    {
        base.EndGameButton();
    }
}
