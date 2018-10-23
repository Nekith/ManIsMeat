using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public float sensitivity = 1.0f;
	public float maxSpeed = 10.0f;
	public float acceleration = 4.0f;
	public float deceleration = 1.0f;
	Vector3 velocity;
	float verticalRotation;
	Rigidbody rigid;
	CapsuleCollider capsule;

	void Start()
	{
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
		rigid = GetComponent<Rigidbody>();
	}

	void Update()
	{
		transform.Rotate(Vector3.up * Input.GetAxisRaw("Mouse X") * sensitivity);
		float h = Input.GetAxisRaw("Horizontal");
		float v = Input.GetAxisRaw("Vertical");
		if (h != 0 || v != 0) {
			velocity += (transform.right * v - transform.forward * h).normalized * acceleration;
		} else {
			velocity -= velocity.normalized * deceleration;
		}
		velocity = Vector3.ClampMagnitude(velocity, maxSpeed);
	}

	void FixedUpdate()
	{
		rigid.MovePosition(rigid.position + velocity * Time.fixedDeltaTime);
	}
}
