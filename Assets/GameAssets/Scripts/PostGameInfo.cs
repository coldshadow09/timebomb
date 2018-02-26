using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PostGameInfo : MonoBehaviour {
	public Dictionary<string, int> playerScores;

	void Awake() {
		DontDestroyOnLoad (gameObject);
	}

	void Start() {
		playerScores = new Dictionary<string, int> ();
	}

	public void Cleanup() {
		Destroy (gameObject);
	}
}
