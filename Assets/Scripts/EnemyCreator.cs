using UnityEngine;
using System.Collections;

public class EnemyCreator : MonoBehaviour
{

    private float countDown;
    public float CountDownTime = 2f;
    public GameObject Enemy;
    public GameObject Player;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (countDown <= 0)
        {
            float z = Player.transform.position.z;
            countDown = CountDownTime;
           // Enemy.transform.RotateAround(new Vector3(0, 1, 0), 180);
        //    Vector3 rot = new Vector3(Enemy.transform.eulerAngles.x, Enemy.transform.eulerAngles.y + 180, Enemy.transform.eulerAngles.z);
            Instantiate(Enemy, new Vector3(Random.Range(-6, 6), 1f, z + 40), Quaternion.AngleAxis(180, Vector3.up));
        }
        countDown -= Time.deltaTime;
    }
}
