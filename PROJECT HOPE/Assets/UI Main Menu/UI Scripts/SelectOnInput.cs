using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectOnInput : MonoBehaviour {

	public EventSystem eventSystem;
	public GameObject[] selectedObject;
	private int selection = 0;

	private bool buttonSelected;

	// Use this for initialization
	void Start () {
		eventSystem.SetSelectedGameObject (selectedObject [0]);
	}

	void OnEnable(){
		eventSystem.SetSelectedGameObject (selectedObject [0]);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetAxis ("MenuVertical") > 0 && Input.GetButtonDown ("MenuVertical")) {
			selection--;
			if (selection < 0) {
				selection = selectedObject.Length;
			}
			eventSystem.SetSelectedGameObject (selectedObject [selection % selectedObject.Length]);
			buttonSelected = true;
		} 
		else if (Input.GetAxis ("MenuVertical") < 0 && Input.GetButtonDown ("MenuVertical")) {
			selection++;
			eventSystem.SetSelectedGameObject (selectedObject [selection % selectedObject.Length]);
		}

	}

	/*private void OnDisable()
	{
		buttonSelected = false;
	}*/
}
