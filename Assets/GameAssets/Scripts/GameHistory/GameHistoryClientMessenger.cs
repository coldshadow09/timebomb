using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

public class GameHistoryClientMessenger {

	private MessageFactory factory;
	private NetworkClientContainer client;

	public delegate void HandleStatus(bool success);
	public delegate void DisplayGames(List<GameRecord> games);

	public GameHistoryClientMessenger(
		NetworkClientContainer client,
		MessageFactory factory
	) {
		this.client = client;
		this.factory = factory;
	}

	public virtual void RegisterOnStatus(HandleStatus callback)
	{
		client.RegisterHandler (
			StatusMessage.Type,
			delegate(NetworkMessage netMsg) {
				StatusMessage msg = netMsg.ReadMessage<StatusMessage>();
				callback(msg.Success);
			}
		);
	}

	public virtual void RegisterOnGameRecordList(DisplayGames callback)
	{
		client.RegisterHandler (
			GameRecordListMessage.Type,
			delegate(NetworkMessage netMsg) {
				GameRecordListMessage msg =
					netMsg.ReadMessage<GameRecordListMessage>();
				callback(msg.Games);
			}
		);
	}

	public virtual void SendGameRecord(GameRecord game)
	{
		client.Send(
			GameRecordMessage.Type,
			factory.GameRecord(game)
		);
	}

	public virtual void SendPlayerProfile(PlayerProfile profile)
	{
		client.Send(
			PlayerProfileMessage.Type,
			factory.PlayerProfile(profile)
		);
	}

	public virtual void SendAllGames()
	{
		client.Send(
			AllGamesMessage.Type,
			factory.AllGames()
		);
	}

}
