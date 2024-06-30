using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuSceneController : MonoBehaviour
{

    // Metoda wirtualna do rozpoczêcia gry, któr¹ mo¿na nadpisaæ w klasach pochodnych
    public virtual void StartGameButton()
    {
        // Domyœlna implementacja (jeœli jakaœ jest)
        Debug.Log("StartGameButton pressed - base implementation");
    }

    public virtual void EndGameButton()
    {
        Application.Quit();
    }

    public virtual void SettingsGameButton()
    {
        // £adowanie ustawieñ - do nadpisania w klasach pochodnych
        Debug.Log("SettingsGameButton pressed - base implementation");
    }

    public virtual void ContinueGameButton()
    {
        // Kontynuowanie gry - do nadpisania w klasach pochodnych
        Debug.Log("ContinueGameButton pressed - base implementation");
    }
}