using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayLifeCount : MonoBehaviour {

	public Text myText;
	public Vector3 offset;
	protected GameObject player;
	protected Transform camera;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player").gameObject;
		camera = GameObject.FindGameObjectWithTag ("MainCamera").transform;
	}

	void OnLevelWasLoaded()
	{
		player = GameObject.FindGameObjectWithTag ("Player").gameObject;
		camera = GameObject.FindGameObjectWithTag ("MainCamera").transform;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = camera.position + offset;
		myText.text = player.GetComponent<PlayerPlatformerController> ().playerLives.ToString();
	}
}
