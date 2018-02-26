using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Networking;

public class PlayerScore : NetworkBehaviour, IScoreable {
	
	[SyncVar]
	private int _score;

	public int score {
		get {
			return _score;
		}
	}

	private Text playerText;

	// Use this for initialization
	void Start () {
		playerText = GameObject.FindWithTag("Score").GetComponent<Text> ();
	}

	void Update() {
		if (!isLocalPlayer)
			return;
		
		playerText.text = "Current Score: " + _score.ToString("f1");
	}

	public void ResetScore() {
		_score = 0;
	}

	public void IncreaseScore(int increase) {
		if (!isServer) {
			CmdIncreaseScore(increase);
			return;
		}

		_score += increase;
	}

	[Command]
	private void CmdIncreaseScore(int increase) {
		IncreaseScore(increase);
	}
}
