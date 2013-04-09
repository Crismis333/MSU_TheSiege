using UnityEngine;
using System.Collections;

public class MapGui : MonoBehaviour {

    public GUISkin gSkin;
    public Location current_location;
    public Texture2D black;
    public MapMovementController mapmove;
    public bool stopped,started,startReset;
    public float countdown;

    //public Texture2D circleDiagram;
    //public Texture2D starColor;
    //public Object starDiagram;
    Vector2 scrollPos;

    /*private Vector2 starCenter = new Vector2(64, 63);
    private Vector2 starTip1 = new Vector2(64, 2);
    private Vector2 starTip2 = new Vector2(121, 42);
    private Vector2 starTip3 = new Vector2(103, 110);
    private Vector2 starTip4 = new Vector2(24, 110);
    private Vector2 starTip5 = new Vector2(6, 42);
    private int counter;*/

    void Map_Main()
    {
        GUI.BeginGroup(new Rect(Screen.width - 400 , Screen.height - 500, Screen.width, Screen.height));
        GUI.Box(new Rect(0, 0, 350, 200), "");

        if (current_location != null)
        {
            GUI.Label(new Rect(5, 5, 350, 50), current_location.name);
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
        //DrawStar(5, 4, 3, 2, 1);
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
            
            if (GUI.Button(new Rect(200, 215 + 10 * 20, 80, 30), "to battle!"))
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
				
				LevelCreator.LEVEL_LENGTH = current_location.difficulty_length;
				
                CurrentGameState.loc = null;
                //CurrentGameState.locID = current_location.levelID;
                current_location.ActivateRigidBody();
                //CurrentGameState.completedlevels.Add(current_location.levelID);
                Screen.lockCursor = true;
				
				foreach(GameObject go in current_location.PitModules) {
					LevelCreator.ROAD_MODULE_LIST.Add(go.name);
				}
				
				foreach(GameObject go in current_location.SideModules) {
					LevelCreator.SIDE_MODULE_LIST.Add(go.name);
				}
				
				foreach(GameObject go in current_location.SpecialModules) {
					LevelCreator.SPECIAL_MODULE_LIST.Add(go.name);
				}
				
				LevelCreator.DEFAULT_ROAD = current_location.DefaultRoad.name;
				
                stopped = true;
                countdown = 5;
                //Application.LoadLevel(2);
            }
        }
        GUI.color = Color.white;
        GUI.EndGroup();
        if (stopped)
        {
            GUI.BeginGroup(new Rect(0, 0, Screen.width, Screen.height));
            GUI.color = new Color(1,1,1,Mathf.Lerp(1, 0, countdown / 2f));
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), black);
            GUI.EndGroup();
        }

        if (started)
        {
            GUI.BeginGroup(new Rect(0, 0, Screen.width, Screen.height));
            GUI.color = new Color(1, 1, 1, Mathf.Lerp(0, 1, countdown / 2f));
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), black);
            GUI.EndGroup();
        }
    }
    
    void OnGUI()
    {
        GUI.skin = gSkin;
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
        started = true;
        Time.timeScale = 50;
        countdown = 50;
        Screen.lockCursor = true;
    }

    void Update()
    {
        if (stopped || started)
        {
            countdown -= Time.deltaTime;
            //print(countdown);

            if (started)
            {
                if (startReset)
                {
                    if (countdown < 3)
                    {
                        if (mapmove != null)
                            mapmove.CenterCamera(CurrentGameState.loc.transform);
                        Time.timeScale = 1;
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
                CurrentGameState.SetWinModifiers(current_location.modifiers,current_location.levelID);
                Application.LoadLevel(2);
            }
        }
    }

    void SetColor(int val)
    {
        if (val == 0)
            GUI.color = Color.white;
        else
            GUI.color = Color.red;
    }

    int fromFloatToInt(float val)
    {
        return ((int)((val - 1.0f) * 10)+1);
    }

    string toNumerals(int val)
    {
        switch (val)
        {
            case -6: return "I";
            case -5: return "I";
            case -4: return "I";
            case -3: return "I";
            case -2: return "I";
            case -1: return "I";
            case 0: return "I";
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
            case 11: return "XI";
            case 12: return "XII";
            case 13: return "XIII";
            case 14: return "XIV";
            case 15: return "XV";
            case 16: return "XVI";
            case 17: return "XVII";
            case 18: return "XVIII";
            case 19: return "XIX";
            case 20: return "XX";
            default: return "I";
        }
    }
}
