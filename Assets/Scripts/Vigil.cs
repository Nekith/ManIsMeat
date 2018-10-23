using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vigil : Enemy
{
	public float shootCooldownDuration;
	public float cooldownLevelMod;
	public float speed = 8f;
	float cooldownTimer = 0f;

	void FixedUpdate()
	{
		if (cooldownTimer > 0f) {
			cooldownTimer -= Time.fixedDeltaTime;
		}
		transform.LookAt(player);
		if (seePlayer) {
			nav.isStopped = true;
			if (cooldownTimer <= 0f) {
				UpdateSeePlayer();
				if (seePlayer) {
					GameObject p = GameObject.Instantiate(Resources.Load("EnemyProjectile"), transform.position, transform.rotation) as GameObject;
					p.transform.LookAt(player);
					cooldownTimer = shootCooldownDuration;
					audioPlayer.Stop();
					audioPlayer.clip = shootSound;
					audioPlayer.Play();
				}
			}
		}
		if (!seePlayer) {
			nav.destination = player.position;
			nav.isStopped = false;
			UpdateSeePlayer();
		}
	}

	override public void SetLevel(int level)
	{
		shootCooldownDuration -= cooldownLevelMod * level;
	}
}
