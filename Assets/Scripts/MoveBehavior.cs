using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBehavior : MonoBehaviour {

public float moveSpeed = 10f;

	public float lookSpeed = 300f;

	private Vector3 inputVector; // pass data from update to fixed update
	
	public float gravityScale = 1.0f;
 
	// Global Gravity doesn't appear in the inspector. Modify it here in the code
	// (or via scripting) to define a different default gravity for all objects.
 
	public static float globalGravity = -9.81f;
 
	Rigidbody m_rb;
 
	void OnEnable ()
	{
		m_rb = GetComponent<Rigidbody>();
		m_rb.useGravity = false;
	}

	// Update is called once per frame
	void Update () {
		//mouselook
		//delta = difference, not position
		float mouseX = Input.GetAxis("Mouse X") * lookSpeed * Time.deltaTime; //horizontal mouseDelta
		float mouseY = Input.GetAxis("Mouse Y") * lookSpeed * Time.deltaTime; //vertical mouseDelta
		//Camera.main.transform.Rotate(-mouseY,mouseX,0); //mouseY is negative because Y is naturally inverted so negative fixes it
		
		//have to unroll the camera
		Camera.main.transform.localEulerAngles -= new Vector3(0,0,Camera.main.transform.localEulerAngles.z);
		
		
		//better mouselook!
		//rotate capsule left and right, but rotate camera up and down
		
		transform.Rotate(0f, mouseX, 0f); //capsule rotation
		Camera.main.transform.localEulerAngles += new Vector3(-mouseY, 0f, 0f); //camera rotation
		
		//player movement
		float vertical = Input.GetAxis("Vertical"); // W and S or Up and Down
		float horizontal = Input.GetAxis("Horizontal");
		//move transform based on keyboard values
		//vertical (forward) movement 
		//transform.position += transform.forward * vertical * moveSpeed * Time.deltaTime;
		//transform.position += transform.right * horizontal * moveSpeed * Time.deltaTime;
		
		//multiplied by time to make it "framerate Independent"

		//the simplest method is bad because we are moving transform directly
		// when you move transform directly, you're basically teleporting it, no collision detection
		//a better method: move using RigidBody forces in FixedUpdate(), which won't have same issues

		inputVector = transform.forward * vertical * moveSpeed; // forward and backwards
		inputVector += transform.right * horizontal * moveSpeed; //left right
	}
	//runs all the time, every physics frame 
	//All physics code should go here!
	void FixedUpdate()
	{
		//apply forces
		GetComponent<Rigidbody>().velocity = inputVector; // no need for Time.deltaTime, already fixed framerate
		Vector3 gravity = globalGravity * gravityScale * Vector3.up;
		m_rb.AddForce(gravity, ForceMode.Acceleration);
	}
}

