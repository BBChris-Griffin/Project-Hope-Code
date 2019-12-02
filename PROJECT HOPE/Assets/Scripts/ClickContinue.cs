using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickContinue : MonoBehaviour {

	public void Continue(int time)
	{
		Time.timeScale = time;
	}
}
