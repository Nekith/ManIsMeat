using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class Enemy : MonoBehaviour
{
	public int health = 1;
	public AudioClip shootSound;
	protected Transform player;
	protected bool seePlayer;
	protected Rigidbody rigid;
	protected NavMeshAgent nav;
	protected AudioSource audioPlayer;
	bool dying;
	float dyingTimer;

	void Start()
	{
		dying = false;
		player = GameObject.Find("Player").transform;
		seePlayer = false;
		rigid = GetComponent<Rigidbody>();
		nav = GetComponent<NavMeshAgent>();
		audioPlayer = GetComponent<AudioSource>();
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
				GameObject.Instantiate(Resources.Load("EnemyExplosion"), new Vector3(transform.position.x, 1.5f, transform.position.z), transform.rotation);
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

	abstract public void SetLevel(int level);
}
