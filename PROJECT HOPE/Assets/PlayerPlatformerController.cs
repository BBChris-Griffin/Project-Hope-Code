using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerPlatformerController : PhysicsObject {

	public Transform headCheck;
	public LayerMask whatIsCeiling;
	public Transform Penny;
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

	public float deltaXDistance = 0.0f;

	private bool kick = false;
	private bool wallBounce = false;
	private bool flip;
	private bool dontFlip = false;
	private float headRadius = 0.2f;
	public float turnSpeed = 0.5f;
	private bool jumped = false;
	private bool wallJump = false;
	private bool rising = false;
	private Vector2 offset;
	public bool endLevel = false;

	//// <health bar>
	public float health = 100f;
	public float damageAmount = 10f;
	private SpriteRenderer healthBar;
	private Vector3 healthScale;
	private bool slideJump = false;
	private bool fallen = false;
	private bool soundPlayed = false;

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
		newJumpSpeed = jumpTakeOffSpeed;
		Camera = GameObject.FindGameObjectWithTag ("MainCamera").gameObject;
		audio = GetComponent<AudioSource> ();
		currPlayerLife = playerLives;
	}

	protected override void ComputeVelocity()
	{
		Vector2 move = Vector2.zero;
		Vector2 wallPropulsion = Vector2.zero;
		Vector2 dodge = Vector2.zero;
		Vector2 sliding = Vector2.zero;

		//////////////////////BASICS////////////////////////////////////////////////////////////////////////////////////////////
		/// 

			move.x = Input.GetAxis ("Horizontal");			
			sliding.x = Input.GetAxis ("Down");
			wallPropulsion.x = Input.GetAxis ("Jump");
			dodge.x = Input.GetAxis ("Dodge");

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
		if (Input.GetButtonDown ("Jump") && (grounded || wallgrounded || rising) ){
				velocity.y = jumpTakeOffSpeed;
			jumped = true;
			PlaySound (0, 1f);
			
		} else if (Input.GetButtonUp ("Jump") && !wallgrounded && !headHit) {
				if (velocity.y > 0)
					velocity.y *= 0.5f;

			} else if (Input.GetButton ("Jump") && headHit) {
				if (velocity.y > 0)
					velocity.y *= headFall;
			}
			
	
			if (Input.GetButtonUp ("Jump") && wallgrounded) {
				if (velocity.y > 0)
					velocity.y *= 0.5f;
			}


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
		}
			

		///HORIZONTAL MOVEMENT/////////////////////////////////////////////////////////////
		if (!dontFlip && !(Input.GetButton("Kick") && grounded))
			targetVelocity = move * maxSpeed;

		if (Input.GetButton ("Sprint")) {
			targetVelocity = move * maxSpeed * sprintMultiplier;
		}
		/*else if (!dodging && !grounded)
			targetVelocity = move * maxSpeed * 0.85f;*///IMPLEMENT THIS LATER, MORE SUITABLE!!!!!!!!!!

		if (flip && !dontFlip)
			targetVelocity = move * maxSpeed * turnSpeed;

		

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

		if (wallgrounded && !grounded) 
		{
			wallBounce = true;
		} 
		else 
		{
			wallBounce = false;
		}


		///ANIMATIONS//////////////////////////////////////////////////////////////////////////////////////////////////////
		animator.SetBool ("grounded", grounded);
		animator.SetBool ("hurt", hurt);
		//animator.SetBool ("wallBounce", wallBounce);
		animator.SetBool("kick", kick);
		animator.SetFloat ("velocityX", Mathf.Abs (velocity.x) / maxSpeed);
		animator.SetBool ("slideJump", slideJump);
	
		if (playerLives == 0 ) 
		{
			destoryCamera = true;
			SceneManager.LoadScene (0); // Loads Main Menu
			Destroy (this.gameObject);
			
		}
		else if (playerLives < currPlayerLife) 
		{
			currPlayerLife--;
			StartCoroutine ("ReloadGame");
			Camera.GetComponent<DeadzoneCamera> ().enabled = false;
			StartCoroutine(TempDeath ());
			
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
	}


	void LoadAfterWait()
	{
		SceneManager.LoadScene (creditScene);
		Destroy (this.gameObject);
	}

	void TakeDamage()
	{
		health -= damageAmount;
		playerLives--;
		GetComponent<PlayerPlatformerController>().enabled = false;
		PlaySound (2, 1f);

	
	}

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
