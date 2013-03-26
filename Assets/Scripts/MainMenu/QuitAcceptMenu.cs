using UnityEngine;
using System.Collections;

public class QuitAcceptMenu : MonoBehaviour {

    public GUISkin gSkin;

    void Menu_Quit()
    {
        GUI.BeginGroup(new Rect(0, Screen.height / 2 - 100, Screen.width, Screen.height));

        GUI.Label(new Rect(0, 0 * 70, Screen.width, 64), "Do you really want to quit?");
        if (GUI.Button(new Rect(0, 1 * 70, Screen.width, 64), "Yes")) { Menu_Quit_Yes(); }
        if (GUI.Button(new Rect(0, 2 * 70, Screen.width, 64), "No")) { Menu_Quit_No(); }


        GUI.EndGroup();
    }

    void OnGUI()
    {
        GUI.skin = gSkin;
        Menu_Quit();
    }

    void Menu_Quit_Yes()
    {
        Application.Quit();
    }

    void Menu_Quit_No()
    {
        QuitAcceptMenu script = GetComponent<QuitAcceptMenu>();
        script.enabled = false;
        MainMenuScript script2 = GetComponent<MainMenuScript>();
        script2.enabled = true;
    }
}
