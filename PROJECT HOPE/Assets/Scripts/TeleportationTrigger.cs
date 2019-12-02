using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportationTrigger : MonoBehaviour {

	public Vector2 telePos;
	public bool disappear = true;
	protected GameObject enemy;
	protected Collider2D enemyCol;
	protected Collider2D col;
	private bool destroy = false;
	private Rigidbody2D rb2d;
	private ContactFilter2D EnemyFilter;

	// Update is called once per frame
	void Start () {
		col = GetComponent<Collider2D> ();
		enemy = GameObject.FindGameObjectWithTag ("Enemy").gameObject;
		enemyCol = enemy.GetComponent<Collider2D> ();
	}

	void OnTriggerExit2D(Collider2D enemyCol){
		destroy = true;
	}
}
