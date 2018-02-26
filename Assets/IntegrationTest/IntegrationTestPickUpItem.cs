using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class IntegrationTestPickUpItem : MonoBehaviour {
	public GameObject item;

	// Use this for initialization
	// Update is called once per frame
	public void OnTriggerEnter2D (Collider2D other) {
		// Checks that the item is set as inactive
		if (!item.activeSelf) {
			// Item successfully set as inactive
			Debug.Log ("Item successfully set as inactive");
			IntegrationTest.Pass (item);
		}
	}
}
