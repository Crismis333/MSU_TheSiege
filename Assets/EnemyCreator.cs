using UnityEngine;
using System.Collections;

public class EnemyCreator : MonoBehaviour {

    private float countDown;
    public float CountDownTime = 2f;
    public GameObject Enemy;
    public GameObject Player;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (countDown <= 0)
        {
            float z = Player.transform.position.z;
            countDown = CountDownTime;

            Instantiate(Enemy, new Vector3(Random.Range(-6, 6), 1, z + 40), Enemy.transform.rotation);
        }
        countDown -= Time.deltaTime;
	}
}
