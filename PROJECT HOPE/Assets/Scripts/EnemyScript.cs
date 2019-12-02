using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : EnemyPhysics {

	public float maxSpeed = 3f;
	public float health = 100f;
	public float damageAmount = 10f;
	public float jumpTakeOffSpeed = 7f;
	public bool destroyTele = false;
	private float jumpTimer = 0f;
	public float jumpTimerLimit = 10f; // Increase/Decrease to make enemy jump higher/lower
	public bool triggerReset = false;
	public bool jumpCheck = false;
	public GameObject trigger;
	protected GameObject jumpTrigger;
	protected GameObject teleTrigger;
	private Collider2D teleCollider;
	private bool teleport = false;
	public bool destroy = false;
	public AudioClip[] sfx;
	private AudioSource audio;

	private SpriteRenderer spriteRenderer;
	private Animator animator;

	void Awake()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
		animator = GetComponent<Animator>();
		jumpTrigger = GameObject.FindGameObjectWithTag ("JumpTrigger").gameObject;
		teleTrigger = GameObject.FindGameObjectWithTag ("TeleportTrigger").gameObject;
		teleCollider = teleTrigger.GetComponent<Collider2D> ();
		audio = GetComponent<AudioSource> ();
	}

	protected override void ComputeVelocity()
	{
		Vector2 move = Vector2.zero;
		Vector2 deltaDistance = Player.position - this.gameObject.GetComponent<Transform>().position;

		if (trigger.GetComponent<EnemyTrigger>().trigger)
        {
				if (deltaDistance.x > 0.1)
					move = new Vector2 (1, 0);
				else if (deltaDistance.x < -0.1)
					move = new Vector2 (-1, 0);

				targetVelocity = move * maxSpeed;

				if ( jumpCheck ) {
					velocity.y = jumpTakeOffSpeed;
				} 

				if (facingRight && deltaDistance.x > 0) {
					facingRight = !facingRight;
					spriteRenderer.flipX = !spriteRenderer.flipX;
				} else if (!facingRight && deltaDistance.x < 0) {
					facingRight = !facingRight;
					spriteRenderer.flipX = !spriteRenderer.flipX;
				}
		} 

		animator.SetBool ("grounded", grounded);
		animator.SetFloat ("velocityX", Mathf.Abs (velocity.x) / maxSpeed);


	}
		
void OnTriggerEnter2D(Collider2D other)
{
		if (other.gameObject.tag == "TeleportTrigger") {
		this.gameObject.GetComponent<Transform> ().position = other.GetComponent<TeleportationTrigger> ().telePos;
		}
		
	if(other.gameObject.tag == "JumpTrigger")
		jumpCheck = true;
}

void OnTriggerExit2D(Collider2D other){
	if (other.gameObject.tag == "TeleportTrigger") {
		if(other.GetComponent<TeleportationTrigger>().disappear || destroyTele)
			Destroy (other.gameObject);
		}

	else if(other.gameObject.tag == "JumpTrigger")
		jumpCheck = false;


}

void OnCollisionEnter2D(Collision2D other)
{
	if (other.gameObject.tag == "SpringGround") {
			velocity.y = 10f;		
	}	
}
	
void PlaySound(int clip)
{
	audio.clip = sfx[clip];
	audio.Play ();
}



}
