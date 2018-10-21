using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCore : MonoBehaviour
{
	public int health;
	public float comboInterval;
	[Header("UI")]
	public GameObject[] hearts;
	public GameObject gougiPanel;
	[HideInInspector]
	public int gougiHelpDisplayed = 0;
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

	void Update()
	{
		gougiComboTimer += Time.deltaTime;
	}

	void UpdateUI()
	{
		hearts[0].SetActive(health >= 5);
		hearts[1].SetActive(health >= 4);
		hearts[2].SetActive(health >= 3);
		hearts[3].SetActive(health >= 2);
		hearts[4].SetActive(health >= 1);
	}

	public void TakeHit()
	{
		health--;
		UpdateUI();
	}

	public void HitAnEnemy(Vector3 enemy)
	{
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
						AddGougi("the Hunter: killed 4 enemies in chain, rate of fire increased", "rts");
					}
				} else if (gougiComboCount >= 3) {
					gougiAttackSpeedTwo = true;
					GetComponent<PlayerShoot>().shootCooldownDuration -= 0.06f;
					AddGougi("the Hunter: killed 3 enemies in chain, rate of fire increased", "rts");
				}
			} else if (gougiComboCount >= 2) {
				gougiAttackSpeedOne = true;
				GetComponent<PlayerShoot>().shootCooldownDuration -= 0.06f;
				AddGougi("the Hunter: killed 2 enemies in chain, rate of fire increased", "rts");
			}
		}
		gougiHitCount++;
		if (gougiSizeOne) {
			if (gougiSizeTwo) {
				if (!gougiSizeThree && gougiHitCount == 45) {
					gougiSizeThree = true;
					GetComponent<PlayerShoot>().projectileSize += 0.04f;
					AddGougi("the Tyrant: killed 45 enemies, size of projectile increased", "rts");
				}
			} else if (gougiHitCount == 25) {
				gougiSizeTwo = true;
				GetComponent<PlayerShoot>().projectileSize += 0.04f;
				AddGougi("the Tyrant: killed 25 enemies, size of projectile increased", "rts");
			}
		} else if (gougiHitCount == 10) {
			gougiSizeOne = true;
			GetComponent<PlayerShoot>().projectileSize += 0.04f;
			AddGougi("the Tyrant: killed 10 enemies, size of projectile increased", "rts");
		}
	}

	void AddGougi(string text, string icon)
	{
		GameObject gougiIcon = GameObject.Instantiate(Resources.Load("GougiIcon")) as GameObject;
		gougiIcon.transform.SetParent(gougiPanel.transform);
		gougiIcon.transform.localPosition = new Vector2(2 + gougiCount * 42, -2);
		GameObject gougiHelp = GameObject.Instantiate(Resources.Load("GougiHelp")) as GameObject;
		gougiHelp.name = "GougiHelp";
		gougiHelp.GetComponent<Text>().text = text;
		gougiHelp.transform.SetParent(gougiPanel.transform);
		++gougiHelpDisplayed;
		gougiHelp.transform.localPosition = new Vector2(2 + gougiCount * 42, -42 - gougiHelpDisplayed * 30);
		++gougiCount;
	}
}
