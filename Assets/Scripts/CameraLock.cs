using UnityEngine;
using System.Collections;

public class CameraLock : MonoBehaviour {

    public Transform Target;

    public float OffsetX;
    public float OffsetY;
    public float OffsetZ;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 newPos = Target.transform.position;

        newPos.x += OffsetX;
        newPos.y += OffsetY;
        newPos.z += OffsetZ;

        transform.position = newPos;
	}
}
