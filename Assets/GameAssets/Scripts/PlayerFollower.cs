using UnityEngine;
using System.Collections;

public class PlayerFollower : MonoBehaviour {
	public GameObject player;

	// Use this for initialization
	void Start () {
	
	}
	
	// LateUpdate is called after all other updates have been performed
	void LateUpdate () {
		Vector3 newPosition = transform.position;

		if (player == null)
			return;

		newPosition.x = player.transform.position.x;
		newPosition.y = player.transform.position.y;

		transform.position = newPosition;
	}
}
