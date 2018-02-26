using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;

public class LobbyPlayerList : MonoBehaviour {
	public Text text;
	public Text beginGameButtonText;

	void Update () {
		LobbyPlayerInfo[] playerList = GameObject.FindObjectsOfType<LobbyPlayerInfo> ();
		text.text = "";

		foreach (LobbyPlayerInfo player in playerList) {
			if (player.profile == null)
				continue;

			text.text += player.profile.Name;
			text.text += "\n";	

			if (player.isLocalPlayer) {
				beginGameButtonText.text = !player.readyToBegin ? "Begin game" : "Waiting for others";
			}
		}
	}
}
