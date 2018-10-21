using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCore : MonoBehaviour
{
	public int health;
	[Header("UI")]
	public GameObject[] hearts;

	public void TakeHit()
	{
		health--;
		UpdateUI();
	}

	void UpdateUI()
	{
		hearts[0].SetActive(health >= 5);
		hearts[1].SetActive(health >= 4);
		hearts[2].SetActive(health >= 3);
		hearts[3].SetActive(health >= 2);
		hearts[4].SetActive(health >= 1);
	}
}
