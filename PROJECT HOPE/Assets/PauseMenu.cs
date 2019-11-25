using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour {

	public GameObject canvas;

	// Update is called once per frame
	void Update () {
		if(Time.timeScale == 0)
			canvas.SetActive (true);
		else
			canvas.SetActive (false);
	}
}
