using UnityEngine;
using UnityEngine.SceneManagement;
using StaticValuesNamespace;
using System;
using System.Net;
using System.Net.Sockets;
using TMPro; 

public class MainMenu : MonoBehaviour {
	public	GameObject sound;
	public	AudioManager AM;
    public TMP_Text IPText;

    void Start()
	{
		//connecting to audiomnaer through tag
		sound=GameObject.FindGameObjectWithTag("AudioManager");
		AM=sound.GetComponent<AudioManager>();
        IPText.text = "Ip Address: " + this.LocalIPAddress();

    }
    public void PlayGame() {
		AM.Play("click");
        Time.timeScale = 1;
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
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }

    public void ExitGame()
    {
		AM.Play("click");
        //dosn't work in editor
        Application.Quit();
    }

    public string LocalIPAddress()
    {
       //gets local ip address
         IPHostEntry host;
        string localIP = "";
        host = Dns.GetHostEntry(Dns.GetHostName());
        foreach (IPAddress ip in host.AddressList)
        {
            if (ip.AddressFamily == AddressFamily.InterNetwork)
            {
                localIP = ip.ToString();
                break;
            }
        }
        return localIP;
    }

}
