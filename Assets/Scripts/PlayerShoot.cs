using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
	public float shootCooldownDuration;
	public float projectileSize;
	public float shootAnimDuration;
	public Vector3 cooldownGunPosition;
	public AudioClip shootSound;
	AudioSource audioPlayer;
	Transform gun;
	Vector3 baseGunPosition;
	Color baseGunColor;
	Color cooldownGunColor;
	Color currentColor;
	float cooldownTimer;

	void Start()
	{
		audioPlayer = GetComponent<AudioSource>();
		gun = transform.Find("MainCamera").Find("Gun");
		baseGunPosition = gun.localPosition;
		baseGunColor = gun.GetComponent<MeshRenderer>().material.GetColor("_EmissionColor");
		cooldownGunColor = Color.white;
		cooldownTimer = 0f;
	}

	void Update()
	{
		if (cooldownTimer <= 0f) {
			if (Input.GetButton("Fire1")) {
				cooldownTimer = shootCooldownDuration;
				currentColor = cooldownGunColor;
				GameObject p = GameObject.Instantiate(Resources.Load("FriendlyProjectile"), gun.position, gun.rotation) as GameObject;
				p.transform.localScale = new Vector3(projectileSize, projectileSize, projectileSize);
				audioPlayer.Stop();
				audioPlayer.clip = shootSound;
				audioPlayer.Play();
			}
		} else {
			cooldownTimer -= Time.deltaTime;
		}
		if (cooldownTimer >= shootCooldownDuration - shootAnimDuration) {
			float p = (shootCooldownDuration - cooldownTimer) / shootAnimDuration;
			gun.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", cooldownGunColor);
			gun.GetComponent<MeshRenderer>().material.SetColor("_Color", cooldownGunColor);
			gun.localPosition = baseGunPosition + (cooldownGunPosition - baseGunPosition) * p;
		} else if (cooldownTimer > 0f) {
			float p = (shootCooldownDuration - cooldownTimer - shootAnimDuration) / (shootCooldownDuration - shootAnimDuration);
			p = 1 - p;
			currentColor.r = baseGunColor.r + (cooldownGunColor.r - baseGunColor.r) * p;
			currentColor.g = baseGunColor.g + (cooldownGunColor.g - baseGunColor.g) * p;
			currentColor.b = baseGunColor.b + (cooldownGunColor.b - baseGunColor.b) * p;
			gun.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", currentColor);
			gun.GetComponent<MeshRenderer>().material.SetColor("_Color", currentColor);
			gun.localPosition = baseGunPosition + (cooldownGunPosition - baseGunPosition) * p;
		}
	}
}
