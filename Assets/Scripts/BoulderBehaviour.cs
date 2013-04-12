using UnityEngine;
using System.Collections;

public class BoulderBehaviour : MonoBehaviour {

    public float SlowTime = 2;
    public float SlowAmount = 5;

    // Update is called once per frame
    void Update()
    {
        if (transform.position.z < ObstacleController.PLAYER.transform.position.z - 64)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            gameObject.GetComponent<Rigidbody>().AddExplosionForce(other.GetComponent<HeroMovement>().CurrentSpeed / 4, other.transform.position, 0);
            Physics.IgnoreCollision(gameObject.collider, other);
            other.GetComponent<HeroMovement>().SlowHero(SlowTime, SlowAmount);
        }
		
		if (other.tag.Equals("Soldier"))
		{
			other.GetComponent<EnemyAttack>().KillSelf();
			other.GetComponent<EnemyAttack>().AddExplosion(600,this.transform.position);
		}
    }
}
