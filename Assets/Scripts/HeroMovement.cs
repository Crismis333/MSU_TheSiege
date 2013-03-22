using UnityEngine;
using System.Collections;

public class HeroMovement : MonoBehaviour {

    public float MoveSpeed = 5.0f;
    public float StrafeSpeed = 3.0f;

    public float SpeedUp = 2.0f;
    public float SpeedDown = 2.0f;

    public float Gravity = 5.0f;

    private CollisionFlags collisionFlag;

    private float verticalSpeed;
	
	public bool Slowed = false;
	private float slowTimer = 0.0f;
	private float slowMax = 0.0f;
	private float slowAmount = 0.0f;
	

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Run();
		
		if (Slowed) {			
			slowTimer -= Time.deltaTime;
			if (slowTimer <= 0.0f) {
				Slowed = false;
				slowTimer = 0.0f;
			}
		}
	}
	
	private void Run() {
        Vector3 moveDirection = new Vector3();

        float v = Input.GetAxisRaw("Vertical");
        float h = Input.GetAxisRaw("Horizontal");
		
		if(!Slowed) {
	        if (v > 0.1)
	            moveDirection.z = MoveSpeed + SpeedUp * v;
	        else if (v < -0.1)
	            moveDirection.z = MoveSpeed + SpeedDown * v;
	        else
	            moveDirection.z = MoveSpeed;
		}
		else {
			moveDirection.z = MoveSpeed - slowAmount * (slowTimer / slowMax);
		}
        
        moveDirection.x = StrafeSpeed * h;

        if (IsGrounded())
            verticalSpeed = 0.0f;
        else
            verticalSpeed -= Gravity * Time.deltaTime;

        moveDirection.y = verticalSpeed;

        moveDirection *= Time.deltaTime;

        CharacterController cc = GetComponent<CharacterController>();
        collisionFlag = cc.Move(moveDirection);
	}
	
	public void SlowHero(float time, float amount) {
		Slowed = true;
		slowTimer = time;
		slowMax = time;
		slowAmount = amount;
	}


    private bool IsGrounded()
    {
        return (collisionFlag & CollisionFlags.CollidedBelow) != 0;
    }
}
