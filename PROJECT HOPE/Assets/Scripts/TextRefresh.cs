using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextRefresh : MonoBehaviour {
	private string message = " ";
	public bool triggered = false;

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Player") {
			triggered = true;
		}
	}
}
