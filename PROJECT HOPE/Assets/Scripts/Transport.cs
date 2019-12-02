using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transport : BoxPhysics {

	public float transportSpeed = 7f;
	public float Range = 0.2f;
	public float deltaReturnY = 0;
	public bool risingPlatform = false;

	private bool playerContact = false;
	public bool contact = false;
	private Vector3 startPos;

	protected GameObject player;


	// Use this for initialization
	void Awake () 
	{
		player = GameObject.FindGameObjectWithTag ("Player").gameObject;
		startPos = this.gameObject.transform.position;
	}

	protected override void ComputeVelocity()
	{
		Vector2 movement = Vector2.zero; 

		float deltaDistance = player.gameObject.transform.position.x - this.gameObject.transform.position.x;
		float heightDistance = player.gameObject.transform.position.y - this.gameObject.transform.position.y;
		contact = this.GetComponent<Collider2D>().IsTouching(player.gameObject.GetComponent<Collider2D>());

		if (contact && (deltaDistance < Range && deltaDistance > -Range) && heightDistance > 0) 
		{
			velocity.y = transportSpeed;
		} 
		/*else if (!contact && this.gameObject.transform.position.y != startPos.y && heightDistance < deltaReturnY) 
		{
			movement = new Vector3 (0, -1);
			targetVelocity = movement * transportSpeed;
		}*/

		if (!contact)
			risingPlatform = false;
	}


}
