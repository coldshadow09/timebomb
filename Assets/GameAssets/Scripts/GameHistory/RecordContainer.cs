using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using System;

/**
 * This class is responsible for creating the actual event objects
 * that can be stored in GameRecord and persisted.
 */
public class RecordContainer : GameHistoryClient {

	/**
	 * The game record we are currently working on
	 */
	public GameRecord record;

	/**
	 * The current frame of the recording we are at.
	 */
	public int frame;

	/**
	 * Are we currently recording?
	 */
	public bool isRecording;

	public override void Start() {
		base.Start();

		messenger.RegisterOnStatus(delegate(bool status) {
			Debug.Log("Game Record Commit Status: " + status);
		});

		isRecording = false;
	}

	protected override void OnConnect(NetworkMessage msg) {

	}

	void Update() {
		if (isRecording) {
			frame++;
		}
	}

	/**
	 * Initialises a new game record and begins the recording.
	 * @param List<PlayerProfile>         players    list of players
	 * @param int                         seed       RNG seed
	 * @param BackgroundSelector.RoomType background room background.
	 */
	public void InitialiseGame(
		List<PlayerProfile> players, int seed, BackgroundSelector.RoomType background
	) {
		isRecording = true;
		frame = 0;
		record = new GameRecord (seed, background, DateTime.Now);

		foreach (PlayerProfile player in players) {
			record.AddPlayer(player);
		}
	}

	/**
	 * Ends the game recording and finalises the score.
	 * @param Dictionary<PlayerProfile, int> scores the scores of the players
	 */
	public void EndGame(Dictionary<PlayerProfile, int> scores) {
		foreach (var entry in scores) {
			record.UpdateScore(entry.Key, entry.Value);
		}

		isRecording = false;
		messenger.SendGameRecord(record);
	}

	/**
	 * Adds a player movement event to the current game record.
	 * @param {[type]} PlayerProfile profile  The player that moved.
	 * @param {[type]} Vector3       position The position they moved to.
	 */
	public void AddPlayerMovedEvent(PlayerProfile profile, Vector3 position) {
		record.AddEvent (
			new PlayerMovementEvent (profile, frame, position)
		);
	}

	/**
	 * Adds a player dropped item event to the current game record.
	 * @param {[type]} PlayerProfile profile  The player that dropped their item.
	 */
	public void AddPlayerDroppedItemEvent(PlayerProfile profile) {
		record.AddEvent (
			new PlayerDroppedItemEvent (profile, frame)
		);
	}

	/**
	 * Adds a player dash started event to the current game record.
	 * @param {[type]} PlayerProfile profile  The player that started dashing.
	 */
	public void AddPlayerDashStartedEvent(PlayerProfile profile) {
		record.AddEvent (
			new PlayerDashStartedEvent (profile, frame)
		);
	}

	/**
	 * Adds a player dash stopped event to the current game record.
	 * @param {[type]} PlayerProfile profile  The player that stopped dashing.
	 */
	public void AddPlayerDashStoppedEvent(PlayerProfile profile) {
		record.AddEvent (
			new PlayerDashStoppedEvent (profile, frame)
		);
	}

	/**
	 * Adds a player quit event to the current game record.
	 * @param {[type]} PlayerProfile profile  The player that quit.
	 */
	public void AddPlayerQuitEvent(PlayerProfile profile) {
		record.AddEvent (
			new PlayerQuitEvent (profile, frame)
		);
	}

	/**
	 * Pauses the recording of the game.
	 */
	public void PauseGame() {
		isRecording = false;
	}

}
