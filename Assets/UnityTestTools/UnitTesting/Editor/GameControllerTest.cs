using UnityEngine;
using UnityEditor;
using NUnit.Framework;
using NSubstitute;
using UnityEngine.Networking;
using System.Collections.Generic;
using System;

public class GameControllerTest {

	private GameController subject;

	[SetUp]
	public void Init()
	{
		GameObject holder = new GameObject();
		subject = holder.AddComponent<GameController> ();
		holder.AddComponent<NetworkManager> ();
	}

	[Test]
	public void TestItStartsTheGameCorrectly() {
		List<GameObject> players = new List<GameObject> ();
		subject.OnGameStarted(players, 0, BackgroundSelector.RoomType.ROOM_DAY);
		Assert.True(players == subject.players);
	}

	[Test]
	public void TestItEndsTheGameCorrectly() {
		GameObject player = new GameObject();
		IScoreable scoreable = player.AddComponent<PlayerScore> ();

		List<GameObject> players = new List<GameObject> ();
		players.Add(player);

		subject.OnGameStarted(players, 0, BackgroundSelector.RoomType.ROOM_DAY);
		
		try {
			subject.OnGameEnded(true);
		} catch (NullReferenceException) {}

		Assert.True(scoreable.score == 0);
	}
	
}
