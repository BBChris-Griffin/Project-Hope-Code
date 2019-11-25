using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPauser : MonoBehaviour {

	public AudioSource audio;

	// Update is called once per frame
	void Update () {
		if (Time.timeScale == 0) {
			//audio.Pause ();
			audio.volume = 0.25f;
		} else {
			//audio.UnPause ();
			audio.volume = 0.664f;
		}
	}
}
