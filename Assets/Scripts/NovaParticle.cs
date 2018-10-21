using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NovaParticle : MonoBehaviour
{
	public float minRadius = 0.1f;
	public float maxRadius = 1.0f;
	ParticleSystem.ShapeModule shape;
	float timer;
	float duration;

	void Start()
	{
		shape = GetComponent<ParticleSystem>().shape;
		timer = 0;
		duration = GetComponent<ParticleSystem>().main.duration;
	}

	void Update()
	{
		timer += Time.deltaTime;
		shape.radius = minRadius + (maxRadius - minRadius) * (timer / duration);
		if (timer >= duration) {
			Destroy(gameObject);
		}
	}
}
