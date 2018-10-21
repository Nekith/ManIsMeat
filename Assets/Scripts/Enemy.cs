using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	public int health = 1;
	protected Transform player;
	protected bool seePlayer;
	protected Rigidbody rigid;
	bool dying;
	float dyingTimer;

	void Start()
	{
		dying = false;
		player = GameObject.Find("Player").transform;
		seePlayer = false;
		rigid = GetComponent<Rigidbody>();
	}

	void Update()
	{
		if (dying) {
			dyingTimer -= Time.deltaTime;
			if (dyingTimer <= 0f) {
				Destroy(gameObject);
			}
		}
	}

	public void TakeHit(int damage)
	{
		if (!dying) {
			health -= damage;
			if (health <= 0) {
				dying = true;
				dyingTimer = 0.15f;
				GameObject.Find("Director").GetComponent<Director>().EnemyDeath();
				GameObject.Instantiate(Resources.Load("EnemyExplosion"), transform.position, transform.rotation);
			}
		}
	}

	protected void UpdateSeePlayer()
	{
		seePlayer = false;
		RaycastHit hit;
		Ray ray = new Ray(transform.position, player.position - transform.position);
		if (Physics.Raycast(ray, out hit)) {
			if (hit.collider.gameObject.tag == "Player") {
				seePlayer = true;
			}
		}
	}
}
