using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncingPlatform : BoxPhysics {
	public float maxSpeed = 3f;
	public float damageAmount = 10f;
	public float boxSlideTime = 30f;
	public Collider2D player;
	public bool floorBreak = false;
	public GameObject gameObject;
	public bool boxKick = false;
	public bool horizontalFloorBreak = false;

	public AudioClip[] sfx;
	private AudioSource audio;

	protected PlayerPlatformerController Penny;
	protected GameObject VGround;

	private float distance = 0;
	private float timer = 0f;
	private bool kick = false;
	protected Collision2D col;
	private SpriteRenderer spriteRenderer;
	private bool check = false;

	// Use this for initialization
	void Awake () {
		spriteRenderer = GetComponent<SpriteRenderer> ();
		audio = GetComponent<AudioSource> ();
		VGround = GameObject.FindGameObjectWithTag ("VDestructibleGround").gameObject;
	}

	// Update is called once per frame
	protected override void ComputeVelocity () {
		Vector2 move = Vector2.zero;
		Vector2 deltaDistance = Player.position - MainEnemy.position;

		bool contact = rb2d.IsTouching (player);
		if (contact)
			timer++;

		if (Input.GetButtonDown ("Kick")) {
			kick = true;
			timer++;
		} else
			kick = false;

		if (grounded && Input.GetButtonUp ("Kick")) 
		{
			floorBreak = false;
		}
}

void OnCollisionEnter2D(Collision2D other)
{

	if (other.gameObject.tag == "SpringGround") {
		velocity.y = 10f;
	}
}


}
