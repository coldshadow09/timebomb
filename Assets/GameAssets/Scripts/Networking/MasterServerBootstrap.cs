using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System;

public class MasterServerBootstrap : MonoBehaviour {

	public int Port;

	// Use this for initialization
	void Start () {
		NetworkServerContainer server = new NetworkServerContainer();
		MessageFactory factory = new MessageFactory();

		server.Listen(Port);

		GameHistoryService gameHistory 	= new GameHistoryService(
			new GameRecordStore()
		);

		GameHistoryServerMessenger gameHistoryServerMessenger = new GameHistoryServerMessenger(
			server,
			factory
		);

		gameHistoryServerMessenger.RegisterOnGameRecord(gameHistory.RegisterGame);
		gameHistoryServerMessenger.RegisterOnPlayerProfile(gameHistory.ForPlayer);
		gameHistoryServerMessenger.RegisterOnAllGames(gameHistory.All);
	}

}
