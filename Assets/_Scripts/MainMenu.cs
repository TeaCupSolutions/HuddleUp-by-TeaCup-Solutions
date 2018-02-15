using UnityEngine;
using UnityEngine.SceneManagement;
using StaticValuesNamespace;
using System;

public class MainMenu : MonoBehaviour {
	public	GameObject sound;
	public	AudioManager AM;

	void Start()
	{
		//connecting to audiomnaer through tag
		sound=GameObject.FindGameObjectWithTag("AudioManager");
		AM=sound.GetComponent<AudioManager>();
	}
    public void PlayGame() {
		AM.Play("click");
        StaticValues.IsReplay = false;
        DateTime dt = System.DateTime.Now;
        StaticValues.ReplayName = dt.Year + "" + dt.Month + "" + dt.Hour + "" + dt.Hour + "" + dt.Minute + "" + dt.Second;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
	public void HoverSound()
	{
		AM.Play ("hover");
	}

    public void ShowReplay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }

    public void ExitGame()
    {
		AM.Play("click");
        //dosn't work in editor
        Application.Quit();
    }
}
