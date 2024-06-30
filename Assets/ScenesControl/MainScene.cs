using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainScene : MainMenuSceneController
{
    [SerializeField] private GameObject CanvasToLoader;
    [SerializeField] private Slider _slider;
    public override void StartGameButton()
    {
        StartCoroutine(LoadSceneLoaderScene());
    }
    private IEnumerator LoadSceneLoaderScene()
    {
        yield return new WaitForSeconds(1f);
        CanvasToLoader.SetActive(true);
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(0, LoadSceneMode.Single);
        while (!asyncLoad.isDone)
        {
            float progressValue = Mathf.Clamp01(asyncLoad.progress / 0.9f);
            _slider.value = progressValue;

            yield return null;
        }

        CanvasToLoader.SetActive(false);
    }


    public override void EndGameButton()
    {
        base.EndGameButton();
    }
}
