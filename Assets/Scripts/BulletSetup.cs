using UnityEngine;
using System.Collections;

public class BulletSetup : MonoBehaviour {

	private Vector2 velocity;
	private Vector2 wind;
	private Vector2 airDrag;

	private float velAngle;
	private int playerTurn;

	public Sprite[] projectiles = new Sprite[Main_Menu.nLevels];
	
	void Start () {
		// Checks whose turn is it
		playerTurn = GameSetup.playerTurn;

		// Gets the wind vector components
		wind.x = GameSetup.wind.x;
		wind.y = GameSetup.wind.y;

		if (playerTurn == 1) {
			// Gets the bullet's velocity and angle from player 1
			velocity.x = GameObject.FindGameObjectWithTag("Player 1").GetComponent<PlayerSetup>().velocity;
			velAngle = GameObject.FindGameObjectWithTag("Player 1").GetComponent<PlayerSetup>().angle * Mathf.Deg2Rad;
			velocity.y = Mathf.Tan (velAngle) * velocity.x;

			// Creates an air drag vector
			airDrag.x = -velocity.x / 50.0f;
			airDrag.y = -velocity.y / 50.0f;

			GetComponent<Rigidbody2D>().AddForce(velocity + wind + airDrag);

		}

		if (playerTurn == 2) {
			// Gets the bullet's velocity and angle from player 2
			velocity.x = -GameObject.FindGameObjectWithTag("Player 2").GetComponent<PlayerSetup>().velocity;
			velAngle = GameObject.FindGameObjectWithTag("Player 2").GetComponent<PlayerSetup>().angle * Mathf.Deg2Rad;
			velocity.y = Mathf.Tan (velAngle) * -velocity.x;

			// Creates an air drag vector
			airDrag.x = -velocity.x / 50.0f;
			airDrag.y = -velocity.y / 50.0f;
			
			GetComponent<Rigidbody2D>().AddForce(velocity + wind + airDrag);
		}

	}

	void OnCollisionEnter2D (Collision2D colInfo) {
		// If bullet collides with a player, check which one was and return to GameSetup.cs the winner
		if (colInfo.gameObject.tag == "Player 1") {
			DestroyObject(gameObject);
			GameSetup.playerTurn = 0;

		} 
		else if (colInfo.gameObject.tag == "Player 2") {
			DestroyObject(gameObject);
			GameSetup.playerTurn = -1;

		} else {
			// Destroy the bullet and change whose turn is it
			DestroyObject(gameObject);
			GameSetup.playerTurn = playerTurn == 1 ? 2 : 1;

		}
	}
}
