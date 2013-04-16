using UnityEngine;
using System.Collections;

public class HighScoreMenuScript : MonoBehaviour {

    public GUISkin gSkin;
    public bool mainMenu;

    void Menu_HighScore()
    {
        GUI.BeginGroup(new Rect(Screen.width / 2 - 395, Screen.height / 2 - 7.5f * 35, 790, 15 * 35));

        GUI.Box(new Rect(0, 0, 790, 15 * 35), "");
        GUI.Label(new Rect(0, 1 * 35, 790, 64), "I");
        GUI.Label(new Rect(50, 1 * 35, 790, 64), ": " + PlayerPrefs.GetString("Highscore1Name"));
        GUI.Label(new Rect(400, 1 * 35, 790, 64), PlayerPrefs.GetString("Highscore1Score"));
        GUI.Label(new Rect(0, 2 * 35, 790, 64), "II");
        GUI.Label(new Rect(50, 2 * 35, 790, 64), ": " + PlayerPrefs.GetString("Highscore2Name"));
        GUI.Label(new Rect(400, 2 * 35, 790, 64), PlayerPrefs.GetString("Highscore2Score"));
        GUI.Label(new Rect(0, 3 * 35, 790, 64), "III");
        GUI.Label(new Rect(50, 3 * 35, 790, 64), ": " + PlayerPrefs.GetString("Highscore3Name"));
        GUI.Label(new Rect(400, 3 * 35, 790, 64), PlayerPrefs.GetString("Highscore3Score"));
        GUI.Label(new Rect(0, 4 * 35, 790, 64), "IV");
        GUI.Label(new Rect(50, 4 * 35, 790, 64), ": " + PlayerPrefs.GetString("Highscore4Name"));
        GUI.Label(new Rect(400, 4 * 35, 790, 64), PlayerPrefs.GetString("Highscore4Score"));
        GUI.Label(new Rect(0, 5 * 35, 790, 64), "V");
        GUI.Label(new Rect(50, 5 * 35, 790, 64), ": " + PlayerPrefs.GetString("Highscore5Name"));
        GUI.Label(new Rect(400, 5 * 35, 790, 64), PlayerPrefs.GetString("Highscore5Score"));
        GUI.Label(new Rect(0, 6 * 35, 790, 64), "VI");
        GUI.Label(new Rect(50, 6 * 35, 790, 64), ": " + PlayerPrefs.GetString("Highscore6Name"));
        GUI.Label(new Rect(400, 6 * 35, 790, 64), PlayerPrefs.GetString("Highscore6Score"));
        GUI.Label(new Rect(0, 7 * 35, 790, 64), "VII");
        GUI.Label(new Rect(50, 7 * 35, 790, 64), ": " + PlayerPrefs.GetString("Highscore7Name"));
        GUI.Label(new Rect(400, 7 * 35, 790, 64), PlayerPrefs.GetString("Highscore7Score"));
        GUI.Label(new Rect(0, 8 * 35, 790, 64), "VIII");
        GUI.Label(new Rect(50, 8 * 35, 790, 64), ": " + PlayerPrefs.GetString("Highscore8Name"));
        GUI.Label(new Rect(400, 8 * 35, 790, 64), PlayerPrefs.GetString("Highscore8Score"));
        GUI.Label(new Rect(0, 9 * 35, 790, 64), "IX");
        GUI.Label(new Rect(50, 9 * 35, 790, 64), ": " + PlayerPrefs.GetString("Highscore9Name"));
        GUI.Label(new Rect(400, 9 * 35, 790, 64), PlayerPrefs.GetString("Highscore9Score"));
        GUI.Label(new Rect(0, 10 * 35, 790, 64), "X");
        GUI.Label(new Rect(50, 10 * 35, 790, 64), ": " + PlayerPrefs.GetString("Highscore10Name"));
        GUI.Label(new Rect(400, 10 * 35, 790, 64), PlayerPrefs.GetString("Highscore10Score"));
        if (GUI.Button(new Rect(0, 12 * 35, 790, 64), "Return")) { Return(); }
        GUI.EndGroup();
    }

    void OnGUI()
    {
        GUI.skin = gSkin;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Return();
        }
        else
            Menu_HighScore();
    }

    void Return()
    {
        this.enabled = false;
        if (mainMenu)
            GetComponent<MainMenuScript>().enabled = true;
    }
}
