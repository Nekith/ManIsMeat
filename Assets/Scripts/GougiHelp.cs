using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GougiHelp : MonoBehaviour
{
	float timer = 0f;
	
	void Update()
	{
		timer += Time.deltaTime;
		if (timer >= 8.0f) {
			Destroy(gameObject);
		}
	}
}
