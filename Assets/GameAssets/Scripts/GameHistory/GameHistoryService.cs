using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class GameHistoryService {

	private GameRecordStore _records;

	public GameHistoryService(GameRecordStore records)
	{
		_records = records;
	}

	public bool RegisterGame(GameRecord record)
	{
		_records.Add (record);
		return true;
	}

	public List<GameRecord> ForPlayer(PlayerProfile player)
	{
		return All().Where(r => r.InvolvesPlayer(player)).ToList();
	}

	public List<GameRecord> All()
	{
		return _records.All();
	}
	
}
