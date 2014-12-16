using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
	public KeyCode restartKey = KeyCode.Tab;

	void Start () {
	
	}

	void Update () {
		// Restart scene
		if (Input.GetKeyDown (restartKey)) {
			// Destroy ThalmicHub object to prevent duplicate instances
			Destroy(ThalmicHub.instance);

			Application.LoadLevel(Application.loadedLevel);
		}
	}
}
