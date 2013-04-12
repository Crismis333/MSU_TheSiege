using UnityEngine;
using System.Collections;

public class MainMenuScript : MonoBehaviour {
	
	
	public GUISkin gSkin;

	void Menu_Main() {
        GUI.BeginGroup(new Rect(0, Screen.height / 2 - 100, Screen.width, Screen.height));

        if (GUI.Button(new Rect(0, 0*70, Screen.width-30, 64), "Start Game")) { Menu_Main_Start_Game(); }
        if (GUI.Button(new Rect(0, 1*70, Screen.width-30, 64), "Options")) { Menu_Main_Options(); }
        if (GUI.Button(new Rect(0, 2*70, Screen.width-30, 64), "Quit")) { Menu_Main_Quit(); }
		
		GUI.EndGroup();
	}

    void Menu_Main_Start_Game() {
        Application.LoadLevel(1);
    }

    void Menu_Main_Options() {
        this.enabled = false;
        GetComponent<OptionsMenuScript>().Menu_Options_Startup();
    }

    void Menu_Main_Quit() {
        this.enabled = false;
        GetComponent<QuitAcceptMenu>().enabled = true;
    }

	void OnGUI() {
		GUI.skin = gSkin;
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Menu_Main_Quit();
        }
        else
            Menu_Main();
	}
}
