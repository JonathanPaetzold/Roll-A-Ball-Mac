using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



/// <summary>
/// Player controller is a script that allows you as the player to manipulate the
/// player object's movement as well as let the player game object interact with the environment
/// </summary>
public class PlayerController : MonoBehaviour {

	// Setting up the nessecary variables for the script
	private Rigidbody rb;
	private GameObject su;
	private GameObject sd;
	private GameObject ju;
	private GameObject jd;
	private GameObject ewm;
	private GameObject wwm;
	private int count;
	private float speed;
	private float jumpPower; 
	private float time;
	private float timeHelp;
	private float deaths;

	public Text score;
	public Text winText;
	public Text timer;
	public Text speedText;
	public Text jumpText;
	public Text deathText;

	// void -> void
	// the start function is called at the beginnign of the game and handles
	// much of the setup and intializing
	void Start () {
		rb = GetComponent<Rigidbody>();
		su = GameObject.FindGameObjectWithTag ("SpeedUp");
		sd = GameObject.FindGameObjectWithTag ("SlowDown");
		ju = GameObject.FindGameObjectWithTag ("Jump");
		jd = GameObject.FindGameObjectWithTag ("JumpDown");
		ewm = GameObject.FindGameObjectWithTag ("EastMiddle");
		wwm = GameObject.FindGameObjectWithTag ("WestMiddle");
		su.SetActive (false);
		sd.SetActive (false);
		ju.SetActive (false);
		jd.SetActive (false);
		wwm.SetActive (true);
		ewm.SetActive (true);
		deaths = 0;
		count = 0;
		speed = 10;
		jumpPower = 10;
		setCountText ();
		winText.text = "";
		setTimerText ();
		setJumpText ();
		setSpeedText ();
		time = 0;
		timeHelp = 0;

	}

	// void -> void
	// Function called every tick, used to calculate the timer, speed, and jump values etc.
	void Update() {
		setTimerText ();
		setJumpText ();
		setSpeedText ();
		RespawnCheck ();
	}

	// void -> void
	// Function used to correctly measure and display time onto the game screen
	void setTimerText() {
		if (timeHelp >= (1.0/ Time.deltaTime)) {
			time += 1;
			timer.text = "Time(s): " + time.ToString ();
			timeHelp = 0;
		}

		if (count < 30) {
			timeHelp += 1 ;

		} 

	}

	// void -> void 
	// Function used to correctly measure and display speed onto the game screen
	void setSpeedText() {
		speedText.text = "Speed: " + speed.ToString();
	}

	// void -> void 
	// Function used to correctly measure and display jump power onto the game screen
	void setJumpText() {
		jumpText.text = "Jump Power: " + jumpPower.ToString();
	}
		
	// void -> void
	// Fixed Update is called before calculating phsyics
	// This function takes directional input from the user and translate that 
	// input into forces that are then applied to the player game object
	void FixedUpdate () {
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");
		bool jump = Input.GetKeyDown ("space");


		if (jump && transform.position.y == 1) {
			Vector3 movement = new Vector3 (moveHorizontal, jumpPower, moveVertical);
			rb.AddForce (movement * speed);
		} 
		else {
			Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
			rb.AddForce (movement * speed);
		}

	}

	// void -> void
	// Checks to see if the player has fallen off the board and will respawn them at origin
	void RespawnCheck() {
		if (transform.position.y < 0) {
			transform.position = new Vector3 (0, 1, 0);
			deaths += 1;
			deathText.text = "Deaths: " + deaths.ToString();
		}
	}

	// Collider other -> void
	// This Function is called when the player collides with another Collider 
	// and then takes differnet steps depending on what object ot collides with
	void OnTriggerEnter (Collider other) {

		if (other.gameObject.CompareTag("PickUp")) {
			other.gameObject.SetActive(false);
			count += 1;
			setCountText ();
			spawnDespawn ();
		}

		if (other.gameObject.CompareTag ("SpeedUp")) {
			if (speed < 40) {
				speed += 10;
			}
		}

		if (other.gameObject.CompareTag ("SlowDown")) {
			if (speed >= 20) {
				speed += -10;
			}
		}

		if (other.gameObject.CompareTag ("Jump")) {
			if (jumpPower < 150 ) {
				jumpPower += 10;
			}
		}

		if (other.gameObject.CompareTag ("JumpDown")) {
			if (jumpPower > 10) {
				jumpPower += -10;
			}
		}
	}

	// void -> void 
	// Function used to correctly measure and display jump power onto the game screen
	void setCountText () {
		score.text = "Pieces Left: " + (30 - count).ToString();

		if (count >= 30) {
			winText.text = "You Win!";
		}
	}

	// void -> void 
	// Function used to activate and deactivate certain gameobjects when score criteria is meet
	void spawnDespawn () {
		if (count >= 5) {
			su.SetActive (true);
			sd.SetActive (true);
		}

		if (count >= 15) {
			wwm.SetActive (false);
			ewm.SetActive (false);
			ju.SetActive (true);
			jd.SetActive (true);

		}
	}

}
