using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCore : MonoBehaviour
{
	public int health;
	public float comboInterval;
	public float healthIndicatorDuration;
	public AudioClip gougiSound;
	public AudioClip hitSound;
	GameObject[] hearts;
	GameObject gougiHelp;
	GameObject healthIndicatorT;
	GameObject healthIndicatorB;
	AudioSource audioPlayer;
	int gougiCount = 0;
	bool gougiAttackSpeedOne = false;
	bool gougiAttackSpeedTwo = false;
	bool gougiAttackSpeedThree = false;
	int gougiHitCount = 0;
	int gougiComboCount = 0;
	float gougiComboTimer = 0;
	bool gougiSizeOne = false;
	bool gougiSizeTwo = false;
	bool gougiSizeThree = false;
	int gougiVampireCount = 0;
	float healthIndicatorTimer = 0f;

	void Start()
	{
		audioPlayer = GetComponent<AudioSource>();
		hearts = new GameObject[3];
		int imax = GameObject.Find("Hearts").transform.childCount;
		for (int i = 0; i < imax; i++) {
			hearts[i] = GameObject.Find("Hearts").transform.GetChild(i).gameObject;
		}
		gougiHelp = GameObject.Find("GougiHelp");
		gougiHelp.GetComponent<Text>().text = "";
		healthIndicatorT = GameObject.Find("HealthIndicatorT");
		healthIndicatorT.GetComponent<Image>().enabled = false;
		healthIndicatorB = GameObject.Find("HealthIndicatorB");
		healthIndicatorB.GetComponent<Image>().enabled = false;
		UpdateUI();
	}

	void Update()
	{
		gougiComboTimer += Time.deltaTime;
		if (healthIndicatorTimer > 0f) {
			healthIndicatorTimer -= Time.deltaTime;
			if (healthIndicatorTimer <= 0f) {
				healthIndicatorT.GetComponent<Image>().enabled = false;
				healthIndicatorB.GetComponent<Image>().enabled = false;
			}
		}
	}

	void UpdateUI()
	{
		hearts[0].SetActive(health >= 3);
		hearts[1].SetActive(health >= 2);
		hearts[2].SetActive(health >= 1);
	}

	public void TakeHit()
	{
		if (health >= 1) {
			health--;
			if (health == 0) {
				StartCoroutine(GameObject.Find("Director").GetComponent<Director>().PlayerDeath());
				healthIndicatorT.GetComponent<Image>().enabled = false;
				healthIndicatorB.GetComponent<Image>().enabled = false;
			} else {
				UpdateUI();
				audioPlayer.Stop();
				audioPlayer.clip = hitSound;
				audioPlayer.Play();
				healthIndicatorT.GetComponent<Image>().enabled = true;
				healthIndicatorB.GetComponent<Image>().enabled = true;
				healthIndicatorTimer = healthIndicatorDuration;
			}
		}
	}

	public void HitAnEnemy(Vector3 enemy)
	{
		if (health == 1) {
			++gougiVampireCount;
			if (gougiVampireCount >= 7) {
				gougiVampireCount = 0;
				AddGougi("the Leech: killed 7 enemies at 1 health, gained 1 health");
				UpdateUI();
			}
		}
		if (gougiComboTimer >= comboInterval) {
			gougiComboTimer = 0;
			gougiComboCount = 1;
		} else {
			gougiComboTimer = 0;
			++gougiComboCount;
			if (gougiAttackSpeedOne) {
				if (gougiAttackSpeedTwo) {
					if (!gougiAttackSpeedThree && gougiComboCount >= 4) {
						gougiAttackSpeedThree = true;
						GetComponent<PlayerShoot>().shootCooldownDuration -= 0.06f;
						AddGougi("the Hunter: killed 4 enemies in chain, rate of fire increased");
					}
				} else if (gougiComboCount >= 3) {
					gougiAttackSpeedTwo = true;
					GetComponent<PlayerShoot>().shootCooldownDuration -= 0.06f;
					AddGougi("the Hunter: killed 3 enemies in chain, rate of fire increased");
				}
			} else if (gougiComboCount >= 2) {
				gougiAttackSpeedOne = true;
				GetComponent<PlayerShoot>().shootCooldownDuration -= 0.06f;
				AddGougi("the Hunter: killed 2 enemies in chain, rate of fire increased");
			}
		}
		gougiHitCount++;
		if (gougiSizeOne) {
			if (gougiSizeTwo) {
				if (!gougiSizeThree && gougiHitCount == 45) {
					gougiSizeThree = true;
					GetComponent<PlayerShoot>().projectileSize += 0.04f;
					AddGougi("the Tyrant: killed 45 enemies, size of projectile increased");
				}
			} else if (gougiHitCount == 25) {
				gougiSizeTwo = true;
				GetComponent<PlayerShoot>().projectileSize += 0.04f;
				AddGougi("the Tyrant: killed 25 enemies, size of projectile increased");
			}
		} else if (gougiHitCount == 10) {
			gougiSizeOne = true;
			GetComponent<PlayerShoot>().projectileSize += 0.04f;
			AddGougi("the Tyrant: killed 10 enemies, size of projectile increased");
		}
	}

	void AddGougi(string text)
	{
		gougiHelp.GetComponent<Text>().text = text;
		++gougiCount;
		audioPlayer.Stop();
		audioPlayer.clip = gougiSound;
		audioPlayer.Play();
	}
}
