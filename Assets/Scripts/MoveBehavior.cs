using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MoveBehavior : MonoBehaviour {

public float moveSpeed = 10f;
	public GameObject testcube;
	public GameObject testcube2;
	public GameObject testcube3;
	private int countObjects = 0;
	public GameObject futureCube;
	public GameObject futureCube2;
	public GameObject drowner;
	private float timer = 0f;
	public Text countText;
	public float lookSpeed = 300f;
	private int making = 0;
	public Text helpMe;
	private float timeToYell = 20f;
	public Text badkidText;
	public Text pressY;
	public float jumpForce = 2.0f;
	private Rigidbody rb;
	public Vector3 jump;

	private Vector3 inputVector; 
	
	public float gravityScale = 1.0f;
 
	public static float globalGravity = -9.81f;
 
	Rigidbody rBody;

	void Start()
	{
		rb = GetComponent<Rigidbody>();
		jump = new Vector3(0f, 2f, 0f);
		helpMe.text = "";
		pressY.text = "Press Y to Yell at Kids";
	}
 
	void OnEnable ()
	{
		rBody = GetComponent<Rigidbody>();
		rBody.useGravity = false;
	}

	
	void Update ()
	{
		if (Input.GetKeyDown("space"))
		{
			Debug.Log("jump");
			//rb.AddForce(jump * jumpForce, ForceMode.Impulse);
			//transform.Translate(Vector3.up * 260 * Time.deltaTime, Space.World);
			gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up,ForceMode.Impulse);

		}
		
		timeToYell -= Time.deltaTime;
		if (countObjects != 5)
		{
			badkidText.text = "Time Until You Should Yell At Kids: " + Mathf.RoundToInt(timeToYell) + " Seconds";
			countText.text = "Objects Collected: " + countObjects;

		}
		
		
		if (timeToYell < 0f)
		{
			SceneManager.LoadScene(1);
		}

		if (Input.GetKey(KeyCode.Y))
		{
			timeToYell = 20f;
		}
	
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

		if (making == 2 && countObjects == 5)
		{
			drowner.SetActive(true);
			helpMe.text = "There's a child drowning! Press 'Space' to save them!";
			badkidText.text = "  ";
			countText.text = " ";
			pressY.text = " ";
			timeToYell = 100f;
			making++;
			
		
		}
		
		if ( countObjects == 5 && Input.GetKeyDown(KeyCode.Space))
		{
			Debug.Log("space");
			transform.Translate(0f, 2f, 0f);
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

