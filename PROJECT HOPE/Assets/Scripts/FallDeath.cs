using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallDeath : MonoBehaviour {

	public bool fallen = false;

	void OnTriggerEnter2D(Collider2D player)
	{
		if (player.gameObject.tag == "Player")
			fallen = true;
	}
}
