using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SlideShow : MonoBehaviour {

	public Texture[] slides;
	public float changeTime = 10.0f;
	private int currentSlide = 0;
	private float timeSinceLast = 1.0f;
	private GUITexture gui;

	// Use this for initialization
	void Start () 
	{
		transform.position = new Vector3 (0.5f, 0.5f, 0.0f);
		transform.localScale = new Vector3 (0.1f, 0.1f, 0f);
		gui = new GUITexture ();
		gui.texture = slides [currentSlide];
		gui.pixelInset = new Rect (-slides [currentSlide].width / 2.0f, -slides [currentSlide].height / 2.0f, 
			slides [currentSlide].width, slides [currentSlide].height);
		currentSlide++;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(currentSlide == slides.Length)
			SceneManager.LoadScene (0);
		if (timeSinceLast > changeTime && currentSlide < slides.Length) 
		{
			gui.texture = slides [currentSlide];
			gui.pixelInset = new Rect (-slides [currentSlide].width / 2.0f, -slides [currentSlide].height / 2.0f, 
				slides [currentSlide].width, slides [currentSlide].height);
			timeSinceLast = 0.0f;
			currentSlide++;
		}
		
		timeSinceLast += Time.deltaTime;
	}
}
