using UnityEngine;
using System.Collections;

public class PauseReturnToScript : MonoBehaviour {

    public GUISkin gSkin;
    [HideInInspector]
    public bool onMap, quit, restart;

    void Return_Accept()
    {
        GUI.BeginGroup(new Rect(Screen.width / 2 - 395, Screen.height / 2 - 2.5f * 70, 790, 5 * 70));

        GUI.Box(new Rect(0, 0, 790, 5 * 70), "");
        if (quit)
            GUI.Label(new Rect(0, 1 * 70, 790, 64), "Do you really wish to quit? All progress will be lost.");
        else if (restart)
            GUI.Label(new Rect(0, 1 * 70, 790, 64), "Restart level?");
        else if (onMap)
            GUI.Label(new Rect(0, 1 * 70, 790, 64), "Return to main menu? All progress will be lost.");
        else
            GUI.Label(new Rect(0, 1 * 70, 790, 64), "Return to the map?");
        if (GUI.Button(new Rect(0, 3 * 70, 790, 64), "Yes")) { Return_Yes(); }
        if (GUI.Button(new Rect(0, 4 * 70, 790, 64), "No")) { Return_No();  }
        GUI.EndGroup();
    }

    void OnGUI()
    {
        GUI.skin = gSkin;
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Return_No();
        }
        else
            Return_Accept();
    }

    void Return_No()
    {
        quit = onMap = restart = false;
        this.enabled = false;
        GetComponent<PauseMenuScript>().enabled = true;
    }

    void Return_Yes()
    {
        this.enabled = false;

        Time.timeScale = 1;
        if (restart)
        {
            Application.LoadLevel(Application.loadedLevel);
        }
        else if (quit)
        {
            CurrentGameState.Restart();
            Application.Quit();
        }
        else if (onMap)
        {
            CurrentGameState.Restart();
            Application.LoadLevel(0);
        }
        else
        {
            CurrentGameState.previousPosition = CurrentGameState.previousPreviousPosition;
            Application.LoadLevel(1);
        }
    }
}
