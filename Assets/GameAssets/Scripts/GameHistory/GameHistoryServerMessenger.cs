using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

public class GameHistoryServerMessenger {

	private NetworkServerContainer server;
	private MessageFactory factory;

	public delegate bool RegisterGameRecord(GameRecord game);
	public delegate List<GameRecord> ListGameRecordsFor(PlayerProfile profile);
	public delegate List<GameRecord> ListAllGameRecords();

	public GameHistoryServerMessenger(
		NetworkServerContainer server,
		MessageFactory factory
	) {
		this.server = server;
		this.factory = factory;
	}

	public void RegisterOnAllGames(ListAllGameRecords callback)
	{
		server.RegisterHandler (
			AllGamesMessage.Type,
			delegate(NetworkMessage netMsg) {
				netMsg.conn.Send(
					GameRecordListMessage.Type,
					factory.GameRecordList(callback())
				);
			}
		);
	}

	public void RegisterOnGameRecord(RegisterGameRecord callback)
	{
		server.RegisterHandler (
			GameRecordMessage.Type,
			delegate(NetworkMessage netMsg) {
				GameRecordMessage msg = netMsg.ReadMessage<GameRecordMessage>();
				netMsg.conn.Send(
					StatusMessage.Type,
					factory.Status(callback(msg.Game))
				);
			}
		);
	}

	public void RegisterOnPlayerProfile(ListGameRecordsFor callback)
	{
		server.RegisterHandler (
			PlayerProfileMessage.Type,
			delegate(NetworkMessage netMsg) {
				PlayerProfileMessage msg = netMsg.ReadMessage<PlayerProfileMessage>();
				netMsg.conn.Send(
					GameRecordListMessage.Type,
					factory.GameRecordList(callback(msg.Profile))
				);
			}
		);
	}

}
