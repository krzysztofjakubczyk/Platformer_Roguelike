using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    bool show;

    void Start()
    {
        show = false;
        ShowChildren(show);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            show = !show;
            ShowChildren(show);

            if (show)
                Time.timeScale = 0;
            else 
                Time.timeScale = 1;
        }
    }

    void ShowChildren(bool show)
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(show);
        }
    }

    public void PlayButton()
    {
        ShowChildren(false);
    }

    public void Settings()
    {
        // show settings screen
    }

    public void MainMenu()
    {
        //go back to main menu or quit game
    }


}
