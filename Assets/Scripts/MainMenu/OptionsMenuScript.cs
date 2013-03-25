using UnityEngine;
using System.Collections;

public class OptionsMenuScript : MonoBehaviour {
	
	public GUISkin gSkin;

    private float scrollpos;

	void Menu_Options() {
        GUI.BeginGroup(new Rect(0, Screen.height / 2 - 100, Screen.width, Screen.height));
        GUI.Label(new Rect(0, 0*80, Screen.width, 64), "Music volume");
        scrollpos = GUI.HorizontalSlider(new Rect(500, 0 * 80, 512, 64), scrollpos, 0.0f, 10.0f);
        if (GUI.Button(new Rect(0, 1*80, Screen.width, 64), "Go Back!")) { Menu_Options_Back(); }
		
		
		GUI.EndGroup();
	}
	
	void OnGUI() {
		GUI.skin = gSkin;
		Menu_Options();
	}

    void Menu_Options_Back() {
        OptionsMenuScript script = GetComponent<OptionsMenuScript>();
        script.enabled = false;
        MainMenuScript script2 = GetComponent<MainMenuScript>();
        script2.enabled = true;
    }
}
