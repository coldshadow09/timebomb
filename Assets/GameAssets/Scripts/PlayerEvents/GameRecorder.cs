using UnityEngine;
using System.Collections.Generic;

public class GameRecorder : MonoBehaviour, IEventHandler {

	private RecordContainer recordContainer;

	private List<GameObject> players;

	void Start() {
		recordContainer = gameObject.GetComponent<RecordContainer> ();
		
		GameObject
			.FindObjectOfType<EventEmitterContainer> ()
			.GetEventEmitter()
			.RegisterHandler(this);
	}

	public void OnGameStarted(List<GameObject> players, int seed, BackgroundSelector.RoomType background) {
		this.players = players;
		List<PlayerProfile> profiles = new List<PlayerProfile> ();

		foreach (GameObject player in players) {
			profiles.Add(GetProfile(player));
		}

		recordContainer.InitialiseGame(profiles, seed, background);
	}

	public void OnGameEnded(bool bombWasDetonated) {
		Dictionary<PlayerProfile, int> scores =
			new Dictionary<PlayerProfile, int> ();

		foreach (GameObject player in players) {
			scores.Add(
				GetProfile(player),
				player.GetComponent<PlayerScore> ().score
			);
		}

		recordContainer.EndGame(scores);
	}

	public void OnPlayerMoved(GameObject player, Vector3 position) {
		player
			.GetComponent<PlayerRecorder> ()
			.SendPlayerMovedEvent(position);
	}

	public void OnPlayerDroppedItem(GameObject player) {
		player
			.GetComponent<PlayerRecorder> ()
			.SendPlayerDroppedItemEvent();
	}

	public void OnPlayerDashStarted(GameObject player) {
		player
			.GetComponent<PlayerRecorder> ()
			.SendPlayerDashStartedEvent();
	}

	public void OnPlayerDashStopped(GameObject player) {
		player
			.GetComponent<PlayerRecorder> ()
			.SendPlayerDashStoppedEvent();
	}

	public void OnPlayerQuit(GameObject player) {
		player
			.GetComponent<PlayerRecorder> ()
			.SendPlayerQuitEvent();
	}

	private PlayerProfile GetProfile(GameObject player) {
		return player
			.GetComponent<PlayerProfileContainer> ()
			.profile;
	}

}
