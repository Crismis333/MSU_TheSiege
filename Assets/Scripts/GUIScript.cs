using UnityEngine;
using System.Collections;

public class GUIScript : MonoBehaviour {

    

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
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
    }
}
