using UnityEngine;
using System.Collections;

public class IntegrationTestStashButton : MonoBehaviour {
	public GameObject item;
	public PlayerController player;
	// Use this for initialization
	void Start () {
		CheckItemDeactivated ();
		player = FindObjectOfType<PlayerController> ();
		StartCoroutine (CheckItemDeactivated());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public IEnumerator CheckItemDeactivated() {

		// Check if the item has been piked up by player and is set as not active
		yield return new WaitForSeconds(3f);
		if (item.activeSelf) {
			IntegrationTest.Fail ();
		}
		player.EmptyStash();

		// Check if item becomes active when dashbutton is pressed
		if (item.activeSelf) {
			IntegrationTest.Pass ();
		} else {
			IntegrationTest.Fail ();

		}
	}

}
