using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class LevelCreator : MonoBehaviour {
	
	public int LEVEL_LENGTH = 1; //static
	public static int PIT_RATIO = 1;

	public List<string> SIDE_MODULE_LIST = new List<string>(); //static
	public List<string> ROAD_MODULE_LIST = new List<string>(); //static
	public List<string> SPECIAL_MODULE_LIST = new List<string>(); //static
	
	public string DEFAULT_ROAD = ""; //static
	public string DEFAULT_SIDE = ""; //static
	
	private List<GameObject> sideModules;
	private List<GameObject> roadModules;
	private List<GameObject> specialModules;
	
	private GameObject defaultRoad;

	private float moduleCount = 0;
	
	private Queue<GameObject> sidesA;
	private Queue<GameObject> sidesB;
	private Queue<GameObject> roads;
	
	private List<int> specialModuleIndices;
	
	// Use this for initialization
	void Start () {
		sideModules = new List<GameObject>();
		foreach(string s in SIDE_MODULE_LIST) {
			sideModules.Add(Resources.LoadAssetAtPath("Assets/Prefabs/Modules/SideModules/"+s+".prefab", typeof(GameObject)) as GameObject);
		}
		
		roadModules = new List<GameObject>();
		foreach(string s in ROAD_MODULE_LIST)	{
			roadModules.Add(Resources.LoadAssetAtPath("Assets/Prefabs/Modules/RoadModules/"+s+".prefab", typeof(GameObject)) as GameObject);
		}
		
		moduleCount = LengthConverter(LEVEL_LENGTH);
		
		specialModules = new List<GameObject>();
		specialModuleIndices = new List<int>();
		int i = 0;
		foreach(string s in SPECIAL_MODULE_LIST) {
			specialModules.Add(Resources.LoadAssetAtPath("Assets/Prefabs/Modules/SpecialModules/"+s+".prefab", typeof(GameObject)) as GameObject);	
			
			int sectionWidth = Mathf.RoundToInt(moduleCount / SPECIAL_MODULE_LIST.Count);
			float sectionStart = sectionWidth * i;
			float sectionEnd = sectionWidth * i+1;

			i++;
			
			specialModuleIndices.Add(RandomGaussian(sectionStart,sectionEnd));
		}
		
		defaultRoad = Resources.LoadAssetAtPath("Assets/Prefabs/Modules/RoadModules/"+DEFAULT_ROAD+".prefab", typeof(GameObject)) as GameObject;
		
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
			
			if (specialModuleIndices.Contains(i)){	
				continue;
			}
			
			if (IsDefaultRoad())
			{
				tmp = Instantiate(defaultRoad, pos, defaultRoad.transform.rotation) as GameObject;
			}
			else
			{
				int rIndex = Random.Range(0,roadModules.Count - 1);
				tmp = Instantiate(roadModules[rIndex],pos,roadModules[rIndex].transform.rotation) as GameObject;
			}
			
			roads.Enqueue(tmp);
		}
	}
	
	private void CreateSides(Queue<GameObject> sides, bool left) {
		int transitionState = -1;
		int prevSide = -1;
		int randomSide = 0;
		int variationCounter = 0;
		int variationCap = 7;
		bool end = false;
		bool start = false;
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
			
			if (specialModuleIndices.Contains(i)){	
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
			
				switch (transitionState)
				{
				case -1:
					side = Resources.LoadAssetAtPath("Assets/Prefabs/Modules/SideModules/"+tmpName+".prefab", typeof(GameObject)) as GameObject;
					break;
				case 0: 
					side = Resources.LoadAssetAtPath("Assets/Prefabs/Modules/SideModules/"+prevName+"_start.prefab", typeof(GameObject)) as GameObject;
					end = true;
					transitionState++;
					break;
				case 1:
					side = Resources.LoadAssetAtPath("Assets/Prefabs/Modules/SideModules/"+prevName + "_to_" + tmpName+".prefab", typeof(GameObject)) as GameObject;
					transitionState++;
					break;
				case 2:
					side = Resources.LoadAssetAtPath("Assets/Prefabs/Modules/SideModules/"+tmpName+"_start.prefab", typeof(GameObject)) as GameObject;
					transitionState++;
					start = true;
					break;
				case 3:
					side = Resources.LoadAssetAtPath("Assets/Prefabs/Modules/SideModules/"+tmpName+".prefab", typeof(GameObject)) as GameObject;
					transitionState = -1;
					break;
				}
				
				tmp = Instantiate(side,pos,side.transform.rotation) as GameObject;
				
				if ((end && left) || (start && !left)) {
					Vector3 tmpScale = tmp.transform.localScale;
					tmpScale.y = -1;
					tmp.transform.localScale = tmpScale;
				}
				
				end = false;
				start = false;
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
	
	private float LengthConverter(int length)	{
		return Mathf.Round(2.8333f*length + 8.5f);
	}
	
	private int RandomGaussian(float start, float end) {
		float sum = 0.0f;
		
		for( int i = 0; i < 10; i++) {
			sum += Random.Range(start,end);
		}
		
		return Mathf.RoundToInt(sum/10.0f);
	}
	
	private bool IsDefaultRoad() {
		return !(Random.Range(1,10) <= PIT_RATIO);
	}
}
