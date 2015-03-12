using UnityEngine;
using System.Collections;

public class PlayerSetup : MonoBehaviour {
	// Shot properties
	public float angle;
	public float velocity;
	
	// Bullet properties
	public GameObject bullet;
	private Vector2 bulletTransform;
	
	// Other variables
	private GameObject player;
	public GUISkin skin;
	public Sprite defeated;
	private int playerTurn;
	private float GUIangle;
	private float GUIvelocity;
	
	void Start () {
		//Sets the player's position and sets the angle and the velocity to the default
		Positioning (transform.name);
	}
	
	void Update () {
		// Checks if it's player 1 turn and sets the controls
		// W - Increase angle; S - Decrease angle; D - Increase velocity; A - Decrease velocity
		playerTurn = GameSetup.playerTurn;
		player = playerTurn == 1 ? GameObject.FindGameObjectWithTag("Player 1") : GameObject.FindGameObjectWithTag("Player 2");
	
		if (Input.GetKey(KeyCode.D)) {
			player.GetComponent<PlayerSetup>().velocity += 1.0f;
		}
		
		if (Input.GetKey(KeyCode.A)) {
			player.GetComponent<PlayerSetup>().velocity -= 1.0f;
			
			if (player.GetComponent<PlayerSetup>().velocity < 1.0f) {
				player.GetComponent<PlayerSetup>().velocity = 1.0f;
			}
		}
		
		if (Input.GetKey(KeyCode.W)) {
			player.GetComponent<PlayerSetup>().angle += 0.1f;
			
			if (player.GetComponent<PlayerSetup>().angle > 90.0f)
				player.GetComponent<PlayerSetup>().angle = 90.0f;
		}
		
		if (Input.GetKey(KeyCode.S)) {
			player.GetComponent<PlayerSetup>().angle -= 0.1f;
			
			if (player.GetComponent<PlayerSetup>().angle < 0.01f)
				player.GetComponent<PlayerSetup>().angle = 0.01f;
		}
		
		if (Input.GetKeyDown(KeyCode.Space)) {
			// Checks which bullet is it to instantiate it on the right position 
			if (Main_Menu.level == 1 || Main_Menu.level == 3) {
				bulletTransform = new Vector2(player.transform.position.x, player.transform.position.y + 0.8f);
				
				if (GameObject.FindWithTag("Bullet") == null) {
					Instantiate(bullet, bulletTransform, player.transform.rotation);
				}
			}
			else if (Main_Menu.level == 2) {
				bulletTransform = new Vector2(player.transform.position.x, player.transform.position.y + 1.1f);
				
				if (GameObject.FindWithTag("Bullet") == null) {
					Instantiate(bullet, bulletTransform, player.transform.rotation);
				}
			}
		}
	}
	
	void OnGUI () {
		GUI.skin = skin;
		
		if (playerTurn == 1) {
			GUIangle = GameObject.FindGameObjectWithTag("Player 1").GetComponent<PlayerSetup>().angle;
			GUIvelocity = GameObject.FindGameObjectWithTag("Player 1").GetComponent<PlayerSetup>().velocity;
			GUI.Label (new Rect (Screen.width * 0.06f, Screen.height * 0.86f, 100, 100), "Angle: " + GUIangle.ToString ("F") + "°");
			GUI.Label (new Rect (Screen.width * 0.06f, Screen.height * 0.91f, 150, 100), "Velocity: " + (GUIvelocity * Time.deltaTime).ToString ("F") + "m/s");
		}
		if (playerTurn == 2) {
			GUIangle = GameObject.FindGameObjectWithTag("Player 2").GetComponent<PlayerSetup>().angle;
			GUIvelocity = GameObject.FindGameObjectWithTag("Player 2").GetComponent<PlayerSetup>().velocity;
			GUI.Label (new Rect (Screen.width * 0.82f, Screen.height * 0.86f, 100, 100), "Angle: " + GUIangle.ToString ("F") + "°");
			GUI.Label (new Rect (Screen.width * 0.82f, Screen.height * 0.91f, 150, 100), "Velocity: " + (GUIvelocity * Time.deltaTime).ToString ("F") + "m/s");
		}
	}
	
	void OnCollisionEnter2D (Collision2D colInfo) {
		// If player get's hit, change it's sprite to defeated
		if (colInfo.collider.tag == "Bullet") {
			gameObject.GetComponent<SpriteRenderer>().sprite = defeated;
		}
	}

	void Positioning (string name) {
		if (name == "Player 1") {
			transform.position = Camera.main.ScreenToWorldPoint (new Vector3 (Screen.width * 0.1155f, Screen.height * 0.1765f, 30f));
		} else {
			transform.position = Camera.main.ScreenToWorldPoint (new Vector3 (Screen.width * 0.8990f, Screen.height * 0.1765f, 30f));
		}
		GameObject.FindGameObjectWithTag("Player 1").GetComponent<PlayerSetup>().angle = 45.0f;
		GameObject.FindGameObjectWithTag("Player 1").GetComponent<PlayerSetup>().velocity = 500.0f;
		GameObject.FindGameObjectWithTag("Player 2").GetComponent<PlayerSetup>().angle = 45.0f;
		GameObject.FindGameObjectWithTag("Player 2").GetComponent<PlayerSetup>().velocity = 500.0f;
	}
}
