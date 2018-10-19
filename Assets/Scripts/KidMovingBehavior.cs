using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KidMovingBehavior : MonoBehaviour {


	// Update is called once per frame
	void Update () {
		Ray myRay = new Ray(transform.position, transform.forward);
		
		//step 2: define a distance
		float myMaxDis = 0.7f;
		
		//step 3: visualize the raycast (optional)
		Debug.DrawRay(myRay.origin, myRay.direction * myMaxDis, Color.green);
		
		//step 4: actually make ray!
		if (Physics.Raycast(myRay, myMaxDis))
		{
			//if it returns true, there's a wall so we should turn to avoid it
			int randomNumber = Random.Range(0, 100);
			//if less than 50, turn left; otherwise, turn right
			if (randomNumber < 50)
			{
				transform.Rotate(0f, -90f, 0f);
			}
			else
			{
				transform.Rotate(0f, 90f,0f);
			}

		}

		else
		{
			//go forward if raycast is False
			transform.Translate(0f,0f,Time.deltaTime);
		}
	}
}
