using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTrigger : MonoBehaviour {

	protected Collider2D col;
	public bool trigger = false;
	private Rigidbody2D rb2d;
	private ContactFilter2D EnemyFilter;
	
	// Update is called once per frame
	void Start () {
		col = GetComponent<Collider2D> ();
	}

	void OnTriggerEnter2D(Collider2D player)
	{
		if(player.gameObject.tag == "Player")
			trigger = true;	
	}

	public bool Trigger()
	{
		return trigger;
	}

}
