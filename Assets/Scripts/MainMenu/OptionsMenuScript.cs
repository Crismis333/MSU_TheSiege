using UnityEngine;
using System.Collections;
using System;

public class OptionsMenuScript : MonoBehaviour {
	
	public GUISkin gSkin;
    public bool pauseMenu;
    private float musicvol, effectvol, resolution, quality;
    private bool fullscreen;
    private Resolution mres;

	void Menu_Options() {
        Resolution[] res = Screen.resolutions;
        float f = 0.0f;
        if (res.Length > 1)
            for (int i = 0; i < res.Length; i++)
                if (res[i].width < 800 || res[i].height < 600)
                    f += 1.0f;
        GUI.BeginGroup(new Rect(Screen.width/2-395, Screen.height / 2 - 3*70, 790, 6*70));
        GUI.Box(new Rect(0, 0, 790, 6 * 70), "");
        GUI.Label(new Rect(0, 0*70, 790, 64), "Music volume");
        musicvol = GUI.HorizontalSlider(new Rect(250, 0 * 70, 512, 64), musicvol, 0.0f, 10.0f);
        GUI.Label(new Rect(0, 1 * 70, 790, 64), "Effect volume");
        effectvol = GUI.HorizontalSlider(new Rect(250, 1 * 70, 512, 64), effectvol, 0.0f, 10.0f);
        GUI.Label(new Rect(0, 2 * 70, 790, 64), "FullScreen");
        fullscreen = GUI.Toggle(new Rect(350, 2 * 70, 64, 64),fullscreen, " ");
        GUI.Label(new Rect(0, 3 * 70, 790, 64), "Resolution");
        resolution = GUI.HorizontalSlider(new Rect(250, 3 * 70, 512, 64), resolution, f, (float)res.Length - 1);
        resolution = ((int)resolution + 0.5f);
        mres = res[(int)(resolution)];
        GUI.Label(new Rect(0, 4 * 70, 790, 64), "Quality");
        quality = GUI.HorizontalSlider(new Rect(250, 4 * 70, 512, 64), quality, 0.0f, 5.0f);
        quality = ((int)quality + 0.5f);
        GUI.Label(new Rect(450, 3 * 70, 340, 64), mres.width + "x" + mres.height);
        GUI.Label(new Rect(450, 4 * 70, 340, 64), QualitySettings.names[(int)quality]);
        if (GUI.Button(new Rect(0, 5 * 70, 790, 64), "Accept")) { Menu_Options_Back(); }

		GUI.EndGroup();
	}
	
	void OnGUI() {
		GUI.skin = gSkin;
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Menu_Options_Back();
        }
        else
		    Menu_Options();
	}

    public void Menu_Options_Startup() {
        musicvol = OptionsValues.musicVolume;
        effectvol = OptionsValues.sfxVolume;
        mres = Screen.currentResolution;
        fullscreen = Screen.fullScreen;
        quality = (float)QualitySettings.GetQualityLevel();
        for (int i = 0; i < Screen.GetResolution.Length; i++)
            if (Screen.GetResolution[i].height >= Screen.height && Screen.GetResolution[i].width >= Screen.width) {
                resolution = i;
                break;
            }
        enabled = true;
    }

    void Menu_Options_Back() {
        if (mres.width != Screen.width || mres.height != Screen.height || fullscreen != Screen.fullScreen)
            Screen.SetResolution(mres.width, mres.height, fullscreen);
        OptionsValues.musicVolume = musicvol;
        OptionsValues.sfxVolume = effectvol;
        QualitySettings.SetQualityLevel((int)quality);
        this.enabled = false;
        if (!pauseMenu)
            GetComponent<MainMenuScript>().enabled = true;
        else
            GetComponent<PauseMenuScript>().enabled = true;
    }
}
