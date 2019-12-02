using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BruteEnemyScript : EnemyPhysics {

	public float maxSpeed = 1.5f;
	public float health = 100f;
	public float damageAmount = 20f;

	private SpriteRenderer spriteRenderer;
	private Animator animator;

	void Awake()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
		animator = GetComponent<Animator>();
	}

	protected override void ComputeVelocity()
	{
		Vector2 move = Vector2.zero;
		Vector2 deltaDistance = Player.position - this.gameObject.GetComponent<Transform>().position;

		if ((deltaDistance.x > -7 && deltaDistance.x < 7) && (deltaDistance.y > -3.5 && deltaDistance.y < 3.5)) 
		{
			if (deltaDistance.x > 0.1)
				move = new Vector2 (1, 0);
			else if (deltaDistance.x < -0.1)
				move = new Vector2(-1, 0);

			targetVelocity = move * maxSpeed;

			if (facingRight && deltaDistance.x > 0) 
			{
				facingRight = !facingRight;
				spriteRenderer.flipX = !spriteRenderer.flipX;
			} 
			else if (!facingRight && deltaDistance.x < 0) 
			{
				facingRight = !facingRight;
				spriteRenderer.flipX = !spriteRenderer.flipX;
			}
		}
		animator.SetBool ("grounded", grounded);
		animator.SetFloat ("velocityX", Mathf.Abs (velocity.x) / maxSpeed);
	}


}
