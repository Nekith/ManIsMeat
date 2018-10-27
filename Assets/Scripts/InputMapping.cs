using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputMapping : MonoBehaviour
{
	public enum Mapping
	{
		Qwerty,
		Colemak,
		Azerty,
	}

	public bool aimMode = true;
	public Mapping mapping;
	public Text mappingLabel;
	public Text aimLabel;
	Director director;

	void Start()
	{
		director = GameObject.Find("Director").GetComponent<Director>();
	}

	void Update()
	{
		if (!director.HasStarted()) {
			if (Input.GetKeyDown(KeyCode.Tab)) {
				if (mapping == Mapping.Azerty) {
					mapping = Mapping.Qwerty;
				} else if (mapping == Mapping.Colemak) {
					mapping = Mapping.Azerty;
				} else {
					mapping = Mapping.Colemak;
				}
				mappingLabel.text = mapping.ToString();
			}
			if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift)) {
				aimMode = !aimMode;
				aimLabel.text = (aimMode ? "modern" : "classic");
			}
		}
	}

	public float GetHorizontal()
	{
		if (mapping == Mapping.Azerty) {
			return (Input.GetKey(KeyCode.Q) ? -1 : 0) + (Input.GetKey(KeyCode.D) ? 1 : 0);
		} else if (mapping == Mapping.Colemak) {
			return (Input.GetKey(KeyCode.A) ? -1 : 0) + (Input.GetKey(KeyCode.S) ? 1 : 0);
		}
		return (Input.GetKey(KeyCode.A) ? -1 : 0) + (Input.GetKey(KeyCode.D) ? 1 : 0);
	}

	public float GetVertical()
	{
		if (mapping == Mapping.Azerty) {
			return (Input.GetKey(KeyCode.Z) ? 1 : 0) + (Input.GetKey(KeyCode.S) ? -1 : 0);
		} else if (mapping == Mapping.Colemak) {
			return (Input.GetKey(KeyCode.W) ? 1 : 0) + (Input.GetKey(KeyCode.R) ? -1 : 0);
		}
		return (Input.GetKey(KeyCode.W) ? 1 : 0) + (Input.GetKey(KeyCode.S) ? -1 : 0);
	}
}
