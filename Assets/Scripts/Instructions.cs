using UnityEngine;
using System.Collections;

public class Instructions : MonoBehaviour {

	public Texture2D background;
	
	void OnGUI () {
		GUI.DrawTexture (new Rect(0, 0, Screen.width, Screen.height), background);
	}

	void Update () {
		if (Input.GetKey (KeyCode.M)) {
			Application.LoadLevel("Main_Menu");
		}
	}
}
