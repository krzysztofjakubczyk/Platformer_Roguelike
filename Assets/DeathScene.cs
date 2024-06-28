using UnityEngine.SceneManagement;
using UnityEngine.WSA;

public class DeathScene : MainMenuSceneController
{
    static int mainSceneIndex = 9; 
    public override void StartGameButton()
    {
        SceneManager.LoadSceneAsync(mainSceneIndex);
    }
    public override void EndGameButton()
    {
        base.EndGameButton();
    }
}
