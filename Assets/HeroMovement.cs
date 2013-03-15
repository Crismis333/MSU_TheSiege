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

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        Vector3 moveDirection = new Vector3();

        float v = Input.GetAxisRaw("Vertical");
        float h = Input.GetAxisRaw("Horizontal");

        if (v > 0.1)
            moveDirection.z = MoveSpeed + SpeedUp * v;
        else if (v < -0.1)
            moveDirection.z = MoveSpeed + SpeedDown * v;
        else
            moveDirection.z = MoveSpeed;
        
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


    private bool IsGrounded()
    {
        return (collisionFlag & CollisionFlags.CollidedBelow) != 0;
    }
}
