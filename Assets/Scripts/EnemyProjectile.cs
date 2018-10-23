using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
	public float speed = 5.0f;
	Rigidbody rigid;

	void Start()
	{
		rigid = GetComponent<Rigidbody>();
	}

	void FixedUpdate()
	{
		rigid.MovePosition(transform.position + transform.forward * Time.fixedDeltaTime * speed);
		if (transform.position.magnitude >= 300) {
			Destroy(gameObject);
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag != "Enemy" && other.tag != "Gravity" && other.tag != "Projectile") {
			if (other.tag == "Player") {
				other.GetComponent<PlayerCore>().TakeHit();
			}
			Vector3 p = transform.position + (GameObject.Find("Player").transform.position - transform.position).normalized;
			GameObject.Instantiate(Resources.Load("EnemyProjectileExplosion"), p, transform.rotation);
			Destroy(gameObject);
		}
	}
}
