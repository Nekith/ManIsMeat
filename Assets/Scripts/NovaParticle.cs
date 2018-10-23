using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NovaParticle : MonoBehaviour
{
	public float minRadius = 0.1f;
	public float maxRadius = 1.0f;
	public float duration;
	ParticleSystem.ShapeModule shape;
	float timer;
	float particleDuration;

	void Start()
	{
		shape = GetComponent<ParticleSystem>().shape;
		timer = 0;
		particleDuration = GetComponent<ParticleSystem>().main.duration;
		if (duration == 0) {
			duration = particleDuration;
		}
	}

	void Update()
	{
		timer += Time.deltaTime;
		if (timer <= particleDuration) {
			shape.radius = minRadius + (maxRadius - minRadius) * (timer / particleDuration);
		}
		if (timer >= duration) {
			Destroy(gameObject);
		}
	}
}
