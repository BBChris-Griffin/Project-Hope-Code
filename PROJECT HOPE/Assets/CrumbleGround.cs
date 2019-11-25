using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrumbleGround : MonoBehaviour {

	public float destructRange = 0.2f;
	protected GameObject player;
	private bool contact = false;
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player").gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		float deltaDistance = player.gameObject.transform.position.x - this.gameObject.transform.position.x;
		float heightDistance = player.gameObject.transform.position.y - this.gameObject.transform.position.y;
		contact = this.GetComponent<Collider2D>().IsTouching(player.gameObject.GetComponent<Collider2D>());

		if (contact && (deltaDistance < destructRange && deltaDistance > -destructRange) && heightDistance > 0)
			Destroy (this.gameObject);
	}

	/*void OnCollisionEnter2D(Collision2D other)
	{
		float deltaDistance = other.gameObject.transform.position.x - this.gameObject.transform.position.x;
		if (other.gameObject.tag == "Player") && (deltaDistance < destructRange && deltaDistance > -destructRange))
			Destroy (this.gameObject);
	}*/
}
