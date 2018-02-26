using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine.UI;
using System;

public class GameHistoryRetrieval : GameHistoryClient {

	public GameObject buttonPrefab;
	public Transform contentTransform;

	private PlayerProfile player;

	// Use this for initialization
	public override void Start () {
		player = PlayerProfileStore
			.GetInstance()
			.LoadProfile();

		base.Start();
	}

	protected override void OnConnect(NetworkMessage msg) {
		messenger.RegisterOnGameRecordList(OnRetrieveRecords);
		messenger.SendPlayerProfile(player);
	}

	private void OnRetrieveRecords(List<GameRecord> games) {
		foreach (GameRecord game in games) {
			CreateButton(game);
		}
	}

	private void CreateButton(GameRecord game) {
		GameObject button = (GameObject) Instantiate(
			buttonPrefab,
			contentTransform,
			false
		);

		GameSelectionController gameSelector =
			button.GetComponent<GameSelectionController> ();

		gameSelector.game = game;
		gameSelector.player = player;
	}
}
