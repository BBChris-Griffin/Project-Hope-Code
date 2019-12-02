using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewDisplayText : MonoBehaviour {
	public GUIText myText;
	public string endMessage = "Is it working??";
	public string[] messages;
	public float endLevelXOffset = 10f;
	public float endLevelYOffset = 10f;
	public GameObject[] triggers;
	protected GameObject player;

	// Use this for initialization
	void Start () 
	{
		player = GameObject.FindGameObjectWithTag ("Player").gameObject;
		triggers = GameObject.FindGameObjectsWithTag("TextTrigger");

	}

	// Update is called once per frame
	void Update () 
	{

		Vector3 currentPos = new Vector3 (player.transform.position.x + 10f, player.transform.position.y + 10f, 0);

		if (player.GetComponent<PlayerPlatformerController> ().endLevel) {
			this.transform.position = new Vector3 (player.transform.position.x + endLevelXOffset, player.transform.position.y + endLevelYOffset, 0);
			myText.text = endMessage;
		} 
		else {
			myText.text = " ";
		}

		for (int i = 0; i < triggers.Length; ++i) {
			if (triggers [i].GetComponent<TextTrigger> ().triggered) {
				myText.text = triggers [i].GetComponent<TextTrigger> ().message;
			}
		}
	}
}

