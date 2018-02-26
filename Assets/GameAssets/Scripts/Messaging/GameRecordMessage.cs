using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using System;

public class GameRecordMessage : MessageBase {

	public const short Type = 3050;

	public GameRecord Game;
	public MessageFactory Factory;

	private const short _movementEvent = 0;
	private const short _droppedItemEvent = 1;
	private const short _dashStartedEvent = 2;
	private const short _dashStoppedEvent = 3;
	private const short _quitEvent = 4;

	public override void Serialize(NetworkWriter writer)
	{
		// Write game seed
		writer.Write(Game.Seed);

		// Write game background
		writer.Write((int) Game.Background);

		// Write the time
		writer.Write(Game.Time.ToString());

		int number = 0;
		Dictionary<PlayerProfile, int> playerNumbers =
			new Dictionary<PlayerProfile, int> ();

		writer.Write(Game.Players.Count);
		foreach(PlayerProfile player in Game.Players) {
			writer.Write(Factory.PlayerProfile(player));
			playerNumbers.Add(player, number++);
		}
		
		// Write scores
		foreach(KeyValuePair<PlayerProfile, int> entry in Game.Scores) {
			writer.Write(playerNumbers[entry.Key]);
			writer.Write(entry.Value);
		}


		// Write game events.
		writer.Write(Game.Events.Count);
		foreach(PlayerEvent playerEvent in Game.Events) {
			writer.Write(playerNumbers[playerEvent.Profile]);
			writer.Write(playerEvent.Frame);

			switch (playerEvent.GetType().Name) {
				case "PlayerMovementEvent":
					writer.Write(_movementEvent);
					writer.Write(((PlayerMovementEvent) playerEvent).Position);
					break;
				case "PlayerDroppedItemEvent":
					writer.Write(_droppedItemEvent);
					break;
				case "PlayerDashStartedEvent":
					writer.Write(_dashStartedEvent);
					break;
				case "PlayerDashStoppedEvent":
					writer.Write(_dashStoppedEvent);
					break;
				case "PlayerQuitEvent":
					writer.Write(_quitEvent);
					break;
			}
		}
	}

	public override void Deserialize(NetworkReader reader)
	{
		int seed = reader.ReadInt32();
		BackgroundSelector.RoomType background =
			(BackgroundSelector.RoomType) reader.ReadInt32();
		DateTime time = Convert.ToDateTime(reader.ReadString());

		Game = new GameRecord(seed, background, time);

		Dictionary<int, PlayerProfile> players =
			new Dictionary<int, PlayerProfile> ();

		int count = reader.ReadInt32();
		for (int i = 0; i < count; i++) {
			PlayerProfile profile = reader.ReadMessage<PlayerProfileMessage>().Profile;
			players.Add(i, profile);
			Game.AddPlayer(profile);
		}

		for (int i = 0; i < count; i++) {
			int number = reader.ReadInt32();
			int score = reader.ReadInt32();
			Game.UpdateScore(players[number], score);
		}

		count = reader.ReadInt32();
		for (int i = 0; i < count; i++) {
			PlayerEvent playerEvent = null;
			
			PlayerProfile profile = players[reader.ReadInt32()];
			int frame = reader.ReadInt32();

			short eventType = reader.ReadInt16();

			switch (eventType) {
				case _movementEvent:
					Vector3 position = reader.ReadVector3();
					playerEvent = new PlayerMovementEvent(profile, frame, position);
					break;
				case _droppedItemEvent:
					playerEvent = new PlayerDroppedItemEvent(profile, frame);
					break;
				case _dashStartedEvent:
					playerEvent = new PlayerDashStartedEvent(profile, frame);
					break;
				case _dashStoppedEvent:
					playerEvent = new PlayerDashStoppedEvent(profile, frame);
					break;
				case _quitEvent:
					playerEvent = new PlayerQuitEvent(profile, frame);
					break;
			}

			Game.AddEvent(playerEvent);
		}
	}

}
