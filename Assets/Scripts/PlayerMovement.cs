﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public float sensitivity = 1.0f;
	public float maxSpeed = 10.0f;
	public float maxHoverSpeed = 17.0f;
	public float acceleration = 4.0f;
	public float deceleration = 1.0f;
	public float jumpForce = 100.0f;
	public float hoverFov = 80.0f;
	float basicFov;
	Vector3 velocity;
	float verticalRotation;
	bool hovering;
	Rigidbody rigid;
	CapsuleCollider capsule;
	Transform eyes;
	Camera eyesCamera;
	bool canJump;
	bool wantToJump;
	bool jumping;
	Collider ground;

	void Start()
	{
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
		rigid = GetComponent<Rigidbody>();
		eyes = transform.Find("MainCamera");
		eyesCamera = eyes.GetComponent<Camera>();
		basicFov = eyesCamera.fieldOfView;
		canJump = false;
		jumping = false;
	}

	void Update()
	{
		transform.Rotate(Vector3.up * Input.GetAxisRaw("Mouse X") * sensitivity);
		verticalRotation += Input.GetAxisRaw("Mouse Y") * sensitivity;
		verticalRotation = Mathf.Clamp(verticalRotation, -60, 60);
		eyes.localEulerAngles = Vector3.left * verticalRotation;
		if (!jumping) {
			float h = Input.GetAxisRaw("Horizontal") * sensitivity;
			float v = Input.GetAxisRaw("Vertical") * sensitivity;
			hovering = Input.GetButton("Hovering");
			if (h != 0 || v != 0) {
				velocity += (transform.right * h + transform.forward * v).normalized * acceleration;
			} else {
				velocity -= velocity.normalized * deceleration;
			}
			velocity = Vector3.ClampMagnitude(velocity, hovering ? maxHoverSpeed : maxSpeed);
		}
		if (wantToJump && !jumping) {
			if (!Input.GetButton("Jump")) {
				wantToJump = false;
			}
		} else {
			wantToJump = Input.GetButton("Jump");
		}
		if (hovering) {
			eyesCamera.fieldOfView = Mathf.Lerp(eyesCamera.fieldOfView, hoverFov, Time.deltaTime * 10);
		} else {
			eyesCamera.fieldOfView = Mathf.Lerp(eyesCamera.fieldOfView, basicFov, Time.deltaTime * 10);
		}
	}

	void FixedUpdate()
	{
		rigid.MovePosition(rigid.position + velocity * Time.fixedDeltaTime);
		if (wantToJump && canJump) {
			rigid.AddForce(new Vector3(0, jumpForce, 0));
			canJump = false;
			wantToJump = false;
			jumping = true;
		}
	}

	void OnCollisionEnter(Collision collision)
	{
		foreach (ContactPoint contact in collision.contacts) {
			if (contact.normal.y > 0) {
				canJump = true;
				jumping = false;
				ground = collision.collider;
				return;
			}
		}
	}

	void OnCollisionExit(Collision collision)
	{
		if (ground == collision.collider) {
			canJump = false;
		}
	}
}
