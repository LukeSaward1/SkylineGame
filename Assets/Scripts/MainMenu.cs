using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject OptionsMenuObject;
    public GameObject MainMenuObject;

    public bool speedrunStarted;

    public void PlayGame ()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        speedrunStarted = true;
    }

    public void LoadOptions ()
    {
        OptionsMenuObject.SetActive(true);
        MainMenuObject.SetActive(false);
    }

    public void QuitGame ()
    {
        Application.Quit();
        Debug.Log("Quit.");
    }
}
