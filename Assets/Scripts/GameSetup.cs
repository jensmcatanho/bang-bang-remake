using UnityEngine;
using System.Collections;

public class GameSetup : MonoBehaviour {	
	// Wind properties
	static public Vector2 wind;
	public Vector2 xWind;
	public float windAngle;

	// Boundaries
	public BoxCollider2D topWall;
	public BoxCollider2D leftWall;
	public BoxCollider2D rightWall;
	
	// Victory window
	private Rect windowRect = new Rect(Screen.width/2.8f, Screen.height/4, 200, 130);

	// Other variables
	static public int playerTurn;
	public GameObject projectile;
	public Sprite restartPlayer1;
	public Sprite restartPlayer2;
	public GUISkin skin;
	public Transform windArrow;

	void Start () {
		switch (Main_Menu.level) {
			case 1:
				Wind (150, 200);
				projectile.GetComponent<Rigidbody2D>().mass = 1f;
				projectile.GetComponent<SpriteRenderer>().sprite = projectile.GetComponent<BulletSetup>().projectiles[0];
				projectile.transform.localScale = new Vector3(1f, 1f, 1f);
				projectile.GetComponent<CircleCollider2D>().radius = 0.35f;
				break;
			case 2:
				Wind (-200, -150);
				projectile.GetComponent<Rigidbody2D>().mass = 2.5f;
				projectile.GetComponent<SpriteRenderer>().sprite = projectile.GetComponent<BulletSetup>().projectiles[1];
				projectile.transform.localScale = new Vector3(0.5814269f, 0.5814268f, 0.5814268f);
				projectile.GetComponent<CircleCollider2D>().radius = 1.23f;
				break;
			case 3:
				Wind (-50, 100);
				projectile.GetComponent<Rigidbody2D>().mass = 0.5f;
				projectile.GetComponent<SpriteRenderer>().sprite = projectile.GetComponent<BulletSetup>().projectiles[2];
				projectile.transform.localScale = new Vector3(1f, 1f, 1f);
				projectile.GetComponent<CircleCollider2D>().radius = 0.35f;
				break;
			default:
				break;
		}

		// Sorting whose turn is it
		playerTurn = Random.Range (1, 3);

		// Boundaries positioning
		topWall.size = new Vector2 (Camera.main.ScreenToWorldPoint (new Vector3 (Screen.width * 2.0f, 0f, 0f)).x, 1f);
		topWall.offset = new Vector2 (0f, Camera.main.ScreenToWorldPoint (new Vector3 (0f, Screen.height, 0f)).y + 0.5f);

		leftWall.size = new Vector2 (1f, Camera.main.ScreenToWorldPoint (new Vector3 (0f, Screen.height * 2.0f, 0f)).y);;
		leftWall.offset = new Vector2 (Camera.main.ScreenToWorldPoint (new Vector3 (0f, 0f, 0f)).x - 0.5f, 0f);
		
		rightWall.size = new Vector2 (1f, Camera.main.ScreenToWorldPoint (new Vector3 (0f, Screen.height * 2f + 50, 0f)).y);
		rightWall.offset = new Vector2 (Camera.main.ScreenToWorldPoint (new Vector3 (Screen.width, 0f, 0f)).x + 0.5f, 0f);


	}

	void Update () {
		// Checks if someone won
		if (playerTurn <= 0) {
			// Restart the level
			if (Input.GetKey(KeyCode.R)) {	
				// Resets the players stats
				playerTurn = Random.Range (1, 3);

				GameObject.FindGameObjectWithTag("Player 1").GetComponent<SpriteRenderer>().sprite = restartPlayer1;
				GameObject.FindGameObjectWithTag("Player 1").GetComponent<PlayerSetup>().angle = 45.0f;
				GameObject.FindGameObjectWithTag("Player 1").GetComponent<PlayerSetup>().velocity = 500.0f;
				
				GameObject.FindGameObjectWithTag("Player 2").GetComponent<SpriteRenderer>().sprite = restartPlayer2;
				GameObject.FindGameObjectWithTag("Player 2").GetComponent<PlayerSetup>().velocity = 500.0f;
				GameObject.FindGameObjectWithTag("Player 1").GetComponent<PlayerSetup>().angle = 45.0f;

			}
		}

		// Goes to the menu
		if (Input.GetKey(KeyCode.M)) {
			Application.LoadLevel("Main_Menu");
			Main_Menu.level = 0;
		}
	}

	void VictoryWindow(int windowID) {
		if (playerTurn == 0) {
			GUI.Label (new Rect (10, 20, 200, 200), "...And thus the battle ends!!\nThe Right team is victorious!\nTo the victor goes the spoils.\n\n" +
				"\t    R - Restart Level\n\t    M - Menu");

		} else {
			GUI.Label (new Rect (10, 20, 200, 200), "...And thus the battle ends!!\nThe Left team is victorious!\nTo the victor goes the spoils.\n\n" +
				"\t    R - Restart Level\n\t    M - Menu");

		}
	}

	void OnGUI () {
		GUI.skin = skin;
		if (playerTurn <= 0) {
			windowRect = GUI.Window(0, windowRect, VictoryWindow, "Victory!");
		}
	}

	void Wind (int xComponent, int yComponent) {
		// Wind Properties
		wind = new Vector2(xComponent, yComponent);
		xWind = new Vector2 (xComponent, 0);
		windAngle = Mathf.Acos(Vector2.Dot (wind, xWind) / (wind.magnitude * xWind.magnitude)) * Mathf.Rad2Deg; //angle in degrees between wind and x axis
		
		// Wind arrow positioning
		if (wind.x > 0 && wind.y > 0) {
			// 1º quadrante
			Instantiate ( windArrow, new Vector3 (0, 4, 0), Quaternion.Euler ( new Vector3 (0, 0, windAngle) ) );
		}
		else if (wind.x < 0 && wind.y > 0) {
			// 2º quadrante
			Instantiate ( windArrow, new Vector3 (0, 4, 0), Quaternion.Euler ( new Vector3 (0, 0, windAngle + 90.0f) ) );
		}
		else if (wind.x < 0 && wind.y < 0) {
			// 3º quadrante
			Instantiate ( windArrow, new Vector3 (0, 4, 0), Quaternion.Euler ( new Vector3 (0, 0, windAngle + 180.0f) ) );
		}
		else {
			// 4º quadrante
			Instantiate ( windArrow, new Vector3 (0, 4, 0), Quaternion.Euler ( new Vector3 (0, 0, windAngle + 270.0f) ) );
		}
	}
}
