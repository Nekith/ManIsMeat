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
	Transform eyes;

	void Start()
	{
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
		rigid = GetComponent<Rigidbody>();
		eyes = transform.Find("MainCamera");
	}

	void Update()
	{
		transform.Rotate(Vector3.up * Input.GetAxisRaw("Mouse X") * sensitivity);
		verticalRotation += Input.GetAxisRaw("Mouse Y") * sensitivity;
		verticalRotation = Mathf.Clamp(verticalRotation, -60, 60);
		eyes.localEulerAngles = Vector3.left * verticalRotation;
		float h = Input.GetAxisRaw("Horizontal") * sensitivity;
		float v = Input.GetAxisRaw("Vertical") * sensitivity;
		if (h != 0 || v != 0) {
			velocity += (transform.right * h + transform.forward * v).normalized * acceleration;
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
