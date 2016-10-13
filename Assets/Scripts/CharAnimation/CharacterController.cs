using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CharacterController : MonoBehaviour
{

	enum PlayerModes { Idle, Moving, Reverse };

	public float maxWalkSpeed;
	public float maxRunSpeed;
	public float maxReverseSpeed;
	public float jumpForce;
	public float delay;

	private float maxSpeed;
    private Rigidbody rb;
	private Animator anim;
	private bool canJump = true;
	private float currentSpeed = 0.0f;
	private Vector3 prevLoc; 
	private Vector3 currLoc;
	private PlayerModes currentMode; 		// Player's current state

    void Start()
    {
		maxSpeed = maxWalkSpeed;
		currentMode = PlayerModes.Idle;
        rb = GetComponent<Rigidbody>();
		anim = GetComponent<Animator> ();
		currLoc = transform.position;
		prevLoc = currLoc;
    }

	void Update()
	{
		prevLoc = currLoc;
		currLoc = transform.position;

		switch (currentMode) {		
		case PlayerModes.Idle:
			if (Input.GetKeyDown (KeyCode.W)) {
				currentMode = PlayerModes.Moving;	
			}

			if (Input.GetKeyDown (KeyCode.S)) {
				currentMode = PlayerModes.Reverse;
			}
			break;
		case PlayerModes.Moving:
			if (Input.GetKeyDown (KeyCode.S) || Input.GetKeyUp (KeyCode.W)) {
				currentMode = PlayerModes.Idle;
			}
			break;
		case PlayerModes.Reverse:
			if (!Input.GetKey (KeyCode.S)) {
				currentMode = PlayerModes.Idle;
			}
			break;
		}

		float horizontal = Input.GetAxis ("Horizontal");

		switch (currentMode) {
		case PlayerModes.Idle:
			anim.SetBool ("isMoving", false);
			currentSpeed = Mathf.Lerp (currentSpeed, 0.0f, Time.deltaTime);
			break;
		
		case PlayerModes.Moving: 
			anim.SetBool ("isMoving", true);
			currentSpeed = Mathf.Lerp (currentSpeed, maxSpeed, Time.deltaTime);
			break;

		case PlayerModes.Reverse:
			anim.SetBool ("isMoving", true);
			currentSpeed = Mathf.Lerp(currentSpeed, maxReverseSpeed, Time.deltaTime);
			break;
		}

		// Change to running if holding left shift
		if (Input.GetKeyDown (KeyCode.LeftShift)) {
			maxSpeed = maxRunSpeed;
		} else if (Input.GetKeyUp(KeyCode.LeftShift)) {
			maxSpeed = maxWalkSpeed;
		}

		if (horizontal != 0) {
			anim.SetBool ("isTurning", true);
		} else if (Mathf.Approximately (horizontal, 0.0f)) {
			anim.SetBool ("isTurning", false);
		}
			
		transform.Rotate (0.0f, horizontal, 0.0f, Space.Self); 		// Rotate player

		anim.SetFloat ("VelocityX", horizontal);
		anim.SetFloat ("VelocityZ", currentSpeed);

		// Jump
		if (Input.GetKeyDown (KeyCode.Space) && canJump) {											
			canJump = false;
			anim.SetBool ("Jump", true);
			rb.AddForce (new Vector3 (0.0f, jumpForce, 0.0f));
		}			
		
	}
	// Reset canJump
	void OnCollisionStay(Collision other) {		
		anim.SetBool ("Jump", false);
		if (other.gameObject.tag == "Ground") { 
			canJump = true;
		}
	}
		
	IEnumerator Wait() {
		yield return new WaitForSeconds (delay);
	}
}

