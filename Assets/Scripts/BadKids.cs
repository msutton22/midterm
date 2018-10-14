using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BadKids : MonoBehaviour
{
	private float timeToYell = 20f;
	public Text badkidText;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		timeToYell -= Time.deltaTime;
		badkidText.text = "Time Until You Should Yell At Kids: " + Mathf.RoundToInt(timeToYell) + " Seconds";
		
		if (timeToYell < 0f)
		{
			SceneManager.LoadScene(1);
		}

		if (Input.GetKey(KeyCode.Y))
		{
			timeToYell = 20f;
		}
	}
	
	
}
