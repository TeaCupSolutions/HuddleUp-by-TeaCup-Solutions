using UnityEngine;
using UnityEngine.SceneManagement;
using StaticValuesNamespace;
using System;

public class MainMenu : MonoBehaviour {
    public void PlayGame() {
        StaticValues.IsReplay = false;
        DateTime dt = System.DateTime.Now;
        StaticValues.ReplayName = dt.Year + "" + dt.Month + "" + dt.Hour + "" + dt.Hour + "" + dt.Minute + "" + dt.Second;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ShowReplay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ExitGame()
    {
        //dosn't work in editor
        Application.Quit();
    }
}
