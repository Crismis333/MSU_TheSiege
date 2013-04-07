using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObstacleCreator : MonoBehaviour {

    private float countDown;
    private float CountDownTime;

    public List<GameObject> ObstacleList;

	// Use this for initialization
	void Start () {
        CountDownTime = RatioToSeconds(ObstacleController.OBSTACLE_RATIO);
	}
	
	// Update is called once per frame
	void Update () {
        if (countDown <= 0)
        {
            float z = ObstacleController.PLAYER.transform.position.z;
            countDown = CountDownTime;

            int rIndex = Random.Range(0, ObstacleList.Count);

            Instantiate(ObstacleList[rIndex], new Vector3(Random.Range(-6, 6), 0.1f, z + 70), Quaternion.AngleAxis(180, Vector3.up));
        }
        countDown -= Time.deltaTime;
	}

    public float RatioToSeconds(int ratio)
    {
        return (5.333f * Mathf.Pow(10, -0.125076810788137f * ratio));
    }
}
