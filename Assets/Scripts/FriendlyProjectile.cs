using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendlyProjectile : MonoBehaviour
{
	public float speed = 5.0f;
	[HideInInspector]
	public int damage = 1;
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
		if (other.tag != "Player" && other.tag != "Gravity" && other.tag != "Projectile") {
			if (other.tag == "Enemy") {
				other.GetComponent<Enemy>().TakeHit(damage);
				GameObject.Find("Player").GetComponent<PlayerCore>().HitAnEnemy(transform.position);
			}
			Vector3 p = transform.position + (GameObject.Find("Player").transform.position - transform.position).normalized;
			GameObject.Instantiate(Resources.Load("FriendlyProjectileExplosion"), p, transform.rotation);
			Destroy(gameObject);
		}
	}
}
