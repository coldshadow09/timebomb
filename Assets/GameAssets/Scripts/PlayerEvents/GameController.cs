using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Networking;

public class GameController : MonoBehaviour, IEventHandler {

	public List<GameObject> players;

	public NetworkManager networkManager;

	void Start () {
		gameObject
			.GetComponent<EventEmitterContainer> ()
			.GetEventEmitter()
			.RegisterHandler(this);

		networkManager = gameObject.GetComponent<NetworkManager> ();
	}

	public void OnGameStarted(List<GameObject> players, int seed, BackgroundSelector.RoomType background) {
		this.players = players;
	}

	public void OnGameEnded(bool bombWasDetonated) {
		if (bombWasDetonated) {
			foreach (GameObject player in players) {
				player.GetComponent<IScoreable> ().ResetScore();
			}
		}

		networkManager.ServerChangeScene("GameEndScene");
	}

	public void OnPlayerMoved(GameObject player, Vector3 position) {
		player.GetComponent<IMoveable> ().Move(position);
	}

	public void OnPlayerDroppedItem(GameObject player) {
		player.GetComponent<IPlayable> ().EmptyStash();
	}

	public void OnPlayerDashStarted(GameObject player) {
		player.GetComponent<IDashable> ().DashStart();
	}

	public void OnPlayerDashStopped(GameObject player) {
		player.GetComponent<IDashable> ().DashStop();
	}

	public void OnPlayerQuit(GameObject player) {
		// Nothing
	}

}
