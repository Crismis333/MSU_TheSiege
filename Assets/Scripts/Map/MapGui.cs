using UnityEngine;
using System.Collections;

public class MapGui : MonoBehaviour {

    public GUISkin gSkin;
    [HideInInspector]
    public Location current_location;
    public Texture2D black;
    public MapMovementController mapmove;
    [HideInInspector]
    public bool stopped,started;

    private float countdown,startcountdown;
    private bool startHero, startReset;

    Vector2 scrollPos;

    void Map_Main()
    {
        //print("Timescale:" + Time.timeScale + " Countdown: " + countdown + " prevloc: " + CurrentGameState.previousPosition.x);
        GUI.BeginGroup(new Rect(Screen.width - 400 , Screen.height - 500, Screen.width, Screen.height));
        GUI.Box(new Rect(0, 0, 350, 200), "");

        if (current_location != null)
        {
            GUI.Label(new Rect(5, 5, 350, 50), current_location.LevelName);
            GUI.Label(new Rect(15 + 215, 5, 128, 128), "Length: " + toNumerals(current_location.difficulty_length));
            Vector2 sizeOfLabel = GUI.skin.GetStyle("Label").CalcSize(new GUIContent(current_location.description));
            if (sizeOfLabel.y > 150)
            {
                scrollPos = GUI.BeginScrollView(new Rect(0, 50, 350, 150), scrollPos, new Rect(0, 0, 0, sizeOfLabel.y), false, true);
                GUI.Label(new Rect(15, 0, 335, sizeOfLabel.y), current_location.description);
                GUI.EndScrollView();
            }
            else
                GUI.Label(new Rect(15, 50, 335, sizeOfLabel.y), current_location.description);
        }
        GUI.Box(new Rect(0, 200, 350, 250), "");
        GUI.Label(new Rect(15 + 40, 215, 128, 128), "Difficulty:");
        
        GUI.Label(new Rect(15 + 15, 215 + 1 * 20, 128, 128), "Soldiers:");
        GUI.Label(new Rect(15 + 15, 215 + 2 * 20, 128, 128), "Pits:");
        GUI.Label(new Rect(15 + 15, 215 + 3 * 20, 128, 128), "Obstacles:");
        GUI.Label(new Rect(15 + 15, 215 + 4 * 20, 128, 128), "Catapults:");

        GUI.Label(new Rect(15 + 40, 215 + 6 * 20, 128, 128), "Stats:");
        GUI.Label(new Rect(15 + 15, 215 + 7 * 20, 128, 128), "Jump Length:");
        GUI.Label(new Rect(15 + 15, 215 + 8 * 20, 128, 128), "Running Speed:");
        GUI.Label(new Rect(15 + 15, 215 + 9 * 20, 128, 128), "Adrenaline Rush:");

        GUI.color = Color.red;
        if (CurrentGameState.soldierModifier > 0)
            GUI.Label(new Rect(15 + 40 + 100, 215 + 1 * 20, 128, 128), "- " + toNumerals(CurrentGameState.soldierModifier));
        if (CurrentGameState.pitModifier > 0)
            GUI.Label(new Rect(15 + 40 + 100, 215 + 2 * 20, 128, 128), "- " + toNumerals(CurrentGameState.pitModifier));
        if (CurrentGameState.obstacleModifier > 0)
            GUI.Label(new Rect(15 + 40 + 100, 215 + 3 * 20, 128, 128), "- " + toNumerals(CurrentGameState.obstacleModifier));
        if (CurrentGameState.catapultModifier > 0)
            GUI.Label(new Rect(15 + 40 + 100, 215 + 4 * 20, 128, 128), "- " + toNumerals(CurrentGameState.catapultModifier));
        GUI.Label(new Rect(15 + 40 + 100, 215 + 7 * 20, 128, 128), "  " + toNumerals(fromFloatToInt(CurrentGameState.jumpLengthModifier)));
        GUI.Label(new Rect(15 + 40 + 100, 215 + 8 * 20, 128, 128), "  " + toNumerals(fromFloatToInt(CurrentGameState.moveSpeedModifier)));
        GUI.Label(new Rect(15 + 40 + 100, 215 + 9 * 20, 128, 128), "  " + toNumerals(fromFloatToInt(CurrentGameState.slowDownModifier)));
        if (current_location != null)
        {
            GUI.color = Color.white;
            GUI.Label(new Rect(15 + 100, 215 + 1 * 20, 128, 128), toNumerals(current_location.difficulty_soldier));
            GUI.Label(new Rect(15 + 100, 215 + 2 * 20, 128, 128), toNumerals(current_location.difficulty_pits));
            GUI.Label(new Rect(15 + 100, 215 + 3 * 20, 128, 128), toNumerals(current_location.difficulty_obstacles));
            GUI.Label(new Rect(15 + 100, 215 + 4 * 20, 128, 128), toNumerals(current_location.difficulty_catapults));

            GUI.color = Color.red;
            if ((current_location.modifiers.Contains(Modifier.Soldier)))
                GUI.Label(new Rect(15, 215 + 1 * 20, 128, 128), "-");
            if ((current_location.modifiers.Contains(Modifier.Pit)))
                GUI.Label(new Rect(15, 215 + 2 * 20, 128, 128), "-");
            if ((current_location.modifiers.Contains(Modifier.Obstacle)))
                GUI.Label(new Rect(15, 215 + 3 * 20, 128, 128), "-");
            if ((current_location.modifiers.Contains(Modifier.Catapult)))
                GUI.Label(new Rect(15, 215 + 4 * 20, 128, 128), "-");
            if ((current_location.modifiers.Contains(Modifier.Jump)))
                GUI.Label(new Rect(15, 215 + 7 * 20, 128, 128), "+");
            if ((current_location.modifiers.Contains(Modifier.MoveSpeed)))
                GUI.Label(new Rect(15, 215 + 8 * 20, 128, 128), "+");
            if ((current_location.modifiers.Contains(Modifier.SlowDown)))
                GUI.Label(new Rect(15, 215 + 9 * 20, 128, 128), "+");
            GUI.color = Color.white;
            
            if (GUI.Button(new Rect(200, 215 + 10 * 20, 80, 30), "to battle!")) { Battle_Pressed(); }

        }
        GUI.color = Color.white;
        GUI.EndGroup();
        if (stopped)
        {
            GUI.BeginGroup(new Rect(0, 0, Screen.width, Screen.height));
            GUI.color = new Color(1,1,1,Mathf.Lerp(0, 1, 1-countdown));
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), black);
            GUI.EndGroup();
        }

        if (started)
        {
            GUI.BeginGroup(new Rect(0, 0, Screen.width, Screen.height));
            GUI.color = new Color(1, 1, 1, Mathf.Lerp(1, 0, 1-countdown));
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), black);
            GUI.EndGroup();
        }
    }

    void Battle_Pressed()
    {
        if (current_location.difficulty_soldier - CurrentGameState.soldierModifier < 1)
            ObstacleController.SOLDIER_RATIO = 1;
        else
            ObstacleController.SOLDIER_RATIO = current_location.difficulty_soldier - CurrentGameState.soldierModifier;
        if (current_location.difficulty_obstacles - CurrentGameState.obstacleModifier < 1)
            ObstacleController.OBSTACLE_RATIO = 1;
        else
            ObstacleController.OBSTACLE_RATIO = current_location.difficulty_obstacles - CurrentGameState.obstacleModifier;
        if (current_location.difficulty_pits - CurrentGameState.pitModifier < 1)
            LevelCreator.PIT_RATIO = 1;
        else
            LevelCreator.PIT_RATIO = current_location.difficulty_pits - CurrentGameState.pitModifier;
        if (current_location.difficulty_catapults - CurrentGameState.catapultModifier < 1)
            ObstacleController.CATAPULT_RATIO = 1;
        else
            ObstacleController.CATAPULT_RATIO = current_location.difficulty_catapults - CurrentGameState.catapultModifier;

        LevelCreator.LEVEL_LENGTH = current_location.difficulty_length;
		
		ObstacleController.JUMP_MODIFIER = CurrentGameState.jumpLengthModifier;
		ObstacleController.MOVEMENT_MODIFIER = CurrentGameState.moveSpeedModifier;
		ObstacleController.CHARGE_MODIFIER = CurrentGameState.slowDownModifier;

        CurrentGameState.previousPreviousPosition = CurrentGameState.previousPosition;
        CurrentGameState.previousPosition = current_location.transform.position;
        CurrentGameState.hero.MoveToLoc(current_location);
        CurrentGameState.loc = null;

        //CurrentGameState.locID = current_location.levelID;
        current_location.ActivateRigidBody();
        //CurrentGameState.completedlevels.Add(current_location.levelID);
        Screen.lockCursor = true;
		
		LevelCreator.SIDE_MODULE_LIST.Clear();
        foreach (GameObject go in current_location.SideModules)
        {
            LevelCreator.SIDE_MODULE_LIST.Add(go.name);
        }

        if (current_location.SpecialModule != null)
            LevelCreator.SPECIAL_MODULE = current_location.SpecialModule.name;
        LevelCreator.SPECIAL_PART_COUNT = current_location.SpecialPartCount;
        LevelCreator.DEFAULT_ROAD = current_location.DefaultRoad.name;

        started = false;
        stopped = true;
        countdown = 1f;
        startcountdown = 3f;
        //Application.LoadLevel(2);

    }

    void OnGUI()
    {
        GUI.skin = gSkin;
        if (!started && !stopped && Input.GetKeyDown(KeyCode.Escape))
        {
            this.enabled = false;
            transform.parent.gameObject.GetComponent<MapMovementController>().enabled = false;
            GetComponent<PauseMenuScript>().enabled = true;
        }
        else
            Map_Main();
    }

    public void ResetScroll()
    {
        scrollPos = Vector2.zero;
    }

    void Start()
    {
        
        ResetScroll();
        stopped = false;
        startReset = true;
        startHero = true;
        started = true;
        Time.timeScale = 50;
        countdown = 1f;
        startcountdown = 50f;
        Screen.lockCursor = true;

    }

    void Update()
    {
        if (startHero)
        {
            startHero = false;
            CurrentGameState.CreateHero();
            CurrentGameState.hero.transform.position = CurrentGameState.previousPosition;
            CurrentGameState.hero.MoveToLoc(CurrentGameState.loc);
        }
        if (stopped || started)
        {
            startcountdown -= Time.deltaTime;
            if (startcountdown < 0)
                countdown -= 0.02f;

            if (started)
            {
                if (startReset)
                {
                    if (startcountdown < 0)
                    {
                        if (mapmove != null)
                            mapmove.CenterCamera(CurrentGameState.loc.transform);
                        Time.timeScale = 1;
                        GameObject o = GameObject.Find("PreviousLineCreator");
                        o.GetComponent<PreviousLines>().Init(CurrentGameState.hero);
                        startReset = false;
                        Screen.lockCursor = false;
                    }
                }
                if (countdown < 0)
                {
                    countdown = 0;
                    started = false;
                }

            }

            if (stopped && countdown < 0)
            {
                CurrentGameState.SetWinModifiers(current_location.modifiers, current_location.levelID);
                CurrentGameState.hero = null;
                Application.LoadLevel(2);
            }
        }
    }

    int fromFloatToInt(float val)
    {
        return ((int)((val - 1.0f) * 10)+1);
    }

    string toNumerals(int val)
    {
        switch (val)
        {
            case 1: return "I";
            case 2: return "II";
            case 3: return "III";
            case 4: return "IV";
            case 5: return "V";
            case 6: return "VI";
            case 7: return "VII";
            case 8: return "VIII";
            case 9: return "IX";
            case 10: return "X";
            default: return "I";
        }
    }
}
