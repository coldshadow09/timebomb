using UnityEngine;
using System.Collections;

public class IntegrationTestIncreaseTime : MonoBehaviour {
	private UpdateTime timer;
	// Use this for initialization
	void Start () {
		timer = FindObjectOfType<UpdateTime> ();
		timer.timeLeft = 20;
	}
	
	public void OnTriggerEnter2D (Collider2D other) {
		// Checks that the item is set as inactive
		Debug.Log("test 5 trigger");
		ConfirmTimeIncrese ();

	}

	public void ConfirmTimeIncrese() {
		Debug.Log("Checking the time");
		if (timer.timeLeft > 25) {
			Debug.Log ("time is high");
			IntegrationTest.Pass ();
		} else {
			IntegrationTest.Fail ();
		}
	}

}
