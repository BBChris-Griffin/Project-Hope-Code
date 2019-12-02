using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreEnemyScript : MonoBehaviour {

	//public Transform MainEnemy;
	private bool ignore = true;
	private Collider2D col;
	private Collider2D enemy;

	// Use this for initialization
	void Awake(){
		enemy = GetComponent<Collider2D> ();
	}

	void Start () {
		if (col.gameObject.tag == "Enemy") {
			Physics2D.IgnoreCollision(col, enemy, ignore);
		}
	}


}
