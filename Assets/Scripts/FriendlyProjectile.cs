using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendlyProjectile : MonoBehaviour
{
	public float speed = 5.0f;
	Rigidbody rigid;

	void Start()
	{
		rigid = GetComponent<Rigidbody>();
	}

	void FixedUpdate()
	{
		rigid.MovePosition(transform.position - transform.up * Time.fixedDeltaTime * speed);
		if (transform.position.magnitude >= 300) {
			Destroy(gameObject);
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag != "Player") {
			GameObject.Instantiate(Resources.Load("FriendlyProjectileExplosion"), transform.position, transform.rotation);
			Destroy(gameObject);
		}
	}
}
