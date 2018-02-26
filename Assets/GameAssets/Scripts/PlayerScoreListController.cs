using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class PlayerScoreListController : MonoBehaviour {
	public Text textbox;

	void Start() {
		PostGameInfo pgi = GameObject.FindObjectOfType<PostGameInfo> ();

		textbox.text = "";

		foreach (KeyValuePair<string, int> entry in pgi.playerScores) {
			textbox.text += entry.Value.ToString ();
			textbox.text += "\t";
			textbox.text += entry.Key;
			textbox.text += System.Environment.NewLine;
		}

		pgi.Cleanup ();
	}
}
