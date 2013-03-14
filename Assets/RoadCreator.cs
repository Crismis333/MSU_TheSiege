using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RoadCreator : MonoBehaviour {

    public Transform Road;
    public CharacterController Player;

    private Queue<Transform> Roads;
    private List<Transform> DeleteMe;

    private int ModuleLength = 64;

    private int ModuleCount = 2;

	// Use this for initialization
	void Start () {
        Roads = new Queue<Transform>();
        DeleteMe = new List<Transform>();

        for (int i = 0; i <= ModuleCount; i++)
        {
            Vector3 pos = transform.position;
            pos.z = (ModuleLength / 2) + (ModuleLength * i);

            Transform road = Instantiate(Road, pos, transform.rotation) as Transform;

            Roads.Enqueue(road);
        }
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 tmpPos = Roads.Peek().transform.position;

        if (tmpPos.z <= Player.transform.position.z - ModuleLength / 2)
        {
            Vector3 pos = tmpPos;
            pos.z += ModuleLength + (ModuleLength * ModuleCount);
            
            Transform newRoad = Instantiate(Road, pos, transform.rotation) as Transform;

            Roads.Enqueue(newRoad);

            DeleteMe.Add(Roads.Dequeue());
        }

        Transform deleteThis = null;

        foreach (Transform t in DeleteMe)
        {
            if (t.position.z <= Player.transform.position.z - ModuleLength)
                deleteThis = t;
        }

        if (deleteThis != null)
        {
            DeleteMe.Remove(deleteThis);
            Destroy(deleteThis.gameObject);
        }
	}
}
