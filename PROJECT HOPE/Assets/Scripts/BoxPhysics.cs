﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxPhysics : MonoBehaviour {

	[HideInInspector] public bool facingRight = false;

	public Transform Player;
	public Transform MainEnemy;

	public float minGroundNormalY = .65f;//SC
	public float wallNormalRight = 0.1f; // WallJump
	public float wallNormalLeft = 0.9f;
	public float maxCeilingNormal = -0.65f;
	public float minCeilingNormal = 0.9f;
	public float gravityModifier = 1f;//SG
	public float wallTime = 0f;
	public float leftWallTime = 0f;
	public float rightWallTime = 0f;
	public bool ttest = false;


	protected bool Rodtouch;
	//protected bool[] enemyContact = new bool[2];
	public Collider2D RodCollider;
	public Collider2D enemy;
	//public Collider2D[] enemy = new Collider2D[2];

	protected Vector2 targetVelocity;//HM
	protected bool grounded;//SC
	protected bool wallgrounded; // Walljump
	public bool headHit = false;
	protected Vector2 groundNormal;//SC
	protected Rigidbody2D rb2d;//SG
	public Vector2 velocity;//SG
	protected Collider2D col;
	protected Collision2D collision;
	public Vector2 currentNormal;
	//public Transform headCheck;
	//protected bool doublejump = false;

	protected const float minMoveDistance = 0.001f;//DO
	protected const float shellRadius = 0.01f; // DO Extra Padding to insure that there is no overlapping in colliders
	protected ContactFilter2D ContactFilter;//DO 
	protected ContactFilter2D RodFilter;
	protected ContactFilter2D EnemyFilter;
	protected RaycastHit2D[] hitBuffer = new RaycastHit2D[16]; //DO
	protected List<RaycastHit2D> hitBufferList = new List<RaycastHit2D> (16); // DO Creates an empty list to store hitbuffer data
	protected bool ignore = true;
	//public bool useLayerMask = true;
	//public LayerMask mask;
	//ContactFilter.SetLayerMask(mask);

	void OnEnable(){
		rb2d = GetComponent<Rigidbody2D> ();//SG
		col = GetComponent<Collider2D>();
		Player = GameObject.FindGameObjectWithTag ("Player").transform;
	}


	// Use this for initialization
	void Start () {
		ContactFilter.useTriggers = false; // DO
		ContactFilter.SetLayerMask (Physics2D.GetLayerCollisionMask (gameObject.layer)); // DO
		ContactFilter.useLayerMask = true;// DO

	}

	// Update is called once per frame
	void Update () {
		targetVelocity = Vector2.zero;
		ComputeVelocity ();
	}

	protected virtual void ComputeVelocity(){
	}

	void FixedUpdate(){

		//doublejump = false;
		//facingRight = true;

		velocity += gravityModifier * Physics2D.gravity * Time.deltaTime; // SG m/s^2 * m/s^2 * s = m/s
		velocity.x = targetVelocity.x;//HM

		grounded = false;//SC
		wallgrounded = false; // WJ
		headHit = false;


		Vector2 deltaPosition = velocity * Time.deltaTime; //SG 

		Vector2 moveAlongGround = new Vector2 (groundNormal.y, -groundNormal.x);//HM

		Vector2 move = moveAlongGround * deltaPosition.x;//HM

		Movement (move, false);//HM

		move = Vector2.up * deltaPosition.y; // SG Just in the y-direction	

		Movement (move, true); // SG
	}

	void Movement(Vector2 move, bool yMovement){
		float distance = move.magnitude; // SG
		if (distance > minMoveDistance) {
			int count = rb2d.Cast (move, ContactFilter, hitBuffer, distance + shellRadius);	//DO	
			hitBufferList.Clear();
			for (int i = 0; i < count; i++) {//SC
				hitBufferList.Add (hitBuffer [i]); // Adds each hitbuffer to the list
			}

			for (int i = 0; i < hitBufferList.Count; i++) {//SC
				currentNormal = hitBufferList [i].normal;
				if (currentNormal.y > minGroundNormalY) {
					grounded = true;
					if (yMovement) {
						groundNormal = currentNormal;
						currentNormal.x = 0;
					}
				} else if (currentNormal.x > wallNormalLeft || currentNormal.x < wallNormalRight) { //WJ
					wallgrounded = true;
					wallTime = 1;
					if (currentNormal.x > wallNormalLeft)
						leftWallTime = 1;
					else if (currentNormal.x < wallNormalRight)
						rightWallTime = 1;
					if (yMovement) {
						currentNormal.y = 0;
					}
				} else if ((currentNormal.y < maxCeilingNormal) || ((currentNormal.y < 0.1 && currentNormal.y >-0.1) && (currentNormal.x > -0.1 && currentNormal.x < 0.1))){ 
					headHit = true;
					if (yMovement) {
						currentNormal.y = 0;
						currentNormal.x = 0;
					}
				}
				if (currentNormal.y < 1 && currentNormal.y > minGroundNormalY) {
					velocity += gravityModifier * Physics2D.gravity * Time.deltaTime * currentNormal.y;
					velocity.x = targetVelocity.x * currentNormal.y;
				}
				float projection = Vector2.Dot (velocity, currentNormal);//SC
				if (projection < 0)//SC
					velocity -= projection * currentNormal;
				float modifiedDistance = hitBufferList [i].distance - shellRadius;//SC
				distance = modifiedDistance < distance ? modifiedDistance : distance;//SC
			}

		}

		/*if (col.gameObject.tag == "Enemy") {
			Physics2D.IgnoreCollision(MainEnemy.GetComponent<Collider2D>(), col);
		}*/

		rb2d.position = rb2d.position + move.normalized * distance; // SG & SC
	}


}

