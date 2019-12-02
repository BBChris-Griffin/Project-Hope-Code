using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeScript : MonoBehaviour {

	public Vector2 destination;
	public float speed = 1f;

	public float distance = 2f;
	public GameObject ropeNode;
	public GameObject player;
	public GameObject lastNode;
	private bool done = false;
	// Use this for initialization
	void Start ()
	{
		player = GameObject.FindGameObjectWithTag ("Player").gameObject;

		lastNode = transform.gameObject;
	}
	
	// Update is called once per frame
	void Update () 
	{
		transform.position = Vector2.MoveTowards (transform.position, destination, speed);

		if ((Vector2)transform.position != destination) {
			if (Vector2.Distance (player.transform.position, lastNode.transform.position) > distance) {
				CreateNode ();
			}
		} else if (!done) {
			done = true;
			lastNode.GetComponent<HingeJoint2D> ().connectedBody = player.GetComponent<Rigidbody2D> ();

		}
	}

	void CreateNode()
	{
		Vector2 pos2Create = player.transform.position - lastNode.transform.position;
		pos2Create.Normalize ();
		pos2Create *= distance;
		pos2Create += (Vector2)lastNode.transform.position;

		GameObject go = (GameObject)Instantiate (ropeNode, pos2Create, Quaternion.identity);

		go.transform.SetParent (transform);

		lastNode.GetComponent<HingeJoint2D> ().connectedBody = go.GetComponent<Rigidbody2D> ();
		lastNode = go;
	}


}
