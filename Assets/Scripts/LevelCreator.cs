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
		int prevSide = -1;
		for (int i = 0; i < moduleCount; i++) {
			GameObject tmp;
			Vector3 pos = transform.position;
			pos.z = 64*i;
			
			if(left) {
				pos.x -= 40;
			}
			else {
				pos.x += 24;
			}
			
			if (specialModuleIndices.Contains(i)){	
				continue;
			}
			
			int randomSide = Random.Range(0,sideModules.Count);
			
			if (prevSide != -1)
			{
				//string tmpName = Regex.Replace(sideModules[randomSide].name, @"[\d-]", string.Empty);
				//string prevName = Regex.Replace(sideModules[prevSide].name, @"[\d-]", string.Empty);
				
				tmp = Instantiate(sideModules[randomSide],pos,sideModules[randomSide].transform.rotation) as GameObject;
			}
			else {
				tmp = Instantiate(sideModules[randomSide],pos,sideModules[randomSide].transform.rotation) as GameObject;
			}
			
			if (left) {
				tmp.transform.localScale = new Vector3(-1,1,1);
				Vector3 newCent = new Vector3(-16,5,0);
				tmp.GetComponent<BoxCollider>().center = newCent;
			}
			
			sides.Enqueue(tmp);
			
			prevSide = randomSide;
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
