using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pauser : MonoBehaviour {

	protected bool paused = false;

	// Update is called once per frame
	void Update () {
		if(Input.GetKeyUp(KeyCode.P))
		{
			paused = !paused;
		}

		if (Time.timeScale > 1) {
			paused = false;
		}

		if(paused)
			Time.timeScale = 0; //TimeScale determines how fast time moves (multiplication). Now, time is gone.
		else
			Time.timeScale = 1; // Time is back to normal


	}
}
