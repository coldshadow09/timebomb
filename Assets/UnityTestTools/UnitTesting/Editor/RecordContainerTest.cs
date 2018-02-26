using UnityEngine;
using UnityEditor;
using NUnit.Framework;
using NSubstitute;
using UnityEngine.Networking;
using System.Collections.Generic;
using System;

public class RecordContainerTest {

	private GameRecord gameRecordMock;
	private RecordContainer subject;
	private PlayerProfile profile;
	private GameHistoryClientMessenger messengerMock;

	[SetUp]
	public void Init()
	{
		profile = new PlayerProfile("Jayden", Color.black);

		gameRecordMock = Substitute.For<GameRecord> (
			0, BackgroundSelector.RoomType.ROOM_DAY, DateTime.Now
		);

		messengerMock = Substitute.For<GameHistoryClientMessenger> (
			null, null
		);
		
		GameObject holder = new GameObject();
		subject = holder.AddComponent<RecordContainer> ();
		subject.messenger = messengerMock;
		subject.record = gameRecordMock;
	}


	[Test]
	public void TestItAddsAPlayerMovedEventCorrectly()
	{
		subject.frame = 0;
		subject.AddPlayerMovedEvent(profile, Vector3.zero);

		gameRecordMock.Received().AddEvent(
			Arg.Is<PlayerMovementEvent>(e => (
				e.Profile == profile &&
				e.Frame == 0 &&
				e.Position == Vector3.zero
			))
		);
	}

	[Test]
	public void TestItAddsAPlayerDroppedItemEventCorrectly()
	{
		subject.frame = 0;
		subject.AddPlayerDroppedItemEvent(profile);

		gameRecordMock.Received().AddEvent(
			Arg.Is<PlayerDroppedItemEvent>(e => (
				e.Profile == profile &&
				e.Frame == 0
			))
		);
	}

	[Test]
	public void TestItAddsAPlayerDashStartedEventCorrectly()
	{
		subject.frame = 0;
		subject.AddPlayerDashStartedEvent(profile);

		gameRecordMock.Received().AddEvent(
			Arg.Is<PlayerDashStartedEvent>(e => (
				e.Profile == profile &&
				e.Frame == 0
			))
		);
	}

	[Test]
	public void TestItAddsAPlayerDashStoppedEventCorrectly()
	{
		subject.frame = 0;
		subject.AddPlayerDashStoppedEvent(profile);

		gameRecordMock.Received().AddEvent(
			Arg.Is<PlayerDashStoppedEvent>(e => (
				e.Profile == profile &&
				e.Frame == 0
			))
		);
	}

	[Test]
	public void TestItAddsAPlayerQuitEventCorrectly()
	{
		subject.frame = 0;
		subject.AddPlayerQuitEvent(profile);

		gameRecordMock.Received().AddEvent(
			Arg.Is<PlayerQuitEvent>(e => (
				e.Profile == profile &&
				e.Frame == 0
			))
		);
	}

	[Test]
	public void TestItEndsTheGameCorrectly()
	{
		subject.frame = 0;
		subject.isRecording = true;

		List<PlayerProfile> players = new List<PlayerProfile> ();
		players.Add(profile);

		gameRecordMock.Players.Returns(players);

		Dictionary<PlayerProfile, int> scores = new Dictionary<PlayerProfile, int> ();
		scores.Add(profile, 10);

		subject.EndGame(scores);

		gameRecordMock.Received().UpdateScore(profile, Arg.Is(10));
		messengerMock.Received().SendGameRecord(gameRecordMock);

		Assert.False(subject.isRecording);
	}
}
