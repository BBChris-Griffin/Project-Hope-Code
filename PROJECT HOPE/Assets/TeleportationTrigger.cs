using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportationTrigger : MonoBehaviour {

	public Vector2 telePos;
	//public GameObject thisTrigger;
	public bool disappear = true;
	protected GameObject enemy;
	protected Collider2D enemyCol;
	protected Collider2D col;
	//public bool trigger = false;
	private bool destroy = false;
	private Rigidbody2D rb2d;
	private ContactFilter2D EnemyFilter;
	//private EnemyScript enemyMovement;


	// Update is called once per frame
	void Start () {
		col = GetComponent<Collider2D> ();
		enemy = GameObject.FindGameObjectWithTag ("Enemy").gameObject;
		enemyCol = enemy.GetComponent<Collider2D> ();
	}

	void Update(){
		/*if (disappear) {
			if (destroy)
				Destroy (thisTrigger);
		}*/
	}

	/*void OnTriggerEnter2D(Collider2D enemyCol)
	{
		//trigger = true;	
		//GetComponent<EnemyScript>().enabled = true;	
	}*/

	void OnTriggerExit2D(Collider2D enemyCol){
		destroy = true;
	}

	/*void OnCollisionEnter2D (Collision2D collision) {

		if (collision.gameObject.tag == "Enemy") {
			trigger = true;
		}

	}

	void OnCollisionExit2D (Collision2D collision) {
		if (collision.gameObject.tag == "Enemy") {
			trigger = false;
			destroy = true;
		}
	}*/

	/*public bool Trigger()
	{
		return trigger;
	}*/

}
