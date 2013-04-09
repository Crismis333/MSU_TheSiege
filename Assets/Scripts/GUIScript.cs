using UnityEngine;
using System.Collections;

public class GUIScript : MonoBehaviour {

    public GUISkin gSkin;
    public Texture2D runningSoldiers, runningGoal, damagebar, swordLeft, swordRight, bloodsplatter;
	
	private float currentZ;
	private float minZ = 0;
	private float maxZ;

	// Use this for initialization
	void Start () {
		maxZ = (LevelCreator.LengthConverter(LevelCreator.LEVEL_LENGTH)*64)-32;
	}
	
	// Update is called once per frame
	void Update () {
		currentZ = ObstacleController.PLAYER.transform.position.z;
	}

    void Level_Interface()
    {
        GUI.BeginGroup(new Rect(0, 0, Screen.width, Screen.height));
        GUI.Box(new Rect(Screen.width-225, 25, 200, 75), "");
        GUI.Label(new Rect(Screen.width - 225 + 15, 25 + 15, 200, 75), "Score: 9756");
        GUI.Label(new Rect(Screen.width - 225 + 15, 25 + 15*2, 200, 75), "Multiplier: x2");

        GUI.Box(new Rect(15, Screen.height - 65, Screen.width - 30, 50), "");
        GUI.DrawTexture(new Rect(15+currentZ/(maxZ-minZ)*Screen.width-50, Screen.height - 64, 50, 50), runningSoldiers);
        GUI.DrawTexture(new Rect(Screen.width-65, Screen.height - 64, 50, 50), runningSoldiers);

        GUI.DrawTexture(new Rect(Screen.width / 2 - 250, Screen.height - 90, 500, 12), damagebar);
        GUI.DrawTexture(new Rect(Screen.width / 2 - 250 - swordLeft.width / 2, Screen.height - 90 - swordLeft.height / 2 + 6, swordLeft.width, swordLeft.height), swordLeft);
        GUI.DrawTexture(new Rect(Screen.width / 2 + 250 - swordRight.width / 2, Screen.height - 90 - swordRight.height / 2 + 6, swordRight.width, swordRight.height), swordRight);
        GUI.DrawTexture(new Rect(Screen.width / 2 - bloodsplatter.width / 2, Screen.height - 90 - bloodsplatter.height / 2 + 6, bloodsplatter.width, bloodsplatter.height), bloodsplatter);
        GUI.EndGroup();
    }

    void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 80, 30), "Restart"))
        {

            Application.LoadLevel(Application.loadedLevel);

        }

        if (GUI.Button(new Rect(100, 10, 80, 30), "To Menu"))
        {
            //CurrentGameState.SetWin();
            Application.LoadLevel(1);

        }

        GUI.Label(new Rect(10, 50, 80, 30), "" + Time.timeSinceLevelLoad);
        GUI.skin = gSkin;
        Level_Interface();
    }
}
