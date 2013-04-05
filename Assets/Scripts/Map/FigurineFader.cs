using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Location))]
public class FigurineFader : MonoBehaviour {

    private static float duration = 0.4f;
    private float lerp;
    private Renderer[] rs;
	// Use this for initialization
	void Start () {
        lerp = 0f;
	}
	
	// Update is called once per frame
	void Update () {
        
	    if (Camera.main.GetComponent<MapGui>().current_location == this.GetComponent<Location>())
        {
            lerp = Mathf.PingPong(Time.time, duration) / duration;
        }
        else {
            lerp = 0f;
        }
        rs = this.gameObject.GetComponentsInChildren<Renderer>();
        for (int i = 0; i < rs.Length; i++)
            rs[i].material.SetFloat("_Blend", lerp);
        print(lerp);
	}
}
