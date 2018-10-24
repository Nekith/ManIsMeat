using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Director : MonoBehaviour
{
	public Transform[] spawnPoints;
	public GameObject titleScreen;
	public AudioClip spawnSound;
	public AudioClip gameOverSound;
	public GameObject gameOver;
	public GameObject gameOverCamera;
	public GameObject killLabel;
	public GameObject killCount;
	AudioSource audioPlayer;
	int wave;
    int score;
	float timer;
	int evil;
	bool stopped;
	Transform player;
	List<Transform> used;

	void Start()
	{
		wave = 1;
		score = 0;
		timer = 0f;
		evil = 0;
		stopped = false;
		player = GameObject.Find("Player").transform;
		used = new List<Transform>();
		audioPlayer = GetComponent<AudioSource>();
	}

	void Update()
    {
		if (!stopped) {
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
    }

	void SpawnWave()
	{
		int p = 30 + wave * 15 + Random.Range(0, 30);
		used.Clear();
        while (p >= 20) {
			Transform s = GetRandomFreeSpawnPoint();
			int r = Random.Range(0, (wave >= 2 ? 2 : 1));
			Enemy enemy = null;
			if (r == 0) {
				enemy = (GameObject.Instantiate(Resources.Load("Vigil"), s.position + new Vector3(0, 1.2f, 0), Quaternion.identity) as GameObject).GetComponent<Enemy>();
			} else {
				enemy = (GameObject.Instantiate(Resources.Load("Mammoth"), s.position + new Vector3(0, 1.2f, 0), Quaternion.identity) as GameObject).GetComponent<Enemy>();
			}
			enemy.SetLevel(Mathf.FloorToInt(wave / 2));
            ++evil;
            p -= 20;
        }
		killLabel.SetActive(true);
		killCount.SetActive(true);
		killCount.GetComponent<Text>().text = evil.ToString();
		audioPlayer.Stop();
		audioPlayer.clip = spawnSound;
		audioPlayer.Play();
	}

	public void EnemyDeath()
	{
		if (evil >= 1) {
			--evil;
			if (evil == 0) {
				++wave;
				timer = 2f;
				killLabel.SetActive(false);
				killCount.SetActive(false);
			} else {
				killCount.GetComponent<Text>().text = evil.ToString();
			}
		}
	}

	public IEnumerator PlayerDeath()
	{
		stopped = true;
		audioPlayer.Stop();
		audioPlayer.clip = gameOverSound;
		audioPlayer.Play();
		yield return new WaitForSeconds(0.7f);
		gameOver.SetActive(true);
		Destroy(player.gameObject);
		gameOverCamera.SetActive(true);
		GameObject[] list = GameObject.FindGameObjectsWithTag("Enemy");
		foreach (GameObject l in list) {
			Destroy(l);
		}
		list = GameObject.FindGameObjectsWithTag("Projectile");
		foreach (GameObject l in list) {
			Destroy(l);
		}
	}

	public void Restart()
	{
		killLabel.SetActive(false);
		killCount.SetActive(false);
		gameOver.SetActive(false);
		gameOverCamera.SetActive(false);
		wave = 1;
		score = 0;
		timer = 0f;
		evil = 0;
		stopped = false;
		player = (GameObject.Instantiate(Resources.Load("Player")) as GameObject).transform;
		player.name = "Player";
		titleScreen.SetActive(true);
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
