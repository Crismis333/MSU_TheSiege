using UnityEngine;
using System.Collections;

public class MainMenuScript : MonoBehaviour {
	
	
	public GUISkin gSkin;
    public Texture2D black;
    public MusicVolumeSetter music;

    private float countdown;
    private bool stopped, started;

	void Menu_Main() {
        GUI.BeginGroup(new Rect(0, Screen.height / 2 - 100, Screen.width, Screen.height));

        if (GUI.Button(new Rect(0, 0*70, Screen.width-30, 64), "Start Game")) { Menu_Main_Start_Game(); }
        if (GUI.Button(new Rect(0, 1*70, Screen.width-30, 64), "Options")) { Menu_Main_Options(); }
        if (GUI.Button(new Rect(0, 2*70, Screen.width - 30, 64), "Highscores")) { Menu_Main_Highscores(); }
        if (GUI.Button(new Rect(0, 3*70, Screen.width-30, 64), "Quit")) { Menu_Main_Quit(); }
		
		GUI.EndGroup();
        if (stopped)
        {
            GUI.BeginGroup(new Rect(0, 0, Screen.width, Screen.height));
            GUI.color = new Color(1, 1, 1, Mathf.Lerp(0, 1, 1 - countdown));
            music.volume = Mathf.Lerp(OptionsValues.musicVolume, 0, 1 - countdown);
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), black);
            GUI.EndGroup();
        }
        else if (started)
        {
            GUI.BeginGroup(new Rect(0, 0, Screen.width, Screen.height));
            GUI.color = new Color(1, 1, 1, Mathf.Lerp(1, 0, 1-countdown));
            music.volume = Mathf.Lerp(0, OptionsValues.musicVolume, 1 - countdown);
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), black);
            GUI.EndGroup();
        }
        else
            music.volume = OptionsValues.musicVolume;
	}

    void Menu_Main_Start_Game() {
        if (!started && !stopped)
        {
            //Application.LoadLevel(1);
            started = false;
            stopped = true;
            countdown = 1f;
            music.useGlobal = false;
        }
    }

    void Menu_Main_Options() {
        if (!started && !stopped)
        {
            this.enabled = false;
            GetComponent<OptionsMenuScript>().Menu_Options_Startup();
        }
    }

    void Menu_Main_Highscores()
    {
        if (!started && !stopped)
        {
            this.enabled = false;
            GetComponent<HighScoreMenuScript>().enabled = true;
        }
    }

    void Menu_Main_Quit() {
        if (!started && !stopped)
        {
            this.enabled = false;
            GetComponent<QuitAcceptMenu>().enabled = true;
        }
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

    void Start()
    {
        music.useGlobal = false;
        started = true;
        countdown = 1f;

        if (!PlayerPrefs.HasKey("MusicVolume"))
            PlayerPrefs.SetFloat("MusicVolume", 0.5f);
        else
            OptionsValues.musicVolume = PlayerPrefs.GetFloat("MusicVolume");


        if (!PlayerPrefs.HasKey("SFXVolume"))
            PlayerPrefs.SetFloat("SFXVolume", 0.5f);
        else
            OptionsValues.sfxVolume = PlayerPrefs.GetFloat("SFXVolume");

        if (!PlayerPrefs.HasKey("Highscore1Name"))
            PlayerPrefs.SetString("Highscore1Name", "John");
        if (!PlayerPrefs.HasKey("Highscore1Score"))
            PlayerPrefs.SetString("Highscore1Score", "1000000");
        if (!PlayerPrefs.HasKey("Highscore2Name"))
            PlayerPrefs.SetString("Highscore2Name", "John");
        if (!PlayerPrefs.HasKey("Highscore2Score"))
            PlayerPrefs.SetString("Highscore2Score", "1000000");
        if (!PlayerPrefs.HasKey("Highscore3Name"))
            PlayerPrefs.SetString("Highscore3Name", "John");
        if (!PlayerPrefs.HasKey("Highscore3Score"))
            PlayerPrefs.SetString("Highscore3Score", "1000000");
        if (!PlayerPrefs.HasKey("Highscore4Name"))
            PlayerPrefs.SetString("Highscore4Name", "John");
        if (!PlayerPrefs.HasKey("Highscore4Score"))
            PlayerPrefs.SetString("Highscore4Score", "1000000");
        if (!PlayerPrefs.HasKey("Highscore5Name"))
            PlayerPrefs.SetString("Highscore5Name", "John");
        if (!PlayerPrefs.HasKey("Highscore5Score"))
            PlayerPrefs.SetString("Highscore5Score", "1000000");
        if (!PlayerPrefs.HasKey("Highscore6Name"))
            PlayerPrefs.SetString("Highscore6Name", "John");
        if (!PlayerPrefs.HasKey("Highscore6Score"))
            PlayerPrefs.SetString("Highscore6Score", "1000000");
        if (!PlayerPrefs.HasKey("Highscore7Name"))
            PlayerPrefs.SetString("Highscore7Name", "John");
        if (!PlayerPrefs.HasKey("Highscore7Score"))
            PlayerPrefs.SetString("Highscore7Score", "1000000");
        if (!PlayerPrefs.HasKey("Highscore8Name"))
            PlayerPrefs.SetString("Highscore8Name", "John");
        if (!PlayerPrefs.HasKey("Highscore8Score"))
            PlayerPrefs.SetString("Highscore8Score", "1000000");
        if (!PlayerPrefs.HasKey("Highscore9Name"))
            PlayerPrefs.SetString("Highscore9Name", "John");
        if (!PlayerPrefs.HasKey("Highscore9Score"))
            PlayerPrefs.SetString("Highscore9Score", "1000000");
        if (!PlayerPrefs.HasKey("Highscore10Name"))
            PlayerPrefs.SetString("Highscore10Name", "John");
        if (!PlayerPrefs.HasKey("Highscore10Score"))
            PlayerPrefs.SetString("Highscore10Score", "1000000");
        PlayerPrefs.Save();
    }

    void Update()
    {
        if (stopped || started)
        {
            
            countdown -= 0.02f;

            if (started)
            {
                if (countdown < 0)
                {
                    countdown = 0;
                    started = false;
                    music.useGlobal = true;
                }

            }

            if (stopped && countdown < 0)
            {
                Application.LoadLevel(1);
            }
        }
    }
}
