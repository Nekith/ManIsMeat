using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public float mouseSensitivity = 1.0f;
	public float rotateSensitivity = 1.0f;
	public float maxSpeed = 10.0f;
	public float acceleration = 4.0f;
	public float deceleration = 1.0f;
	Vector3 velocity;
	float verticalRotation;
	Rigidbody rigid;
	CapsuleCollider capsule;
	InputMapping inputMapping;

	void Start()
	{
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
		rigid = GetComponent<Rigidbody>();
		inputMapping = GameObject.Find("Input").GetComponent<InputMapping>();
	}

	void Update()
	{
		float h = inputMapping.GetHorizontal();
		float v = inputMapping.GetVertical();
		if (inputMapping.aimMode) {
			transform.Rotate(Vector3.up * Input.GetAxisRaw("Mouse X") * mouseSensitivity);
			if (h != 0 || v != 0) {
				velocity += (transform.right * v - transform.forward * h).normalized * acceleration;
			} else {
				velocity -= velocity.normalized * deceleration;
			}
			velocity = Vector3.ClampMagnitude(velocity, maxSpeed);
		} else {
			transform.Rotate(Vector3.up * h * rotateSensitivity);
			if (v != 0) {
				velocity += (transform.right * v).normalized * acceleration;
			} else {
				velocity -= velocity.normalized * deceleration;
			}
			velocity = Vector3.ClampMagnitude(velocity, maxSpeed);
		}
	}

	void FixedUpdate()
	{
		rigid.MovePosition(rigid.position + velocity * Time.fixedDeltaTime);
	}
}
