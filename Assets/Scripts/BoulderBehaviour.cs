using UnityEngine;
using System.Collections;

public class BoulderBehaviour : MonoBehaviour {

    public float SlowTime = 2;
    public float SlowAmount = 5;
	
	private ParticleSystem ps;
	
	bool hitGround = false;
	Vector3 hitPos;

    void Start()
    {
		ps = gameObject.GetComponentInChildren<ParticleSystem>();
		ps.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.z < ObstacleController.PLAYER.transform.position.z - 64)
        {
            Destroy(gameObject);
        }
		ps.transform.rotation = Quaternion.Euler(0,0,0);
		if (!hitGround)
			ps.transform.position = transform.position + new Vector3(0,-0.9f,0);
		else
			ps.transform.position = hitPos;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            gameObject.GetComponent<Rigidbody>().AddExplosionForce((other.GetComponent<HeroMovement>().CurrentSpeed / 4)*10, other.transform.position, 0);
            Physics.IgnoreCollision(gameObject.collider, other);
            other.GetComponent<HeroMovement>().SlowHero(SlowTime, SlowAmount);
        }
		
		if (other.tag.Equals("Soldier"))
		{
			other.GetComponent<EnemyAttack>().KillSelf();
			other.GetComponent<EnemyAttack>().AddExplosion(600,this.transform.position);
		}
		
		if (other.tag.Equals("Road") && !hitGround) {
			ps.Play();
			hitGround = true;
			hitPos = ps.transform.position;
		}
    }
}
