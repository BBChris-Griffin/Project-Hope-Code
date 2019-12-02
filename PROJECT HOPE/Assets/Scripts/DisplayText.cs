using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayText : MonoBehaviour {

	public Text myText;
	private string endMessage = "Nice One, Dude!";
	public string[] messages;
	public float endLevelXOffset = 390f;
	public float endLevelYOffset = 390f;
	public GameObject[] triggers;
	protected GameObject player;

	// Use this for initialization
	void Start () 
	{
		player = GameObject.FindGameObjectWithTag ("Player").gameObject;
		triggers = GameObject.FindGameObjectsWithTag("TextTrigger");
	}

	void OnLevelWasLoaded()
	{
		triggers = GameObject.FindGameObjectsWithTag("TextTrigger");
	}
	
	// Update is called once per frame
	void Update () 
	{
		int count = 0;
		Vector3 currentPos = new Vector3 (player.transform.position.x + 10f, player.transform.position.y + 10f, 0);

		for (int i = 0; i < triggers.Length; ++i) {
			if (triggers [i].GetComponent<TextTrigger> ().triggered) {
				count++;
			}
		}

		for (int i = 0; i < triggers.Length; ++i) {
			if (triggers [i].GetComponent<TextTrigger> ().triggered) {
				this.transform.position = new Vector3 (player.transform.position.x, 
					player.transform.position.y, 0);
				myText.text = triggers [i].GetComponent<TextTrigger> ().message;
			}
		}

		if (player.GetComponent<PlayerPlatformerController> ().endLevel) {
			this.transform.position = new Vector3 (player.transform.position.x, player.transform.position.y, 0);
			myText.text = endMessage;
		} 

		if (count > 1) {
			for (int i = 0; i < triggers.Length; ++i) {
				triggers [i].GetComponent<TextTrigger> ().triggered = false;
			}
		}

	}
}
