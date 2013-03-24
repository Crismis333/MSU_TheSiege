using UnityEngine;
using System.Collections;

public class MainMenuScript : MonoBehaviour {
	
	
	public GUISkin gSkin;
	
	void Menu_Main() {
		GUI.BeginGroup(new Rect(Screen.width / 2 - 150, 50, 300, 250));
		
		GUI.Box(new Rect(0,0, 300, 250), "");
		
		if (GUI.Button (new Rect(55,100,180,40), "Start Game")) {
			Application.LoadLevel(1);
		}
		
		if (GUI.Button (new Rect(55,150,180,40), "Options")) {
			MainMenuScript script = GetComponent<MainMenuScript>();
			script.enabled = false;
			OptionsMenuScript script2 = GetComponent<OptionsMenuScript>();
			script2.enabled = true;
		}
		
		if (GUI.Button (new Rect(55,200,180,40), "Quit")) {
			Application.Quit();
		}
		
		GUI.EndGroup();
	}
	
	void OnGUI() {
		GUI.skin = gSkin;
		Menu_Main();
	}
}
