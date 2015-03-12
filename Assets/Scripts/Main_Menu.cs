using UnityEngine;
using System.Collections;

public class Main_Menu : MonoBehaviour {

	static public int nLevels = 3;
	static public int level;
	public GUISkin skin;
	public Texture2D background;
	private Rect windowRect = new Rect(Screen.width/2.8f, Screen.height/4, 200, 120);

	void LevelWindow(int windowID) {
		switch(level) {
			case 1:
				GUI.Label (new Rect (10, 20, 200, 200), "\t   Projectile: Bowling ball\n\t\t  Weight: Medium\n\t\t    Wind: 2.49m/s" + 
			           "\n\n\t    B - Back\t   P - Play");

				if (Input.GetKey(KeyCode.P))
					Application.LoadLevel("PlayScene");

				if (Input.GetKey(KeyCode.B))
					level = 0;

				break;
			
			case 2:
				GUI.Label (new Rect (10, 20, 200, 200), "\t\t Projectile: Asteroid\n\t\t Weight: Very Heavy\n\t\t    Wind: 3.31m/s" +
			           "\n\n\t    B - Back\t   P - Play");
			
				if (Input.GetKey(KeyCode.P))
					Application.LoadLevel("PlayScene");
				
				if (Input.GetKey(KeyCode.B))
					level = 0;

				break;
			
			case 3:
				GUI.Label (new Rect (10, 20, 200, 200), "\t\tProjectile: Beach ball\n\t\t  Weight: Very light\n\t\t    Wind: 0.83m/s" + 
			           "\n\n\t    B - Back\t   P - Play");
			
				if (Input.GetKey(KeyCode.P))
					Application.LoadLevel("PlayScene");
				
				if (Input.GetKey(KeyCode.B))
					level = 0;

				break;

			default:
				break;
		}
	}

	void OnGUI () {
		GUI.skin = skin;
		GUI.DrawTexture (new Rect(0, 0, Screen.width, Screen.height), background);

		if (GUI.Button (new Rect (Screen.width/2.51f, Screen.height/2.56f, Screen.width/5.27f, Screen.height/4.39f), "Instructions")) {
			Application.LoadLevel("Instructions");
			
		}
		//Bowling ball
		if (GUI.Button (new Rect (Screen.width/5.51f, Screen.height/9.06f, Screen.width/5.27f, Screen.height/4.39f), ""))
			level = 1;

		//Asteroid ball
		if (GUI.Button (new Rect (Screen.width/1.56f, Screen.height/4.53f, Screen.width/5.27f, Screen.height/4.39f), ""))
			level = 2;

		//Plastic ball
		if (GUI.Button (new Rect (Screen.width/4.97f, Screen.height/1.53f, Screen.width/5.27f, Screen.height/4.39f), ""))
			level = 3;

		if (GUI.Button (new Rect (Screen.width/1.52f, Screen.height/1.51f, Screen.width/5.27f, Screen.height/4.39f), "Quit"))
			Application.Quit();

		if (level != 0)
			windowRect = GUI.Window(0, windowRect, LevelWindow, "Level Select");

	}
}
