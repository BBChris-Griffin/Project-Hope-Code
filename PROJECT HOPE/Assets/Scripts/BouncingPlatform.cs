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

	//private Animator animator;

	// Use this for initialization
	void Awake () {
		spriteRenderer = GetComponent<SpriteRenderer> ();
		audio = GetComponent<AudioSource> ();
		VGround = GameObject.FindGameObjectWithTag ("VDestructibleGround").gameObject;
		//animator = GetComponent<Animator>();
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


		/*if (Input.GetButtonDown ("Kick") && deltaDistance.x > -0.3f && deltaDistance.x < 0.5f && contact) {
			//if (col.gameObject.tag == "DestructibleGround") {
			//Destroy (gameObject);
			floorBreak = true;
			boxKick = true;
			PlaySound (0, 1f);
			//}
		} else if (kick && contact && !floorBreak || (velocity.y != 0 && deltaDistance.x > -1 && deltaDistance.x < 1 
			&& deltaDistance.y > -1 && deltaDistance.y < 1 && kick)) {
			distance = 40f;
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



			distance--;
		}
		if (distance == 1 && check) {
			boxKick = false;
			horizontalFloorBreak = false;
		}
		if (distance == 0)
			check = false;
		/*else if (kick && timer > 1 && !floorBreak) {
			if (deltaDistance.x > 0.2)
				move = new Vector2 (-1, 0);
			else if (deltaDistance.x < -0.2)
				move = new Vector2 (1, 0);
			targetVelocity = move * maxSpeed;
			timer++;

			if (timer > boxSlideTime) {
				timer = 0;
				kick = false;
			}
		} else if (timer > 5f) {
			timer = 0;
		}*/

		/*if (horizontalFloorBreak && rb2d.IsTouching (VGround.GetComponent<Collider2D>())) {
		PlaySound (0);
	}*/

}

/*void PlaySound(int clip, float volume)
{
	audio.clip = sfx[clip];
	audio.volume = volume;
	audio.Play ();
}*/

void OnCollisionEnter2D(Collision2D other)
{

	if (other.gameObject.tag == "SpringGround") {
		//PlaySound (3, 0.5f);
		velocity.y = 10f;
	}
}


}
