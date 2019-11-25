using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneOnClick : MonoBehaviour {

	private GameObject penny;
	private GameObject camera;

	void Awake()
	{
		penny = GameObject.FindGameObjectWithTag ("Player").gameObject;
		camera = GameObject.FindGameObjectWithTag ("MainCamera").gameObject;
	}
	public void LoadByIndex(int sceneIndex)
	{
		if (penny != null) {
			Destroy (penny);
			Destroy (camera);
		}
		SceneManager.LoadScene (sceneIndex);
	}
		
}
