using UnityEngine;
using System.Collections;

public class OptionsMenuScript : MonoBehaviour {
	
	public GUISkin gSkin;
	
	void Menu_Options() {
		GUI.BeginGroup(new Rect(Screen.width / 2 - 150, 50, 300, 200));
		
		GUI.Box(new Rect(0,0, 300, 200), "");
		
		if (GUI.Button (new Rect(55,100,180,40), "Button")) {
		}
		
		if (GUI.Button (new Rect(55,150,180,40), "Go Back")) {
			OptionsMenuScript script = GetComponent<OptionsMenuScript>();
			script.enabled = false;
			MainMenuScript script2 = GetComponent<MainMenuScript>();
			script2.enabled = true;
		}
		
		if (GUI.Button (new Rect(55,200,180,40), "Quit")) {
			Application.Quit();
		}
		
		GUI.EndGroup();
	}
	
	void OnGUI() {
		GUI.skin = gSkin;
		Menu_Options();
	}
}
