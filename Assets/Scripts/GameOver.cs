using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
	void Update()
	{
		if (Input.GetButtonDown("Jump")) {
			GameObject.Find("Director").GetComponent<Director>().Restart();
		}
	}
}
