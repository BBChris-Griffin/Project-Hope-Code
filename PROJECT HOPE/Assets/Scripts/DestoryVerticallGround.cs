using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestoryVerticallGround : MonoBehaviour {
	protected GameObject[] BOX;
	public bool destruction = false;

	private Collider2D collider;

	// Use this for initialization
	void Start () {
		collider = GetComponent<Collider2D>();
		BOX = GameObject.FindGameObjectsWithTag("Box");
	}

	// Update is called once per frame
	void Update () {

		for (int i = 0; i < BOX.Length; ++i) {
			if (BOX[i].GetComponent<BoxMovement> ().horizontalFloorBreak && collider.IsTouching (BOX[i].GetComponent<Collider2D> ())) {
				Destroy (this.gameObject);
			}
		}
	}



}
