using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameRecordStore {

	private List<GameRecord> _records;

	public GameRecordStore()
	{
		_records = new List<GameRecord>();
	}

	public List<GameRecord> All()
	{
		return _records;
	}

	public void Add(GameRecord record)
	{
		_records.Add(record);
	}
	
}
