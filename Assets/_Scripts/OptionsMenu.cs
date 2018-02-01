using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour {

    public AudioMixer audioMixer;

    public Dropdown resolutionDropdown;

    Resolution[] resolutions;

	public	GameObject sound;
	public	AudioManager AM;

    void Start()
    {
		//connecting to audiomnaer through tag
		sound=GameObject.FindGameObjectWithTag("AudioManager");
		AM=sound.GetComponent<AudioManager>();

        //gets a list of possible resolutions for the user
        resolutions = Screen.resolutions;

        //we clear our temp values from the list
        resolutionDropdown.ClearOptions();

        //New list for drop down menu
        List<string> options = new List<string>();

        int currentResIndex = 0;

        //Loop through resolutions and add each to the options list
        for(int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResIndex = i;
            }

        }

        //add options list to out resolution dropdown
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void SetResolution(int resolutionIndex)
    {
		Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
		AM.Play("click");
    }

	public void SetQuality2 (int qualityIndex)
	{
		QualitySettings.SetQualityLevel(qualityIndex);
		AM.Play("click");
	}

    // Sets volume from volume slider
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("MasterVolume", volume);
		AM.Play("click");
    }

    // sets quality from quality dropdown
    public void SetQuality (int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
		AM.Play("click");
    }

    //Sets fullscreen from toggle (ONLY WORKS WHEN BUILT)
    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
		AM.Play("click");
    }
}
