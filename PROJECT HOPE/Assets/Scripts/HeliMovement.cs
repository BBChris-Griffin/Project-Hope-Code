using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeliMovement : BoxPhysics {
	public float speed = 7f;
	public float timer = 0.0f;
	public bool rise = false;
	public float timeLimit = 4.5f; 
	private float heliTimer = 0.0f;
	private GameObject player;
	private GameObject clone;
	private bool destroyed = false;
	private Vector3 startingPos;
	// Use this for initialization
	void Start () 
	{
		timeLimit = 4.5f;
		player = GameObject.FindGameObjectWithTag ("Player").gameObject;
		startingPos = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y, this.gameObject.transform.position.z);
		gravityModifier = 0f;
	}

	protected override void ComputeVelocity()
	{

		if (rise) {
			if (gravityModifier < 0) {
				gravityModifier = -gravityModifier;
			}	

			timer += Time.deltaTime;
		}

		if (!rise) {
			timer = 0.0f;
		}

		if (timer > timeLimit || (((Input.GetAxis("Horizontal") >0.3f || Input.GetAxis("Horizontal") < -0.5f)  
				&& Input.GetButtonDown("Jump")) && timer > 0.25f)) {
			Destroy (this.gameObject);
			destroyed = true;
		}

		heliTimer++;
	}


	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.tag == "Player") {
			rise = true;
			gravityModifier = 1f;
		}
	}

	void OnCollisionExit2D(Collision2D other)
	{
		if (other.gameObject.tag == "Player") {
			rise = false;
		}
	}
}
