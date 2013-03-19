using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RoadCreator : MonoBehaviour {

    public Transform Road;
	public Transform Side;
    public CharacterController Player;
	
    private Queue<Transform> Roads;
	private Queue<Transform> Sides;
    private List<Transform> DeleteMe;
	private List<Transform> DeleteMeSides;

    private int ModuleLength = 64;
	private int ModuleWidth = 16;

    private int ModuleCount = 3;

	// Use this for initialization
	void Start () {
        Roads = new Queue<Transform>();
		Sides = new Queue<Transform>();
        DeleteMe = new List<Transform>();
		DeleteMeSides = new List<Transform>();

        for (int i = 0; i <= ModuleCount; i++)
        {
            Vector3 pos = transform.position;
            pos.z = (ModuleLength / 2) + (ModuleLength * i);
			
			Vector3 sidePosA = transform.position;
			sidePosA.x -= ModuleWidth;
			sidePosA.z = (ModuleLength / 2) + (ModuleLength * i);
			
			Vector3 sidePosB = transform.position;
			sidePosB.x += ModuleWidth;
			sidePosB.z = (ModuleLength / 2) + (ModuleLength * i);
			Quaternion sideRot = Quaternion.Euler(270,180,0);

            Transform road = Instantiate(Road, pos, transform.rotation) as Transform;
			
			Transform sideA = Instantiate(Side, sidePosA, transform.rotation) as Transform;
			Transform sideB = Instantiate(Side, sidePosB, sideRot) as Transform;

            Roads.Enqueue(road);
			Sides.Enqueue(sideA);
			Sides.Enqueue(sideB);
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
