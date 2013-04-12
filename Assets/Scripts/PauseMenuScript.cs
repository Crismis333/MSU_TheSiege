using UnityEngine;
using System.Collections;

public class PauseMenuScript : MonoBehaviour {

    public GUISkin gSkin;
    public bool onMap;

    void Menu_Options()
    {
        

        if (onMap)
        {
            GUI.BeginGroup(new Rect(Screen.width / 2 - 395, Screen.height / 2 - 3 * 70, 790, 6 * 70));
            GUI.Box(new Rect(0, 0, 790, 6 * 70), "");
            if (GUI.Button(new Rect(0, 0 * 70, 790, 64), "Options")) { Pause_Options(); }
            if (GUI.Button(new Rect(0, 1 * 70, 790, 64), "Controls")) { }
            if (GUI.Button(new Rect(0, 2 * 70, 790, 64), "Main Menu")) { Pause_MainMenu(); }
            if (GUI.Button(new Rect(0, 3 * 70, 790, 64), "Quit")) { Pause_Quit(); }
            if (GUI.Button(new Rect(0, 5 * 70, 790, 64), "Return")) { Pause_Back(); }
        }
        else
        {
            GUI.BeginGroup(new Rect(Screen.width / 2 - 395, Screen.height / 2 - 3.5f * 70, 790, 7 * 70));
            GUI.Box(new Rect(0, 0, 790, 7 * 70), "");
            if (GUI.Button(new Rect(0, 0 * 70, 790, 64), "Options")) { Pause_Options(); }
            if (GUI.Button(new Rect(0, 1 * 70, 790, 64), "Controls")) { }
            if (GUI.Button(new Rect(0, 2 * 70, 790, 64), "Map")) { Pause_Map(); }
            if (GUI.Button(new Rect(0, 3 * 70, 790, 64), "Restart")) { Pause_Restart(); }
            if (GUI.Button(new Rect(0, 4 * 70, 790, 64), "Quit")) { Pause_Quit(); }
            if (GUI.Button(new Rect(0, 6 * 70, 790, 64), "Return")) { Pause_Back(); }
        }
        GUI.EndGroup();
    }

    void OnGUI()
    {
        GUI.skin = gSkin;
        if (Input.GetKeyDown(KeyCode.Escape))
            Pause_Back();
        else
            Menu_Options();
    }

    void Pause_Restart()
    {
        this.enabled = false;
        GetComponent<PauseReturnToScript>().restart = true;
        GetComponent<PauseReturnToScript>().enabled = true;
    }

    void Pause_Map()
    {
        this.enabled = false;
        GetComponent<PauseReturnToScript>().enabled = true;
    }

    void Pause_MainMenu()
    {
        this.enabled = false;
        GetComponent<PauseReturnToScript>().onMap = true;
        GetComponent<PauseReturnToScript>().enabled = true;
    }

    void Pause_Quit()
    {
        this.enabled = false;
        GetComponent<PauseReturnToScript>().quit = true;
        GetComponent<PauseReturnToScript>().enabled = true;
    }

    void Pause_Back()
    {
        this.enabled = false;
        if (onMap)
        {
            GetComponent<MapGui>().enabled = true;
            transform.parent.gameObject.GetComponent<MapMovementController>().enabled = true;
        }
        else
        {
            Time.timeScale = 1; // TODO: Set SLOWDOWN!
            GetComponent<GUIScript>().enabled = true;
        }
    }

    void Pause_Options()
    {
        this.enabled = false;
        if (onMap)
        {
            GetComponent<OptionsMenuScript>().enabled = true;
        }
    }
}
