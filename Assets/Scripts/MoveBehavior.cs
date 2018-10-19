using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MoveBehavior : MonoBehaviour
{
	public AudioSource yell;
	public AudioSource help;
public float moveSpeed = 10f;
	public GameObject testcube;
	public GameObject testcube2;
	public GameObject testcube3;
	private int countObjects = 0;
	public GameObject futureCube;
	public GameObject futureCube2;
	public GameObject futureCube3;
	public GameObject futureCube4;
	public GameObject drowner;
	private float timer = 0f;
	public Text countText;
	public float lookSpeed = 300f;
	private int making = 0;
	public Text helpMe;
	private float timeToYell = 20f;
	public Text badkidText;
	public Text pressY;
	public Text overallTime;
	private float timeToYell2 = 1000f;
	public Text intro; 

	private Vector3 inputVector; 
	
	public float gravityScale = 1.0f;

	private float upDownRotation;
	public static float globalGravity = -9.81f;
 
	Rigidbody rBody;

	void Start()
	{
		//help.Stop();
		helpMe.text = "";
		pressY.text = "Press Y to Yell at Kids";
		//yell = GetComponent<AudioSource>();
	}
 
	void OnEnable ()
	{
		rBody = GetComponent<Rigidbody>();
		rBody.useGravity = false;
	}

	
	void Update ()
	{
		if (Input.anyKeyDown)
		{
			intro.text = "";
		}
		timer += Time.deltaTime;
		overallTime.text = "Overall Time: " + Mathf.RoundToInt(timer) + " Seconds";

		if (Input.GetKeyDown("space"))
		{
			Debug.Log("jump");
			gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up,ForceMode.Impulse);

		}
		
		timeToYell -= Time.deltaTime;
		timeToYell2 -= Time.deltaTime;
		if (countObjects != 7)
		{
			badkidText.text = "Time Until You Should Yell At Kids: " + Mathf.RoundToInt(timeToYell) + " Seconds";
			countText.text = "Objects Collected: " + countObjects;

		}

		if (countObjects == 7)
		{
			badkidText.text = "Time Until Kid Will Drown: " + Mathf.RoundToInt(timeToYell2) + " Seconds!";

		}
		
		
		if (timeToYell < 0f )
		{
			SceneManager.LoadScene(1);
		}
		
		
		if (timeToYell2 < 0f )
		{
			SceneManager.LoadScene(1);
		}

		if (countObjects != 7 && Input.GetKey(KeyCode.Y))
		{
			timeToYell = 20f;
			yell.Play();
			
		}
	
		timer += Time.deltaTime;
		float mouseX = Input.GetAxis("Mouse X") * lookSpeed * Time.deltaTime; 
		float mouseY = Input.GetAxis("Mouse Y") * lookSpeed * Time.deltaTime;

		upDownRotation -= mouseY;
		upDownRotation = Mathf.Clamp(upDownRotation, -80, 80);

		Camera.main.transform.localEulerAngles = new Vector3(upDownRotation, 0f, 0f);
		
		transform.Rotate(0f, mouseX, 0f);
	//	Camera.main.transform.localEulerAngles += new Vector3(-mouseY, 0f, 0f); 
		
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

		if (newTime == 20 && making == 1)
		{
			futureCube2.SetActive(true);
			making++;
		}

		if (newTime == 25 && making == 2)
		{
			futureCube3.SetActive(true);
			making++;
		}

		if (newTime == 30 && making == 3)
		{
			futureCube4.SetActive(true);
			making++;
		}

		if (making == 4 && countObjects == 7)
		{
			help.Play();
			timeToYell2 = 10f;
			drowner.SetActive(true);
			helpMe.text = "There's a child drowning! Press 'Space' to jump in and save them!";
			countText.text = " ";
			pressY.text = " ";
			making++;


		}
		
		
		//if ( Input.GetKeyDown(KeyCode.Space))
		//{
		//	Debug.Log("space");
		//	transform.Translate(0f, 1.5f, 0f);
		//}
		
		Ray myRay = new Ray(transform.position, Vector3.down);
		
		//Step 2: Define the distance of the raycast
		float myRaycastMaxDist = 1.6f; //a little bit longer than half of the height.
		
		//Step 3: (optional) visualize the raycast
		Debug.DrawRay(myRay.origin, myRay.direction * myRaycastMaxDist, Color.magenta);
		
		//Step 4: let's finally cast some raycasts!
		//raycasts return true or false, so ideal for if statements
		if (Physics.Raycast(myRay, myRaycastMaxDist))
		{
			//if true (if raycast hit collider)
			Debug.Log("grounded is TRUE");
			if ( Input.GetKeyDown(KeyCode.Space))
			{
				Debug.Log("space");
				transform.Translate(0f, 2.5f, 0f);
			}
		}

		else
		{
			//if false (if raycast did not hit the floor
			transform.Rotate(0f,0f,0f);
			
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
			intro.text = "You picked up the pool noodle that was left behind!";
			Destroy(testcube);
			countObjects++;
		}

		if (col.gameObject.tag == "cube2")
		{
			intro.text = "You picked up the ice cream someone dropped!";
			Destroy(testcube2);
			countObjects++;
		}
		
		if (col.gameObject.tag == "cube3")
		{
			intro.text = "You picked up the discarded towel!";
			Destroy(testcube3);
			countObjects++;
		}

		if (col.gameObject.tag == "cube4")
		{
			intro.text = "You picked up the fins! Good thing no one tripped over them...";
			Destroy(futureCube);
			countObjects++;
		}
		
		if (col.gameObject.tag == "cube5")
		{
			intro.text = "You picked up the inflatable tube! Hope no one forgot this.";
			Destroy(futureCube2);
			countObjects++;
		}

		if (col.gameObject.tag == "cube6")
		{
			intro.text = "You picked up the sunglasses! You might as well keep them.";
			Destroy(futureCube3);
			countObjects++;
		}

		if (col.gameObject.tag == "cube7")
		{
			intro.text = "Ew! You found some poop. Better clean it up before your boss sees.";
			Destroy(futureCube4);
			countObjects++;
		}
		
		if (col.gameObject.tag == "drowner")
		{
			SceneManager.LoadScene(2);
		}
		if (col.gameObject.tag == "water" && countObjects != 7)
		{
		
			intro.text = "Don't jump in the pool on duty unless someone is drowning!\nThe more you jump in, the slower you go!";
			moveSpeed -= 1;
		}

		if (col.gameObject.tag == "person1")
		{
			intro.text = "Hi there!";
		}

		if (col.gameObject.tag == "person2")
		{
			intro.text = "Can you get off of me? This feels inappropriate";
		}

		if (col.gameObject.tag == "person3")
		{
			intro.text = "Go away! You can't play with us!";
		}

		if (col.gameObject.tag == "person4")
		{
			intro.text = "Thank you for keeping my kids safe!";
		}

		if (col.gameObject.tag == "person5")
		{
			intro.text = "*swimming*";
		}
	}


}

