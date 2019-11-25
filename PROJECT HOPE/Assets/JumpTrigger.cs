using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpTrigger : MonoBehaviour {

	protected GameObject[] enemy;
	protected Collider2D col;
	public bool trigger = false;
	private Rigidbody2D rb2d;
	private ContactFilter2D EnemyFilter;
	//private EnemyScript enemyMovement;


	// Update is called once per frame
	void Start () {
		col = GetComponent<Collider2D> ();
		enemy = GameObject.FindGameObjectsWithTag ("Enemy");
	}

	void Update(){
		for (int i = 0; i < enemy.Length; ++i) {
			if (enemy[i] != null) {
				if (enemy[i].GetComponent<EnemyScript> ().triggerReset)
					trigger = false;
			}
		}
	}

	void OnTriggerEnter2D(Collider2D enemyCol)
	{
		if(enemyCol.gameObject.tag == "Enemy")
			trigger = true;	
		//GetComponent<EnemyScript>().enabled = true;	
	}

	public bool Trigger()
	{
		return trigger;
	}

}
