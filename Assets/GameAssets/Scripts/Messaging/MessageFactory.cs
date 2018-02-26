using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking.NetworkSystem;

public class MessageFactory {

	public virtual StatusMessage Status(bool success) {
		StatusMessage msg = new StatusMessage ();
		msg.Success = success;

		return msg;
	}

	public virtual GameRecordMessage GameRecord(GameRecord game) {
		GameRecordMessage msg = new GameRecordMessage ();
		msg.Game = game;
		msg.Factory = this;

		return msg;
	}

	public virtual GameRecordListMessage GameRecordList(List<GameRecord> games) {
		GameRecordListMessage msg = new GameRecordListMessage ();
		msg.Games = games;
		msg.Factory = this;

		return msg;
	}

	public virtual PlayerProfileMessage PlayerProfile(PlayerProfile profile) {
		PlayerProfileMessage msg = new PlayerProfileMessage ();
		msg.Profile = profile;

		return msg;
	}

	public virtual AllGamesMessage AllGames() {
		AllGamesMessage msg = new AllGamesMessage();
		return msg;
	}

}
