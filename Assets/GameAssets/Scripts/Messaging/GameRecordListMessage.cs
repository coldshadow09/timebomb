using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using System;

public class GameRecordListMessage : MessageBase {

	public const short Type = 3051;

	public List<GameRecord> Games;
	public MessageFactory Factory;

	public override void Serialize(NetworkWriter writer)
	{
		writer.Write(Games.Count);
		foreach (GameRecord game in Games) {
			writer.Write(Factory.GameRecord(game));
		}
	}

	public override void Deserialize(NetworkReader reader)
	{
		Games = new List<GameRecord>();

		int count = reader.ReadInt32();
		for (int i = 0; i < count; i++) {
			Games.Add(
				reader.ReadMessage<GameRecordMessage>().Game
			);
		}
	}

}