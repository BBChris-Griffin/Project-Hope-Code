using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextTrigger : MonoBehaviour {
	public string message = " ";
	public bool triggered = false;
	private Animator animator;

	void Start()
	{
		if(this.gameObject.GetComponent<Animator>() != null)
			animator = GetComponent<Animator> ();	
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Player") {
			triggered = true;
			if( animator != null)
				animator.SetBool ("showTip", triggered);
		}
	}
}
