using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.Networking;
using System;

public class LeaderboardRetrieval : GameHistoryClient {

	public GameObject entryPrefab;
	public Transform contentTransform;

	protected override void OnConnect(NetworkMessage msg) {
		messenger.RegisterOnGameRecordList(OnRetrieveRecords);
		messenger.SendAllGames();
	}

	private void OnRetrieveRecords(List<GameRecord> games) {
		Dictionary<PlayerProfile, GameRecord> gameMap = 
			new Dictionary<PlayerProfile, GameRecord> ();

		foreach (GameRecord game in games) {
			foreach (PlayerProfile player in game.Players) {
				if (!gameMap.ContainsKey(player) || game.Scores[player] > gameMap[player].Scores[player]) {
					gameMap[player] = game;
				}
			}
		}

		List<KeyValuePair<PlayerProfile, GameRecord>> gameList = gameMap
			.ToList()
			.OrderByDescending(a => a.Value.Scores[a.Key])
			.ToList();

		for (int i = 0; i < gameList.Count; i++) {
			CreateEntry(gameList[i], i + 1);
		}
	}

	private void CreateEntry(
		KeyValuePair<PlayerProfile, GameRecord> entry,
		int rank
	) {
		GameObject container = (GameObject) Instantiate(
			entryPrefab,
			contentTransform,
			false
		);

		GameSelectionController gameSelector =
			container.GetComponentInChildren<GameSelectionController> ();

		container.GetComponent<Text> ().text = 
			rank + ". " + entry.Key.Name + " - " + entry.Value.Scores[entry.Key];		

		gameSelector.game = entry.Value;
		gameSelector.player = entry.Key;
	}

	

}
