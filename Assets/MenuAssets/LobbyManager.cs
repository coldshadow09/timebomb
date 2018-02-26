using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using System.Threading;

public class LobbyManager : NetworkLobbyManager {

	private IEventEmitter eventEmitter;

	private List<GameObject> gamePlayers;

	public override void OnLobbyStartServer() {	
		eventEmitter = gameObject
			.GetComponent<EventEmitterContainer> ()
			.GetEventEmitter();

		Debug.Log("scene: " + offlineScene);

		InitialiseGamePlayersList();
	}

	public override void OnLobbyServerSceneChanged(string sceneName) {
		if (sceneName == "LobbyScene") {
			InitialiseGamePlayersList();
		}
	}

	public override GameObject OnLobbyServerCreateLobbyPlayer(
		NetworkConnection conn,
		short playerControllerId
	) {
		LobbyPlayerInfo lobbyPlayerInfo = 
			(LobbyPlayerInfo) Instantiate(this.lobbyPlayerPrefab);

		return lobbyPlayerInfo.gameObject;
	}

	public override GameObject OnLobbyServerCreateGamePlayer(
		NetworkConnection conn,
		short playerControllerId
	) {
		GameObject player = (GameObject) Instantiate(
			this.gamePlayerPrefab, new Vector3(0, 0, 0), Quaternion.identity
		);

		NetworkServer.Spawn(player);
		return player;
	}

	public override void OnLobbyServerDisconnect(NetworkConnection conn) {
		OnLobbyServerPlayerRemoved(conn, 0);
	}

	public override void OnLobbyServerPlayerRemoved(
		NetworkConnection conn,
		short playerControllerId
	) {
		for (int i = gamePlayers.Count - 1; i >= 0; i--) {
		    if (gamePlayers[i].GetComponent<NetworkIdentity> ().connectionToClient == conn) {
				gamePlayers.RemoveAt(i);
			}
		}

		if (gamePlayers.Count == 0)
			ServerReturnToLobby();
	}

	private void OnLobbyServerGameStarted() {
		int seed = 0;
		BackgroundSelector.RoomType background = GameObject.FindObjectOfType<BackgroundSelector> ().room;

		eventEmitter.EmitGameStartedEvent(gamePlayers, seed, background);
	}

	public override bool OnLobbyServerSceneLoadedForPlayer(GameObject lobbyPlayer, GameObject gamePlayer) {
		gamePlayer.GetComponent<PlayerProfileContainer> ().SetPlayerProfile(
			lobbyPlayer.GetComponent<LobbyPlayerInfo> ().profile
		);

		gamePlayers.Add(gamePlayer);

		if (AllPlayersLoaded())
			OnLobbyServerGameStarted();

		return base.OnLobbyServerSceneLoadedForPlayer(lobbyPlayer, gamePlayer);
	}

	private void InitialiseGamePlayersList() {
		gamePlayers = new List<GameObject> ();
	}

	private bool AllPlayersLoaded() {
		int count = 0;

		foreach(LobbyPlayerInfo info in lobbySlots) {
			if (info != null)
				count++;
		}

		return gamePlayers.Count == count;
	}


}

