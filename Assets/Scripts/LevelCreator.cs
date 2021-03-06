using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class LevelCreator : MonoBehaviour {
	
	public static int LEVEL_LENGTH = 1;
	public static int PIT_RATIO = 1;

	public static List<string> SIDE_MODULE_LIST = new List<string>();
    public static string SPECIAL_MODULE = "";
	public static string DEFAULT_ROAD = "";

    public static int SPECIAL_PART_COUNT = 0;
	
	private List<GameObject> sideModules;
	private List<GameObject> roadModules;

	private GameObject specialModule;
	private GameObject defaultRoad;

	private float moduleCount = 0;
	
	private Queue<GameObject> sidesA;
	private Queue<GameObject> sidesB;
	private Queue<GameObject> roads;
	
	private int specialModuleIndex = -1;
	
	// Use this for initialization
	void Start () {
		sideModules = new List<GameObject>();
		foreach(string s in SIDE_MODULE_LIST) {
			sideModules.Add(Resources.Load("SideModules/Sides/"+s, typeof(GameObject)) as GameObject);
		}
		
		moduleCount = LengthConverter(LEVEL_LENGTH);

        specialModule = Resources.Load("SpecialModules/" + SPECIAL_MODULE, typeof(GameObject)) as GameObject;

        if (specialModule != null)
            specialModuleIndex = RandomGaussian(0, moduleCount);

		defaultRoad = Resources.Load("RoadModules/"+DEFAULT_ROAD, typeof(GameObject)) as GameObject;
		
		roads = new Queue<GameObject>();
		sidesA = new Queue<GameObject>();
		sidesB = new Queue<GameObject>();
		
		CreateRoads();
		CreateSides(sidesA, true);
		CreateSides(sidesB, false);
	}
	
	// Update is called once per frame
	void Update () {
		GameObject tmpRoad = roads.Peek();
		if (tmpRoad.transform.position.z <= ObstacleController.PLAYER.transform.position.z - 64) {
			Destroy(roads.Dequeue());
		}
		
		GameObject tmpSideA = sidesA.Peek();
		if (tmpSideA.transform.position.z <= ObstacleController.PLAYER.transform.position.z - 64) {
			Destroy(sidesA.Dequeue());
		}
		
		GameObject tmpSideB = sidesB.Peek();
		if (tmpSideB.transform.position.z <= ObstacleController.PLAYER.transform.position.z - 64) {
			Destroy(sidesB.Dequeue());
		}
	}
	
	private void CreateRoads() {
		for (int i = 0; i < moduleCount; i++) {
			GameObject tmp;
			Vector3 pos = transform.position;
			pos.z = 64*i;
			
			if (i >= specialModuleIndex && i < specialModuleIndex + SPECIAL_PART_COUNT){	
				continue;
			}

			tmp = Instantiate(defaultRoad, pos, defaultRoad.transform.rotation) as GameObject;
			
			roads.Enqueue(tmp);
		}
	}
	
	private void CreateSides(Queue<GameObject> sides, bool left) {
		int transitionState = -1;
		int prevSide = -1;
		int randomSide = 0;
		int variationCounter = 0;
		int variationCap = 7;
		for (int i = 0; i < moduleCount; i++) {
			GameObject tmp;
			Vector3 pos = transform.position;
			pos.z = 64*i;
			
			if(left) {
				pos.x -= 40;
			}
			else {
				pos.x += 40;
			}

            if (i >= specialModuleIndex && i < specialModuleIndex + SPECIAL_PART_COUNT)
            {
                continue;
            }
			

			if (transitionState == -1) {
				randomSide = Random.Range(0,sideModules.Count);
				if (variationCounter >= variationCap) {
					int q = randomSide;
					while(q == randomSide) {
						randomSide = Random.Range (0,sideModules.Count);
					}
				}
			}
			
			if (prevSide != -1)
			{
				string tmpName = Regex.Replace(sideModules[randomSide].name, @"[\d-]", string.Empty);
				string prevName = Regex.Replace(sideModules[prevSide].name, @"[\d-]", string.Empty);
				GameObject side = null;
				
				if (!tmpName.Equals(prevName) && transitionState == -1) {
					transitionState = 0;
					variationCounter = 0;
				}
				else{
					variationCounter++;
				}

                string realName = sideModules[randomSide].name;
			
				switch (transitionState)
				{
				case -1:
                    side = Resources.Load("SideModules/Sides/"+realName, typeof(GameObject)) as GameObject;
					break;
				case 0: 
					side = Resources.Load("SideModules/SideStarts/"+prevName+"_start", typeof(GameObject)) as GameObject;
					transitionState++;
					break;
				case 1:
					side = Resources.Load("SideModules/SideTransitions/"+prevName + "_to_" + tmpName, typeof(GameObject)) as GameObject;
					transitionState++;
					break;
				case 2:
					side = Resources.Load("SideModules/SideStarts/"+tmpName+"_start", typeof(GameObject)) as GameObject;
					transitionState++;
					break;
				case 3:
                    side = Resources.Load("SideModules/Sides/" + realName, typeof(GameObject)) as GameObject;
					transitionState = -1;
					break;
				}
				
				tmp = Instantiate(side,pos,side.transform.rotation) as GameObject;
				
				if (((transitionState == 1 || transitionState == 2) && left) || (transitionState == 3 && !left)) {
					Vector3 tmpScale = tmp.transform.localScale;
					tmpScale.y = -1;
					tmp.transform.localScale = tmpScale;
				}
			}
			else {
				tmp = Instantiate(sideModules[randomSide],pos,sideModules[randomSide].transform.rotation) as GameObject;
			}
			
			if (!left) {
				tmp.transform.rotation *= Quaternion.Euler(0, 0, 180);
			}
			
			sides.Enqueue(tmp);
			
			if (transitionState == -1 || transitionState == 3) {
				prevSide = randomSide;
			}
		}
	}
	
	public static float LengthConverter(int length)	{
		return Mathf.Round(2.8333f*length + 8.5f);
	}
	
	private int RandomGaussian(float start, float end) {
		float sum = 0.0f;
		
		for( int i = 0; i < 10; i++) {
			sum += Random.Range(start,end);
		}
		
		return Mathf.RoundToInt(sum/10.0f);
	}
}
