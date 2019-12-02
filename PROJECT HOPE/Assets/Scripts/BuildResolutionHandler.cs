using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class BuildResolutionHandler : MonoBehaviour {

	private Resolution game;
	private float pixelsPerUnit = 64f;

	// Use this for initialization
	void Start ()
	{
		game = Screen.resolutions[0];
		CalculateSize();
	}

	public void CalculateSize()
	{
		Camera.main.aspect = game.width / game.height;
		Camera.main.orthographicSize = game.width / ((( game.width / game.height ) * 1f) * pixelsPerUnit);
	}
}
