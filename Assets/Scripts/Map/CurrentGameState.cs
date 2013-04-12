using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum Modifier { None, Soldier, Obstacle, Pit, Catapult, Jump, MoveSpeed, SlowDown }

public class CurrentGameState : MonoBehaviour {

    public static int locID = 0;
    public static Location loc;
    public static bool JustStarted = true;
    public static int soldierModifier = 0;
    public static int obstacleModifier = 0;
    public static int pitModifier = 0;
    public static int catapultModifier = 0;
    public static float jumpLengthModifier = 1.0f;
    public static float moveSpeedModifier = 1.0f;
    public static float slowDownModifier = 1.0f;
    public static List<int> completedlevels = new List<int>();
    public static Hero hero;
    public static Vector3 previousPosition, previousPreviousPosition;
    private static List<Modifier> wins;
    private static int nextLevel;
    

    public static void CreateHero()
    {
        GameObject o = Resources.LoadAssetAtPath("Assets/Prefabs/Map/HeroFigure.prefab", typeof(GameObject)) as GameObject;
        hero = (Instantiate(o) as GameObject).GetComponent<Hero>();
        hero.transform.position = loc.startLocation;

        if (loc.locations.Length > 0)
        {
            hero.LookAtLoc(loc.locations[0]);
        }
    }
    public static void SetWinModifiers(List<Modifier> modifiers, int levelID)
    {
        wins = modifiers;
        nextLevel = levelID;
    }

    public static void SetWin() 
    {
        locID = nextLevel;
        CurrentGameState.completedlevels.Add(locID);
        foreach (Modifier m in wins)
            IncreaseModifier(m);
    }

    public static void Restart()
    {
        locID = 0;
        loc = null;
        JustStarted = true;
        soldierModifier = obstacleModifier = pitModifier = catapultModifier = 0;
        jumpLengthModifier = moveSpeedModifier = slowDownModifier = 1.0f;
        completedlevels = new List<int>();
        hero = null;
        wins = null;
        nextLevel = 0;
    }

    private static void IncreaseModifier(Modifier mod) 
    {
        switch (mod)
        {
            case Modifier.Soldier: soldierModifier++; break;
            case Modifier.Obstacle: obstacleModifier++; break;
            case Modifier.Pit: pitModifier++; break;
            case Modifier.Catapult: catapultModifier++; break;
            case Modifier.Jump:
                {
                    if (jumpLengthModifier < 1.5f)
                        jumpLengthModifier += 0.1f; 
                    break;
                }
            case Modifier.MoveSpeed:
                {
                    if (moveSpeedModifier < 1.5f)
                        moveSpeedModifier += 0.1f;
                    break;
                }
            case Modifier.SlowDown:
                {
                    if (moveSpeedModifier < 1.5f)
                        moveSpeedModifier += 0.1f;
                    break;
                }
        }
    }
}
