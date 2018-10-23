using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mammoth : Enemy
{
	public float moveDuration;
	public float shootCooldownDuration;
	public float stalkCooldownDuration;
	public float durationMod;
	public float shootWaitDuration;
	public float speed = 6f;
	float cooldownTimer = 0f;
	bool isMoving = false;
	Vector3 direction;

	void FixedUpdate()
	{
		transform.LookAt(player);
		if (isMoving) {
			rigid.MovePosition(transform.position + direction * speed * Time.fixedDeltaTime);
			cooldownTimer -= Time.fixedDeltaTime;
			if (cooldownTimer <= 0) {
				isMoving = false;
				UpdateSeePlayer();
			}
		}
		if (!isMoving) {
			if (cooldownTimer <= 0) {
				if (seePlayer) {
					StartCoroutine(Shoot());
					cooldownTimer = shootCooldownDuration + Random.Range(-durationMod, durationMod);
				} else {
					cooldownTimer = stalkCooldownDuration + Random.Range(-durationMod, durationMod);
				}
			} else {
				cooldownTimer -= Time.fixedDeltaTime;
				if (cooldownTimer <= 0) {
					isMoving = true;
					cooldownTimer = moveDuration + Random.Range(-durationMod, durationMod);
					direction = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized;
				}
			}
		}
	}

	IEnumerator Shoot()
	{
		GameObject p = GameObject.Instantiate(Resources.Load("EnemyProjectile"), transform.position, transform.rotation) as GameObject;
		p.transform.LookAt(player);
		audioPlayer.Stop();
		audioPlayer.clip = shootSound;
		audioPlayer.Play();
		yield return new WaitForSeconds(shootWaitDuration);
		p = GameObject.Instantiate(Resources.Load("EnemyProjectile"), transform.position, transform.rotation) as GameObject;
		p.transform.LookAt(player);
		audioPlayer.Stop();
		audioPlayer.clip = shootSound;
		audioPlayer.Play();
		yield return new WaitForSeconds(shootWaitDuration);
		p = GameObject.Instantiate(Resources.Load("EnemyProjectile"), transform.position, transform.rotation) as GameObject;
		p.transform.LookAt(player);
		audioPlayer.Stop();
		audioPlayer.clip = shootSound;
		audioPlayer.Play();
	}
}
