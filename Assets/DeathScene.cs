using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScene : MainMenuSceneController
{
    GameObject uiCanvas;
    static int mainSceneIndex = 9;
    private void Start()
    {
        uiCanvas = GameObject.FindGameObjectWithTag("UI");
        uiCanvas.gameObject.SetActive(false);
    }
    public override void StartGameButton()
    {
        SceneManager.LoadSceneAsync(mainSceneIndex);
    }
    public override void EndGameButton()
    {
        base.EndGameButton();
    }
}
