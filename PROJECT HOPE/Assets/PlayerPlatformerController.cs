using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerPlatformerController : PhysicsObject {

	public Transform headCheck;
	public LayerMask whatIsCeiling;
	public Transform Penny;
	public Transform Pole;
	//protected EnemyScript Badass;

	public AudioClip[] sfx;
	private AudioSource audio;

	public float playerLives = 9f;
	public float jumpTakeOffSpeed = 7f;
	public float maxSpeed = 7;
	public float minFallSpeed = 1f;
	public float wallLimit = 10f;
	public float headFall = 0.5f;
	public float newJumpSpeed;
	public float sprintMultiplier = 1.25f;
	public float springHeight = 10f;
	public int creditScene = 2;
	public float endLevelWaitingTime = 10f;
	public float Range = 0.5f;

	private float turnAngle = 4f;
	public Vector3 checkPointLocation;
	public float currPlayerLife; 
	private bool checkPoint = false;

	private bool poleJump = false;

	private GameObject heli;

	public bool groundStomp = false;
	public float deltaXDistance = 0.0f;
	public bool stompReady = false;

	private bool kick = false;
	private bool airKick = false;
	private bool wallBounce = false;
	private bool slide = false;
	private bool slidejump;
	private bool flip;
	private bool dontFlip = false;
	private float headRadius = 0.2f;
	public float turnSpeed = 0.5f;
	private bool jumped = false;
	private bool wallJump = false;
	private bool poleSwing = false;
	private bool poleSwung = false;
	private bool slip = false;
	private bool rising = false;
	private Vector2 offset;
	public bool endLevel = false;

	/// <summary>
	/// Dodging	/// </summary>
	private bool dodging = false;
	private bool rightDodge = false;
	private bool leftDodge = false;
	private bool gDodge = false;
	private bool aDodge = false;

	//// <health bar>
	public float health = 100f;
	public float damageAmount = 10f;
	private SpriteRenderer healthBar;
	private Vector3 healthScale;
	private bool slideJump = false;
	private bool fallen = false;
	private bool soundPlayed = false;

	 
	protected float dodgeTimer = 0.0f;
	protected float invinsibleTimer = 2.1f;

	//Rotation /// <summary>
	private float RotateSpeed = 5f;
	private float Radius;
	private Vector2 center;
	private float angle;

	private bool celebrate = false;
	private Vector3 startPos;
	private int checkCounter = 0;
	private Vector3 firstCheckLocation;
	public bool destoryCamera = false;

	private GameObject pauseMenu;

	private SpriteRenderer spriteRenderer;
	private Animator animator;
	// Use this for initialization

	void Awake () {
		DontDestroyOnLoad (this);

		if (FindObjectsOfType (GetType ()).Length > 1) {
			Destroy (this.gameObject);
		}
		

		spriteRenderer = GetComponent<SpriteRenderer> ();
		animator = GetComponent<Animator> ();
		//healthBar = GameObject.Find("HealthBar").GetComponent<SpriteRenderer>();
		newJumpSpeed = jumpTakeOffSpeed;
		Camera = GameObject.FindGameObjectWithTag ("MainCamera").gameObject;
		audio = GetComponent<AudioSource> ();
		currPlayerLife = playerLives;

		//pauseMenu = GameObject.FindGameObjectWithTag ("Menu").gameObject;
		//Initial Scale for the health bar
		//healthScale = healthBar.transform.localScale;
	}

	protected override void ComputeVelocity()
	{
		//facingRight = false;
		Vector2 move = Vector2.zero;
		Vector2 wallPropulsion = Vector2.zero;
		Vector2 dodge = Vector2.zero;
		Vector2 sliding = Vector2.zero;

		//////////////////////BASICS////////////////////////////////////////////////////////////////////////////////////////////
		/// 
		//headHit = Physics2D.Linecast(transform.position, headCheck.position, 1 << LayerMask.NameToLayer ("Level"));
		//headHit = Physics2D.OverlapCircle(headCheck.position, headRadius, whatIsCeiling);
			move.x = Input.GetAxis ("Horizontal");			
			sliding.x = Input.GetAxis ("Down");
			wallPropulsion.x = Input.GetAxis ("Jump");
			dodge.x = Input.GetAxis ("Dodge");

		if (!dodging)
			dodgeTimer = 0f;
		else
			dodgeTimer += 1;

		invinsibleTimer += Time.deltaTime;

		//SLIDING JUMPING////////////////////////////////////////////////////////////////
		if (slide && Input.GetButtonDown ("Jump") && Input.GetButton ("Down")) {
			slideJump = true;	
			PlaySound (0, 1f);
		} 

		if (slideJump && (velocity.y == 0 || Input.GetButtonUp("Down"))) {
			jumpTakeOffSpeed = newJumpSpeed;
			slideJump = false;
		}

		if (slideJump) {
			jumpTakeOffSpeed = 10.5f;
			if (wallgrounded) {
				jumpTakeOffSpeed = 7f;
			}
		}


		/////////////////JUMPING//////////////////////////////////////////////////////////////////
		if (Input.GetButtonDown ("Jump") && (grounded || wallgrounded || rising) && !poleSwing){ /*|| wallgrounded)) {/* || !doublejump)*/
				velocity.y = jumpTakeOffSpeed;
			jumped = true;
			PlaySound (0, 1f);
				//if (!grounded && !doublejump)
				//doublejump = true; 
		} else if (Input.GetButtonUp ("Jump") && !wallgrounded && !headHit) {
				if (velocity.y > 0)
					velocity.y *= 0.5f;

				/*if (velocity.y <= 0)
				doublejump = false; */
			} else if (Input.GetButton ("Jump") && headHit) {
				if (velocity.y > 0)
					velocity.y *= headFall;
			}
			
	
			if (Input.GetButtonUp ("Jump") && wallgrounded) {
				if (velocity.y > 0)
					velocity.y *= 0.5f;
			}


			//Code for Sliding Feature - To be implemented when slide animation is made//////////////////////////////////
		if (currentNormal.y < 0.95 && currentNormal.y > minGroundNormalY && Input.GetButtonDown ("Down")) {
				slide = true;
		} else if (Input.GetButtonUp ("Down") || currentNormal.y > 0.95 /*|| currentNormal.y < minGroundNormalY*/)
				slide = false;


		///SPRITE FLIPPING//////////////////////////////////////////////////////////////////////////////////////////////////
		if (dontFlip)
			move.x = 0;
		
		if (wallgrounded && Input.GetButtonDown("Jump")) {
			facingRight = !facingRight;
			spriteRenderer.flipX = !spriteRenderer.flipX;
		} else if(Input.GetButton("Horizontal"))
			{
				if (move.x < -0.01f && facingRight ) {
					facingRight = !facingRight;
					spriteRenderer.flipX = !spriteRenderer.flipX;
					if (!grounded && !wallgrounded)
						flip = true;
				}
				else if (move.x > 0.01f && !facingRight) {
					facingRight = !facingRight;
					spriteRenderer.flipX = !spriteRenderer.flipX;
					if (!grounded && !wallgrounded)
						flip = true;
				}
			}

		if (grounded || wallgrounded)
			flip = false;

		if (!grounded && wallgrounded && !soundPlayed) {
			PlaySound (1, 0.5f);
			soundPlayed = true;
		} 
		else if (grounded || !wallgrounded) {
			soundPlayed = false;
		}


		if (Input.GetButtonDown ("Kick")) {
			kick = true;
		} 
		else if (Input.GetButtonUp ("Kick")) 
		{
			kick = false;
			groundStomp = false;
		}

		if (Input.GetButtonDown ("Kick") && !grounded) {
			airKick = true;
		} 
		else if (Input.GetButtonUp ("Kick") || grounded) 
		{
			airKick = false;
		}
			
		if (stompReady) 
		{
			if (deltaXDistance > -0.3f && deltaXDistance < 0.5f && Input.GetButtonDown ("Kick")) {
				groundStomp = true;
			} 

		}

		///HORIZONTAL MOVEMENT/////////////////////////////////////////////////////////////
		if (!dodging && !dontFlip && !(Input.GetButton("Kick") && grounded))
			targetVelocity = move * maxSpeed;

		if (Input.GetButton ("Sprint")) {
			targetVelocity = move * maxSpeed * sprintMultiplier;
		}
		/*else if (!dodging && !grounded)
			targetVelocity = move * maxSpeed * 0.85f;*///IMPLEMENT THIS LATER, MORE SUITABLE!!!!!!!!!!

		if (flip && !dontFlip)
			targetVelocity = move * maxSpeed * turnSpeed;

		///DODGING/////////////////////////////////////////////////////////////////////////////////////////
		if (Input.GetButtonDown("Dodge") && Input.GetButton ("Horizontal")) {
			dodging = true;
			if (move.x > 0)
				rightDodge = true;
			else
				leftDodge = true;
		}

		if (rightDodge) {
			if (grounded)
				gDodge = true;
			else
				aDodge = true;
			move.x = 1f;
			targetVelocity = move* maxSpeed * 1.5f;	//Manipulate the last float to make faster			
		} else if (leftDodge) {
			if (grounded)
				gDodge = true;
			else
				aDodge = true;
			move.x = -1f;
			targetVelocity = move * maxSpeed * 1.5f;
		}

		if (dodgeTimer > 10f) { // Manipulate condition to make longer
			dodging = false;
			rightDodge = false;
			leftDodge = false;
			gDodge = false;
			aDodge = false;
		}

		//SPRINTING!!!!!!!!!////////////////////////////////////////////

		
				//SLIDING!!!!!!!!!!/////////////////////////////
		if (slide) 
		{
			this.gameObject.transform.localEulerAngles = new Vector3(0f, 0f, -groundNormal.x*(180f/3.14f));

			if (groundNormal.x > 0.4f && groundNormal.x < 0.8f) {
				targetVelocity = sliding * maxSpeed * 1.25f;
				if (!facingRight) {
					facingRight = !facingRight;
					spriteRenderer.flipX = !spriteRenderer.flipX;
				}
			} else if (groundNormal.x < -0.4f && groundNormal.x > -0.8f) {
				targetVelocity = -sliding * maxSpeed * 1.25f;
				if (facingRight) {
					facingRight = !facingRight;
					spriteRenderer.flipX = !spriteRenderer.flipX;
				}
			}
		}

		if (slip) 
		{
			this.gameObject.transform.localEulerAngles = new Vector3(0f, 0f, -groundNormal.x*(180f/3.14f));

			sliding = new Vector2 (1, 0);
			if (groundNormal.x > 0.4f && groundNormal.x < 0.8f) {
				targetVelocity = sliding * maxSpeed;
				if (!facingRight) {
					facingRight = !facingRight;
					spriteRenderer.flipX = !spriteRenderer.flipX;
				}
			} else if (groundNormal.x < -0.4f && groundNormal.x > -0.8f) {
				targetVelocity = -sliding * maxSpeed;
				if (facingRight) {
					facingRight = !facingRight;
					spriteRenderer.flipX = !spriteRenderer.flipX;
				}
			}
		}
		else
			sliding = Vector2.zero;

        ///WALL JUMP 2.0////////PROPULSION////////////////////////////////////////////////////////////////////////////
        if (jumped && leftWallTime > 0 && !grounded && !wallJump) {
            if (rightWallTime > 0)
            {
                jumped = false;
            }
            else
            {
                targetVelocity = (wallPropulsion * 1.5f) * (maxSpeed / 2);
                wallTime += Time.deltaTime;
                dontFlip = true;
                if (Input.GetButton("Horizontal") && flip)
                {
                    wallJump = true;
                    targetVelocity = move * maxSpeed * turnSpeed;
                }
            }
        } else if (jumped && rightWallTime > 0 && !grounded && !wallJump) {
            if (leftWallTime > 0)
            {
                jumped = false;
            }
            else
            {
                targetVelocity = (-wallPropulsion * 1.5f) * (maxSpeed / 2);
                wallTime += Time.deltaTime;
                dontFlip = true;
                if (Input.GetButton("Horizontal") && flip)
                {
                    wallJump = true;
                    targetVelocity = move * maxSpeed * turnSpeed;
                }
            }
        }

		///TIME THAT THE PLAYER CANNOT MOVE AFTER WALL JUMP///////////////////
		if (wallTime > 1.5f)
			dontFlip = false;

		if (grounded || Input.GetButtonUp("Jump")) {
			wallTime = 0;
			rightWallTime = 0;
			leftWallTime = 0;
			wallJump = false;
			dontFlip = false;
			jumped = false;
			rising = false;
		}


		if (!poleSwing && !slide ) {
			this.gameObject.transform.localEulerAngles = new Vector3(0f, 0f, 0f);

		}

		//ROTATION////////////////////////////////////////////////////////////////////////////////////////////////
		/*if (!poleSwing) {
			offset = new Vector2 (0, -1) * Radius;
			angle = 4f;
		}


		if (poleSwing && !Input.GetButton("Jump")) {


			if (!facingRight) {
				transform.position = center + offset;
				angle += RotateSpeed * Time.deltaTime;
				offset = new Vector2 (Mathf.Sin (angle),Mathf.Cos (angle)) * Radius;

				this.gameObject.transform.localEulerAngles = new Vector3(0f, 0f, (angle+0.1f)*(-180/3.14f));
			} else {
				transform.position = center + offset;
				angle += RotateSpeed * Time.deltaTime;
				offset = new Vector2 (Mathf.Cos (angle),Mathf.Sin (angle)) * (Radius+0.1f);
				this.gameObject.transform.localEulerAngles = new Vector3(0f, 0f, (angle+0.1f)*(180/3.14f));
			}
			//rb2d.MoveRotation (angle);
		} 
		if (poleSwing && Input.GetButton("Jump")) {
			jumped = true;

			if (Mathf.Cos (angle) > 0.9f) {
				velocity.y = jumpTakeOffSpeed;
				poleSwing = false;
			} 
			else if (Mathf.Cos (angle) < -0.9f) {
				velocity.y = -jumpTakeOffSpeed;
				poleSwing = false;
				Physics.IgnoreLayerCollision (13, 14);
				//Physics.IgnoreCollision (this.GetComponent<Collider2D> (), GameObject.FindGameObjectWithTag ("Pole").GetComponent<Collider> ());
			}

			if (!facingRight) {
				transform.position = center + offset;
				angle += RotateSpeed * Time.deltaTime;
				offset = new Vector2 (Mathf.Sin (angle),Mathf.Cos (angle)) * Radius;

				this.gameObject.transform.localEulerAngles = new Vector3(0f, 0f, (angle+0.1f)*(-180/3.14f));
			} else {
				transform.position = center + offset;
				angle += RotateSpeed * Time.deltaTime;
				offset = new Vector2 (Mathf.Cos (angle),Mathf.Sin (angle)) * (Radius+0.1f);
				this.gameObject.transform.localEulerAngles = new Vector3(0f, 0f, (angle)*(180/3.14f));
			}



			//Add if statements for angles
			if (Mathf.Cos (angle) < 0) {
				velocity.y = jumpTakeOffSpeed;
			} else if(Mathf.Cos (angle) >= 0 && !grounded) {
				velocity.y = 0;
			}
			if (facingRight) {
				if (Mathf.Sin (angle) > 0 && !grounded) {
					targetVelocity = move* maxSpeed * 1.25f;
				} else if (Mathf.Sin (angle) <= 0 && !grounded) {
					targetVelocity = move * 0f;
				}
			} else {
				if (Mathf.Sin (angle) < 0 && !grounded) {
					targetVelocity = move * maxSpeed * 1.25f;
				} else if (Mathf.Sin (angle) >= 0 && !grounded) {
					targetVelocity = move * 0f;
				}
			}
			poleSwing = false;
			Debug.Log ("Here is the angle, sin, and cos: ");
			Debug.Log (angle);
			Debug.Log (Mathf.Sin(angle));
			Debug.Log (Mathf.Cos(angle));
		}*/

		//HANGING/////////////////////////////////////////
		if (!poleSwing) {
			offset = new Vector2 (0, -1) * Radius;
			angle = 4f;
		}


		if (poleSwing && heli != null) 
		{
			this.gameObject.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
			center = heli.transform.position;
			Radius = heli.gameObject.GetComponent<CircleCollider2D> ().radius + 0.05f;

			if (!facingRight) {
				transform.position = center + offset;
				offset = new Vector2 (Mathf.Sin (angle),Mathf.Cos (angle)) * Radius;
			} 
			else {
				transform.position = center + offset;
				offset = new Vector2 (Mathf.Cos (angle),Mathf.Sin (angle)) * Radius;
			}
		} 
		else {
			poleSwing = false;
		}


		//RISING/TRANSPORTING PLATFORM//////////////////////////////////////////////////



		if (wallgrounded && !grounded) 
		{
			wallBounce = true;
		} 
		else 
		{
			wallBounce = false;
		}


		///ANIMATIONS//////////////////////////////////////////////////////////////////////////////////////////////////////
		animator.SetBool ("slide", slide || slip);
		animator.SetBool ("grounded", grounded);
		animator.SetBool ("hurt", hurt);
		animator.SetBool ("poleSwing", poleSwing);
		//animator.SetBool ("wallBounce", wallBounce);
		animator.SetBool("kick", kick);
		animator.SetBool ("groundStomp", groundStomp);
		animator.SetBool("airKick", airKick);
		animator.SetFloat ("velocityX", Mathf.Abs (velocity.x) / maxSpeed);
		animator.SetBool ("slideJump", slideJump);
		//DODGE 
		//animator.SetBool ("hurt", gDodge);
		//animator.SetBool ("isOnLadder", aDodge);
			
		//if (touch)
		//	Penny.RotateAround (Pole.position, Penny.up, 20f * Time.deltaTime); 

		///HEALTH/////////TAKE DAMAGE/////////////////////////////////////////////////////
		if (invinsibleTimer > 3f) 
		{
			for (int i = 0; i < enemy.Length; ++i) 
			{
				if (enemyContact[i])
				{
					Vector2 deltaDistance = Player.position - enemy[i].GetComponent<Transform>().position;
					TakeDamage ();

					if (facingRight && deltaDistance.x > 0) 
					{
						facingRight = !facingRight;
						spriteRenderer.flipX = !spriteRenderer.flipX;
					} else if (!facingRight && deltaDistance.x < 0) 
					{
						facingRight = !facingRight;
						spriteRenderer.flipX = !spriteRenderer.flipX;
					}
					invinsibleTimer = 0;
				}
			}
		}

		if (playerLives == 0 ) 
		{
			destoryCamera = true;
			SceneManager.LoadScene (0); // Loads Main Menu
			Destroy (this.gameObject);
			/*StartCoroutine ("ReloadGame");
			Camera.GetComponent<DeadzoneCamera> ().enabled = false;
			StartCoroutine(Death ());*/
		}
		else if (playerLives < currPlayerLife) 
		{
			//GetComponent<PlayerPlatformerController>().enabled = false;
			currPlayerLife--;
			StartCoroutine ("ReloadGame");
			Camera.GetComponent<DeadzoneCamera> ().enabled = false;
			StartCoroutine(TempDeath ());
			//GetComponent<PlayerPlatformerController>().enabled = true;
			//this.gameObject.transform.position = new Vector3 (checkPointLocation.x, checkPointLocation.y, 1);
		}

		if (endLevel) 
		{
			celebrate = true;
			if(grounded)
				GetComponent<PlayerPlatformerController>().enabled = false;
			animator.SetBool ("finalCelebration", celebrate);
			StartCoroutine (DestroyCamera ());
			Invoke("LoadAfterWait", 2.5f);
		}

		/*if (pauseMenu.GetComponent<LoadSceneOnClick> ().destroyOldModels) 
		{
			Destroy (this.gameObject);
		}*/
	}


	void LoadAfterWait()
	{
		//yield return new WaitForSeconds(endLevelWaitingTime);
		SceneManager.LoadScene (creditScene);
		Destroy (this.gameObject);
	}

	void TakeDamage()
	{
		//Reduce Health
		health -= damageAmount;
		playerLives--;
		GetComponent<PlayerPlatformerController>().enabled = false;
		PlaySound (2, 1f);

		//Update Health Bar
		//UpdateHealthBar();
	}

	/*public void UpdateHealthBar()
	{
		//Set the health bar's color to proportion of
		healthBar.material.color = Color.Lerp(Color.green, Color.red, 1-health*0.01f);

		//Set the scale of the health bar to be proportional to the player's health
		healthBar.transform.localScale = new Vector3(healthScale.x * health * 0.01f, 1, 1); 
	}*/

	IEnumerator ReloadGame()
	{			
		// ... pause briefly
		yield return new WaitForSeconds(1.5f);
		// ... and then reload the level.
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
	}

	IEnumerator DestroyCamera()
	{			
		// ... pause briefly
		yield return new WaitForSeconds(2.25f);
		// ... and then reload the level.
		destoryCamera = true;
	}

	void PlaySound(int clip, float volume)
	{
		audio.clip = sfx[clip];
		audio.volume = volume;
		audio.Play ();
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.tag == "Pole") {
			poleSwing = true;
			heli = other.gameObject;
			center = other.transform.position;
			Radius = other.gameObject.GetComponent<CircleCollider2D> ().radius + 0.05f;
		} else if (other.gameObject.tag == "SlipperyGround") {
			slip = true;
		} else if (other.gameObject.tag == "SpringGround") {
			float deltaXDistance = this.gameObject.transform.position.x - other.gameObject.transform.position.x;
			float heightDistance = this.gameObject.transform.position.y - other.gameObject.transform.position.y;
			if ((deltaXDistance < Range && deltaXDistance > -Range) && heightDistance > 0) {
				PlaySound (3, 1f);
				velocity.y = springHeight;
				//jumped = true;
			}
		} else if (other.gameObject.tag == "Box") {
			deltaXDistance = this.gameObject.transform.position.x - other.gameObject.transform.position.x;
			stompReady = true;
		} else if (other.gameObject.tag == "BrittleGround") {
			/*float deltaXDistance = this.gameObject.transform.position.x - other.gameObject.transform.position.x;
			float heightDistance = this.gameObject.transform.position.y - other.gameObject.transform.position.y;
			float destroyRange = other.gameObject.GetComponent<CrumbleGround> ().destructRange;

			if ((deltaXDistance < destroyRange && deltaXDistance > -destroyRange) && heightDistance > 0)*/
				PlaySound(4, 1f);
		}
	}

	void OnCollisionExit2D(Collision2D other)
	{
		/*if (other.gameObject.tag == "SpringGround") {
			rising = false;
		}*/
		if (other.gameObject.tag == "SlipperyGround") {
			slip = false;
		}
		else if (other.gameObject.tag == "Box") {
			stompReady = false;
		}

	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "KillZone") {
			playerLives--;
		}
		else if (other.gameObject.tag == "EndLevel") {
			endLevel = true;
		} 
		else if (other.gameObject.tag == "CheckPoint") {
			checkPoint = true;
			checkPointLocation = new Vector3 (other.gameObject.transform.position.x, other.gameObject.transform.position.y, 1f);
			if (checkCounter == 0) {
				firstCheckLocation = new Vector3 (other.gameObject.transform.position.x, other.gameObject.transform.position.y, 1f);
				checkCounter++;
			}
		}
	}

	IEnumerator Death()
	{
		playerLives = 9f;
		currPlayerLife = playerLives;
		yield return new WaitForSeconds (1.5f);
		this.gameObject.transform.position = new Vector3 (firstCheckLocation.x, firstCheckLocation.y+1f, 1);
	}

	IEnumerator TempDeath()
	{
		if (playerLives  == 9) {
			yield return new WaitForSeconds (1.5f);
			this.gameObject.transform.position = new Vector3 (firstCheckLocation.x, firstCheckLocation.y+1f, 1);
		} 
		else {
			yield return new WaitForSeconds (1.5f);
			this.gameObject.transform.position = new Vector3 (checkPointLocation.x, checkPointLocation.y+1f, 1);
		}

	}

}
