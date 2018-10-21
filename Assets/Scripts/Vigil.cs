using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vigil : Enemy
{
	public float shootCooldownDuration;
	public float speed = 8f;
	float cooldownTimer = 0f;

	void FixedUpdate()
	{
		if (cooldownTimer > 0f) {
			cooldownTimer -= Time.fixedDeltaTime;
		}
		transform.LookAt(player);
		if (seePlayer) {
			if (cooldownTimer <= 0f) {
				UpdateSeePlayer();
				if (seePlayer) {
					GameObject p = GameObject.Instantiate(Resources.Load("EnemyProjectile"), transform.position, transform.rotation) as GameObject;
					p.transform.LookAt(player);
					cooldownTimer = shootCooldownDuration;
				}
			}
		}
		if (!seePlayer) {
			rigid.MovePosition(transform.position + (player.position - transform.position).normalized * speed * Time.fixedDeltaTime);
			UpdateSeePlayer();
		}
	}
}
