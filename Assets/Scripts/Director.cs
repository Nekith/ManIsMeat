using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Director : MonoBehaviour
{
	public Transform[] spawnPoints;
	public GameObject titleScreen;
	int wave;
    int score;
	float timer;
	int evil;
	Transform player;
	List<Transform> used;

	void Start()
	{
		wave = 1;
		score = 0;
		timer = 0f;
		evil = 0;
		player = GameObject.Find("Player").transform;
		used = new List<Transform>();
	}

	void Update()
    {
        if (timer > 0f) {
            timer -= Time.deltaTime;
            if (timer <= 0f) {
				SpawnWave();
            }
        }
		if (timer == 0 && evil == 0 && score == 0 && Input.GetButtonDown("Jump")) {
			SpawnWave();
			titleScreen.SetActive(false);
		}
    }

	void SpawnWave()
	{
		int p = 30 + wave * 15 + Random.Range(0, 30);
		used.Clear();
        while (p >= 20) {
			Transform s = GetRandomFreeSpawnPoint();
			Instantiate(Resources.Load("Vigil"), s.position + new Vector3(0, 1, 0), Quaternion.identity);
            ++evil;
            p -= 20;
        }
	}

	public void EnemyDeath()
	{
		if (evil >= 1) {
			--evil;
			if (evil == 0) {
				++wave;
				timer = 2f;
			}
		}
	}

	Transform GetRandomFreeSpawnPoint()
	{
		int count = 0;
		while (count < 10) {
			Transform c = spawnPoints[Random.Range(0, spawnPoints.Length)];
			if (used.Contains(c)) {
				continue;
			}
			RaycastHit hit;
			Ray ray = new Ray(c.position, player.position - c.position);
			if (Physics.Raycast(ray, out hit)) {
				if (hit.collider.gameObject.tag != "Player") {
					used.Add(c);
					return c;
				}
			}
			++count;
		}
		Transform d = spawnPoints[Random.Range(0, spawnPoints.Length)];
		used.Add(d);
		return d;
	}
}
