using UnityEngine;
using System.Collections;

public class ObstacleBehaviour : MonoBehaviour {
	
	public float SlowTime = 2;
	public float SlowAmount = 5;
	
	// Update is called once per frame
	void Update () {
        if (transform.position.z < ObstacleController.PLAYER.transform.position.z - 64)
        {
            Destroy(gameObject);
        }
	}
	
	void OnTriggerEnter(Collider other) {
		if (other.tag.Equals("Player")) {
			foreach(Rigidbody rb in this.GetComponentsInChildren<Rigidbody>())
			{
				rb.isKinematic = false;
				rb.AddExplosionForce(other.GetComponent<HeroMovement>().CurrentSpeed/4,other.transform.position + Vector3.up,0);
				
				Physics.IgnoreCollision(rb.gameObject.collider, other);
			}
			
			other.GetComponent<HeroMovement>().SlowHero(SlowTime,SlowAmount);
		}
	}
}
