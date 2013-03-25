using UnityEngine;
using System.Collections;

public class MainMenuScript : MonoBehaviour {
	
	
	public GUISkin gSkin;

	void Menu_Main() {
        GUI.BeginGroup(new Rect(0, Screen.height / 2 - 100, Screen.width, Screen.height));

        if (GUI.Button(new Rect(0, 0*80, Screen.width-30, 64), "Start Game")) { Menu_Main_Start_Game(); }
        if (GUI.Button(new Rect(0, 1*80, Screen.width-30, 64), "Options")) { Menu_Main_Options(); }
        if (GUI.Button(new Rect(0, 2*80, Screen.width-30, 64), "Quit")) { Menu_Main_Quit(); }
		
		GUI.EndGroup();
	}

    void Menu_Main_Start_Game() {
        Application.LoadLevel(1);
    }

    void Menu_Main_Options() {
        MainMenuScript script = GetComponent<MainMenuScript>();
        script.enabled = false;
        OptionsMenuScript script2 = GetComponent<OptionsMenuScript>();
        script2.enabled = true;
    }

    void Menu_Main_Quit() {
        MainMenuScript script = GetComponent<MainMenuScript>();
        script.enabled = false;
        QuitAcceptMenu script2 = GetComponent<QuitAcceptMenu>();
        script2.enabled = true;
    }

	void OnGUI() {
		GUI.skin = gSkin;
		Menu_Main();
	}
}
