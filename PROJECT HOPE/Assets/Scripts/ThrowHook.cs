using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowHook : MonoBehaviour {

	public GameObject hook;
	GameObject curHook;

	private Vector2 deltaDistance;
	private GameObject[] grapple;
	private GameObject player;


	// Use this for initialization
	void Start () {
		grapple = GameObject.FindGameObjectsWithTag ("Hook");
		player = GameObject.FindGameObjectWithTag ("Player").gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		for (int i = 0; i < grapple.Length; i++) {
			deltaDistance = player.GetComponent<Transform> ().position - grapple[i].GetComponent<Transform> ().position;
			if ((deltaDistance.x < 3f && deltaDistance.x > -3f && deltaDistance.y < 3f && deltaDistance.y > -3f) && Input.GetButtonDown ("HookThrow")) {
				if ((!player.GetComponent<PlayerPlatformerController> ().facingRight && deltaDistance.x >= 0) || (player.GetComponent<PlayerPlatformerController> ().facingRight && deltaDistance.x <= 0)) {
					curHook = (GameObject)Instantiate (hook, transform.position, Quaternion.identity);

					curHook.GetComponent<RopeScript> ().destination = grapple[i].GetComponent<Transform> ().position;
				}
			}
		}
	}
}
