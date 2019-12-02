using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarFollow : MonoBehaviour {

	public Vector3 offset;

	//protected Transform player;
	protected Transform camera;


	// Use this for initialization
	void Awake () {
		//Sets up the reference
		//player = GameObject.FindGameObjectWithTag("Player").transform;
		camera = GameObject.FindGameObjectWithTag("MainCamera").transform;

	}
	
	// Update is called once per frame
	void Update () {
		//transform.position = player.position + offset;
		transform.position = camera.position + offset;

	}
}
