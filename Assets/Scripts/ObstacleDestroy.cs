using UnityEngine;
using System.Collections;

public class ObstacleDestroy : MonoBehaviour {

	// Update is called once per frame
	void Update () {
        if (transform.position.z < ObstacleController.PLAYER.transform.position.z - 5)
        {
            Destroy(gameObject);
        }
	
	}
}
