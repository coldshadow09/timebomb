using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections;

public class StartGameButton : MonoBehaviour, IPointerClickHandler {
	public void OnPointerClick(PointerEventData eventData) {
		NetworkLobbyPlayer[] playerList = GameObject.FindObjectsOfType<LobbyPlayerInfo> ();

		foreach (NetworkLobbyPlayer player in playerList) {
			if (player.isLocalPlayer) {
				player.SendReadyToBeginMessage ();
				break;
			}
		}
	}
}
