using UnityEngine;
using System.Collections;

public class ObstacleController : MonoBehaviour {
	
	public int SoldierRatio = 1;
	
	private GameObject player = null;
	
	public GameObject Player {
		get { return player; }
	}
	
	// Use this for initialization
	void Start () {
		player = GameObject.Find("HeroTemp");
	}
}
