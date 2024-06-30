using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainScene : MainMenuSceneController
{
    public override void StartGameButton()
    {
        StartCoroutine(LoadSceneLoaderScene());
    }
    private IEnumerator LoadSceneLoaderScene()
    {
        yield return new WaitForSeconds(5);
       SceneManager.LoadScene(0,LoadSceneMode.Single);

    }

    public override void EndGameButton()
    {
        base.EndGameButton();
    }
}
