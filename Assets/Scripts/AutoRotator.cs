using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoRotator : MonoBehaviour
{
	public Vector3 angles;

	public void Update()
	{
		transform.Rotate(angles * Time.deltaTime, Space.Self);
	}
}
