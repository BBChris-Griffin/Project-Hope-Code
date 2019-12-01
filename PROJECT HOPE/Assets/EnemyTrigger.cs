using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTrigger : MonoBehaviour {

	//public Collider2D player;
	protected Collider2D col;
	public bool trigger = false;
	private Rigidbody2D rb2d;
	private ContactFilter2D EnemyFilter;
	//private EnemyScript enemyMovement;

	
	// Update is called once per frame
	void Start () {
		col = GetComponent<Collider2D> ();
		/*if (trigger == false) {
			GetComponent<EnemyScript>().enabled = false;
		}*/
	}

	void OnTriggerEnter2D(Collider2D player)
	{
		if(player.gameObject.tag == "Player")
			trigger = true;	
		//GetComponent<EnemyScript>().enabled = true;	
	}

	public bool Trigger()
	{
		return trigger;
	}

}
