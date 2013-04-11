using UnityEngine;
using System.Collections;

public class ObstacleController : MonoBehaviour {
	
	public static int SOLDIER_RATIO = 1;
    public static int CATAPULT_RATIO = 1;
    public static int PIT_RATIO = 1;
    public static int OBSTACLE_RATIO = 1;
	
	public static GameObject PLAYER = null;

    void Start()
    {
        PLAYER = GameObject.Find("HeroTemp");
    }
}
