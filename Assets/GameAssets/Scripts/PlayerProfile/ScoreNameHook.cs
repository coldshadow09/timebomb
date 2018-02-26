using UnityEngine;
using System.Collections;

public class ScoreNameHook : MonoBehaviour {
	PostGameInfo pgi;

	void Start() {
		pgi = GameObject.FindObjectOfType<PostGameInfo> ();
	}

	void OnDestroy() {
		string playerName = gameObject.GetComponent<PlayerProfileContainer> ().profile.Name;
		int playerScore = gameObject.GetComponent<PlayerScore> ().score;

		if (pgi != null) {
			pgi.playerScores [playerName] = playerScore;
			Debug.Log ("Destroyed and hooked name and score");
		}
	}
}
