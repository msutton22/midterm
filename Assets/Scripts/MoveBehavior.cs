using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveBehavior : MonoBehaviour {

public float moveSpeed = 10f;
	public GameObject testcube;
	public GameObject testcube2;
	public GameObject testcube3;
	private int countObjects = 0;
	public GameObject futureCube;
	public GameObject futureCube2;
	private float timer = 0f;
	public Text countText;
	public float lookSpeed = 300f;
	private int making = 0;

	private Vector3 inputVector; 
	
	public float gravityScale = 1.0f;
 
	public static float globalGravity = -9.81f;
 
	Rigidbody rBody;
 
	void OnEnable ()
	{
		rBody = GetComponent<Rigidbody>();
		rBody.useGravity = false;
	}

	
	void Update ()
	{

		timer += Time.deltaTime;
		float mouseX = Input.GetAxis("Mouse X") * lookSpeed * Time.deltaTime; 
		float mouseY = Input.GetAxis("Mouse Y") * lookSpeed * Time.deltaTime; 

		Camera.main.transform.localEulerAngles -= new Vector3(0, 0, Camera.main.transform.localEulerAngles.z);
		
		transform.Rotate(0f, mouseX, 0f);
		Camera.main.transform.localEulerAngles += new Vector3(-mouseY, 0f, 0f); 
		
		float vertical = Input.GetAxis("Vertical"); 
		float horizontal = Input.GetAxis("Horizontal");
	
		
		inputVector = transform.forward * vertical * moveSpeed; 
		inputVector += transform.right * horizontal * moveSpeed;

		countText.text = "Objects Collected: " + countObjects;

		int newTime = Mathf.RoundToInt(timer);

		if (newTime == 15 && making == 0)
		{
			futureCube.SetActive(true);
			making++;

		}

		if (newTime == 25 && making == 1)
		{
			futureCube2.SetActive(true);
			making++;
		}
		
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
			countObjects++;
		}

		if (col.gameObject.tag == "cube2")
		{
			Destroy(testcube2);
			countObjects++;
		}
		
		if (col.gameObject.tag == "cube3")
		{
			Destroy(testcube3);
			countObjects++;
		}

		if (col.gameObject.tag == "cube4")
		{
			Destroy(futureCube);
			countObjects++;
		}
		
		if (col.gameObject.tag == "cube5")
		{
			Destroy(futureCube2);
			countObjects++;
		}
	}
}

