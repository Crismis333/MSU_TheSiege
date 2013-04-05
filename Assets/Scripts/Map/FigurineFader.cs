using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Location))]
public class FigurineFader : MonoBehaviour {

    private static float duration = 10f;
    private float lerp;
    private Renderer[] rs;
    private int counter;
	// Use this for initialization
	void Start () {
        lerp = 0f;
        counter = 0;
	}
	
	// Update is called once per frame
	void Update () {
        
	    if (Camera.main.GetComponent<MapGui>().current_location == this.GetComponent<Location>())
        {
            counter++;
            lerp = Mathf.PingPong(counter, duration) / duration;
        }
        else {
            lerp = 0f;
            counter = 0;
        }
        rs = this.gameObject.GetComponentsInChildren<Renderer>();
        for (int i = 0; i < rs.Length; i++)
            rs[i].material.SetFloat("_Blend", lerp);
        print(lerp);
	}
}
