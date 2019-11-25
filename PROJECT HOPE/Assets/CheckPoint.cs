using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour {

	private GameObject player;
	private Animator animator;
	private bool check;

	// Use this for initialization
	void Start () {
		//player = GameObject.FindGameObjectWithTag ("Player").gameObject;
		animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		animator.SetBool("checkPoint", check);
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Player") {
			check = true;
		}
	}
}
