using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class newSlideShow : MonoBehaviour {

	public Texture[] imageArray;
	private float timeSinceLast = 1.0f;
	public float changeTime = 10.0f;
	private int currentImage;
	private Rect imageRect;
	private Rect buttonRect;

	// Use this for initialization
	void Start () 
	{
		currentImage = 0;
		imageRect = new Rect (0, 0, Screen.width, Screen.height);
		GUI.Label (imageRect, imageArray [currentImage]);
		currentImage++;	
	}
	
	// Update is called once per frame
	void OnGUI ()
	{
		if (currentImage == imageArray.Length || (Input.GetButtonUp ("Jump") || Input.GetButtonUp("Submit")))
			SceneManager.LoadScene (0);
		
			GUI.Label (imageRect, imageArray [currentImage]);
		if(timeSinceLast > changeTime)
		{
			currentImage++;	
			timeSinceLast = 0.0f;
			Debug.Log (timeSinceLast);
		}
		timeSinceLast += Time.deltaTime;
	}
}
