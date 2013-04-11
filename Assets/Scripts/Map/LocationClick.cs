using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Location))]
public class LocationClick : MonoBehaviour {

	
	// Update is called once per frame
    void OnMouseDown()
    {
        if (this.GetComponent<Location>().isChildOfCurrent())
        {
            Camera.main.GetComponent<MapGui>().current_location = this.GetComponent<Location>();
            Camera.main.GetComponent<MapGui>().ResetScroll();
        }
	}
}
