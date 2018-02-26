using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[System.Serializable]
public class GameRecord
{

	private Dictionary<PlayerProfile, int> _scores;

	public Dictionary<PlayerProfile, int> Scores {
		get {
			return _scores;
		}
	}

	private List<PlayerProfile> _players;

	public virtual List<PlayerProfile> Players {
		get {
			return _players;
		}
	}

	private PlayerProfile _winner;

	public PlayerProfile Winner {
		get {
			return _winner;
		}
	}

	private List<PlayerEvent> _events;

	public List<PlayerEvent> Events {
		get {
			return _events;
		}
	}

	private int _seed;

	public int Seed {
		get {
			return _seed;
		}
	}

	private DateTime _time;

	public DateTime Time {
		get {
			return _time;
		}

		set {
			_time = value;
		}
	}

	private BackgroundSelector.RoomType _background;

	public BackgroundSelector.RoomType Background {
		get {
			return _background;
		}

		set {
			_background = value;
		}
	}

	public GameRecord (int seed, BackgroundSelector.RoomType background, DateTime time)
	{
		Background = background;
		Time = time;
		_seed = seed;
		_events = new List<PlayerEvent> ();
		_scores = new Dictionary<PlayerProfile, int> ();
		_players = new List<PlayerProfile> ();
	}

	public void AddEvent (PlayerEvent playerEvent)
	{
		_events.Add (playerEvent);
	}

	public void AddPlayer(PlayerProfile player)
	{
		if (!_players.Contains(player)) {
			_players.Add(player);
			_scores [player]  = 0;
		}
	}

	public bool InvolvesPlayer (PlayerProfile playerProfile)
	{
		return _scores.ContainsKey (playerProfile);
	}

	public void UpdateScore (PlayerProfile playerProfile, int score)
	{
		_scores [playerProfile] = score;

		if (_winner == null || score > _scores[_winner]) {
			_winner = playerProfile;
		}
	}

	public void NullAllScores ()
	{
		foreach (PlayerProfile profile in _scores.Keys) {
			_scores [profile] = 0;
		}

		_winner = null;
	}


}
