using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScene : MainMenuSceneController
{
    [SerializeField] GameObject UI;
    static int mainSceneIndex = 9;
    private void Start()
    {
        UI = GameObject.FindGameObjectWithTag("UI");
        UI.SetActive(false);
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
