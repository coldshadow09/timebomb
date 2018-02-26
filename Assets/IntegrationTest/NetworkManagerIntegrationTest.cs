using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class NetworkManagerIntegrationTest : MonoBehaviour {
	public PlayerMovement player;
	// Use this for initialization
	void Start () {
		GameObject.FindObjectOfType<NetworkManager> ().StartHost();
		//StartCoroutine(StartGame ());
	}

	IEnumerator StartGame() {
		yield return new WaitForSeconds(4f);
		Debug.Log ("lets do this");
		NetworkLobbyPlayer[] playerList = GameObject.FindObjectsOfType<LobbyPlayerInfo> ();
		Debug.Log ("going through");
		Debug.Log (playerList.Length);
		foreach (NetworkLobbyPlayer player in playerList) {
			Debug.Log ("Player found, ");
			if (player.isLocalPlayer) {
				Debug.Log("ready to start game");
				player.SendReadyToBeginMessage ();
				break;
			}
		}

	}
	// Update is called once per frame
	void Update () {
		if (player == null) {
			initialisePlayer ();
		} else {
			player.transform.Translate (1, 0, 0);
		}
	
	}

	void initialisePlayer() {
		player = GameObject.FindObjectOfType<PlayerMovement> ();
	}
}

