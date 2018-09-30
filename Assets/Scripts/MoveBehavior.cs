using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBehavior : MonoBehaviour {

public float moveSpeed = 10f;
	public GameObject testcube;

	public float lookSpeed = 300f;

	private Vector3 inputVector; 
	
	public float gravityScale = 1.0f;
 
	public static float globalGravity = -9.81f;
 
	Rigidbody rBody;
 
	void OnEnable ()
	{
		rBody = GetComponent<Rigidbody>();
		rBody.useGravity = false;
	}

	
	void Update () {
		
		float mouseX = Input.GetAxis("Mouse X") * lookSpeed * Time.deltaTime; 
		float mouseY = Input.GetAxis("Mouse Y") * lookSpeed * Time.deltaTime; 

		Camera.main.transform.localEulerAngles -= new Vector3(0, 0, Camera.main.transform.localEulerAngles.z);
		
		transform.Rotate(0f, mouseX, 0f);
		Camera.main.transform.localEulerAngles += new Vector3(-mouseY, 0f, 0f); 
		
		float vertical = Input.GetAxis("Vertical"); 
		float horizontal = Input.GetAxis("Horizontal");
	
		
		inputVector = transform.forward * vertical * moveSpeed; 
		inputVector += transform.right * horizontal * moveSpeed; 
	}

	void FixedUpdate()
	{
		GetComponent<Rigidbody>().velocity = inputVector;
		Vector3 gravity = globalGravity * gravityScale * Vector3.up;
		rBody.AddForce(gravity, ForceMode.Acceleration);
	}

	void OnCollisionEnter(Collision col)
	{
		if (col.gameObject.tag == "cube")
		{
			Destroy(testcube);
		}
	}
}

