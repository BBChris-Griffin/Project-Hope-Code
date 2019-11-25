using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class endLevelAnim : MonoBehaviour {

	private Animator animator;
	private bool trigger = false;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void OnTriggerEnter2D (Collider2D other) {
		if (other.gameObject.tag == "Player") {
			trigger = true;
			animator.SetBool ("EndGame", trigger);
		}
	}
}
