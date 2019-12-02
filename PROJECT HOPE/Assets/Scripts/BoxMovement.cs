using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxMovement : BoxPhysics {
	public float maxSpeed = 3f;
	public float damageAmount = 10f;
	public float boxSlideTime = 1.0f;
	public Collider2D player;
	public bool floorBreak = false;
	public bool boxKick = false;
	public bool horizontalFloorBreak = false;

	public AudioClip[] sfx;
	private AudioSource audio;

	protected PlayerPlatformerController Penny;
	protected GameObject Ground;

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
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Collider2D> ();
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

		if (Input.GetButtonDown ("Kick") && deltaDistance.x > -0.3f && deltaDistance.x < 0.5f && contact) {
			floorBreak = true;
			PlaySound (0, 1f);
		} else if (kick && contact && !floorBreak || (velocity.y != 0 && deltaDistance.x > -1 && deltaDistance.x < 1 
			 && deltaDistance.y > -1 && deltaDistance.y < 1 && kick)) {
			distance = 1.075f;
			horizontalFloorBreak = true;
			boxKick = true;
			check = true;
			PlaySound (1, 1f);
		}

		if (distance > 0) {
			if (deltaDistance.x < -0.2f)
				move = new Vector2 (1, 0);
			if (deltaDistance.x > 0.2f)
				move = new Vector2 (-1, 0);
			targetVelocity = move * maxSpeed;



			distance -= Time.deltaTime;
		}
		if (distance < 0.01 && check) {
			boxKick = false;
			horizontalFloorBreak = false;
		}
		if (distance == 0)
			check = false;
	}

void PlaySound(int clip, float volume)
{
	audio.clip = sfx[clip];
	audio.volume = volume;
	audio.Play ();
}

	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.tag == "Enemy" && boxKick ) {
			PlaySound (2, 1f);
		} else if (other.gameObject.tag == "VDestructibleGround" && horizontalFloorBreak) {
			PlaySound (0, 1f);
		} else if (other.gameObject.tag == "SpringGround") {
			PlaySound (3, 0.5f);
			velocity.y = 10f;
		} else if (other.gameObject.tag == "DestructibleGround") {
			Ground = other.gameObject;
		}
	}

void OnTriggerEnter2D(Collider2D other)
{
	if (other.gameObject.tag == "TeleportTrigger") {
		this.gameObject.GetComponent<Transform> ().position = other.GetComponent<TeleportationTrigger> ().telePos;
	}
	else if (other.gameObject.tag == "BoxDamageTrigger") {
		PlaySound (2, 1f);
	}
}

}
