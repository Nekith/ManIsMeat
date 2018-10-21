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
	int gougiCount = 0;
	bool gougiAttackSpeedOne = false;
	bool gougiAttackSpeedTwo = false;
	bool gougiAttackSpeedThree = false;
	int gougiHitCount = 0;
	int gougiComboCount = 0;
	float gougiComboTimer = 0;

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
			gougiComboCount = 0;
		} else {
			gougiComboTimer = 0;
			++gougiComboCount;
		}
		gougiHitCount++;
		if (gougiAttackSpeedOne) {
			if (gougiAttackSpeedTwo) {
				if (!gougiAttackSpeedThree && gougiHitCount == 45) {
					gougiAttackSpeedThree = true;
					GetComponent<PlayerShoot>().shootCooldownDuration -= 0.06f;
					AddGougi("the Hunter: killed 10 enemies, rate of fire increased", "rts");
				}
			} else if (gougiHitCount == 25) {
				gougiAttackSpeedTwo = true;
				GetComponent<PlayerShoot>().shootCooldownDuration -= 0.06f;
				AddGougi("the Hunter: killed 10 enemies, rate of fire increased", "rts");
			}
		} else if (gougiHitCount == 10) {
			gougiAttackSpeedOne = true;
			GetComponent<PlayerShoot>().shootCooldownDuration -= 0.06f;
			AddGougi("the Hunter: killed 10 enemies, rate of fire increased", "rts");
		}
	}

	void AddGougi(string text, string icon)
	{
		GameObject gougiIcon = GameObject.Instantiate(Resources.Load("GougiIcon")) as GameObject;
		gougiIcon.transform.SetParent(gougiPanel.transform);
		gougiIcon.transform.localPosition = new Vector2(2 + gougiCount * 42, -2);
		GameObject gougiHelp = GameObject.Instantiate(Resources.Load("GougiHelp")) as GameObject;
		gougiHelp.GetComponent<Text>().text = text;
		gougiHelp.transform.SetParent(gougiPanel.transform);
		gougiHelp.transform.localPosition = new Vector2(2 + gougiCount * 42, -44);
		++gougiCount;
	}
}
