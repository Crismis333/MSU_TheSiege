using UnityEngine;
using System.Collections;

public class GUIScript : MonoBehaviour {

    

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void Level_Interface()
    {
        GUI.BeginGroup(new Rect(0, 0, Screen.width, Screen.height));
        GUI.Box(new Rect(Screen.width-225, 25, 200, 75), "");
        GUI.Label(new Rect(Screen.width - 225 + 15, 25 + 15, 200, 75), "Score: 9756");
        GUI.Label(new Rect(Screen.width - 225 + 15, 25 + 15*2, 200, 75), "Multiplier: x2");
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

            Application.LoadLevel(1);

        }

        GUI.Label(new Rect(10, 50, 80, 30), "" + Time.timeSinceLevelLoad);
        Level_Interface();
    }
}
