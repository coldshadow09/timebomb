using UnityEngine;
using System.Collections;

public class IntegrationTestDashButton : MonoBehaviour {
	private DashButtonController dashbutton;
	private PlayerMovement player;
	// Use this for initialization
	void Start () {
		dashbutton = FindObjectOfType<DashButtonController> ();
		player = FindObjectOfType<PlayerMovement> ();
		pressDashButton();
		// check if player dashes when button is pressed
		if (!player.isDashing ()) {
			Debug.Log ("Player is not dashing incorrectly");
			IntegrationTest.Fail ();
		} else {
			IntegrationTest.Pass ();
		}
		releaseDashButton ();
		// check player does not dash when button released
		if (!player.isDashing ()) {
			Debug.Log ("player is not dashing correctly");
			IntegrationTest.Pass ();
		} else {
			IntegrationTest.Fail ();
		}
	}

	// Simulate pressing the dash button

	void pressDashButton() {
		dashbutton.setIsDashing (true);
		dashbutton.NotifyDashState();
	}

	// Simulate releasing the dash button

	void releaseDashButton() {
		dashbutton.setIsDashing (false);
		dashbutton.NotifyDashState ();
	}
}
