using UnityEngine;
using UnityEditor;
using NUnit.Framework;
using NSubstitute;
using UnityEngine.Networking;
using System.Collections.Generic;
using System;

public class GameHistoryClientMessengerTest {

	private NetworkClientContainer clientMock;
	private MessageFactory factoryMock;
	private GameHistoryClientMessenger subject;

	private NetworkMessage message;
	private NetworkWriter writer;

	[SetUp]
	public void Init()
	{
		clientMock = Substitute.For<NetworkClientContainer> ();
		factoryMock = Substitute.For<MessageFactory> ();
		subject = new GameHistoryClientMessenger(clientMock, factoryMock);

		writer = new NetworkWriter();
		message = new NetworkMessage();
		message.reader = new NetworkReader(writer);
	}	

	[Test]
	public void TestItRegistersAStatusHandlerCorrectly()
	{
		subject.RegisterOnStatus(delegate(bool status) {});

		clientMock.Received().RegisterHandler(
			Arg.Any<short>(),
			Arg.Any<NetworkMessageDelegate>()
		);
	}

	[Test]
	public void TestItRegistersAGameRecordListHandlerCorrectly()
	{
		subject.RegisterOnGameRecordList(delegate(List<GameRecord> list) {});

		clientMock.Received().RegisterHandler(
			Arg.Any<short>(),
			Arg.Any<NetworkMessageDelegate>()
		);
	}

	[Test]
	public void TestItCallsTheOnStatusHandlerCorrectly()
	{
		writer.Write(true);

		clientMock.RegisterHandler(
			Arg.Any<short>(),
			Arg.InvokeDelegate<NetworkMessageDelegate>(message)
		);

		subject.RegisterOnStatus(delegate(bool status) {
			Assert.True(status);
		});
	}

	[Test]
	public void TestItCallsTheOnGameRecordListHandlerCorrectly()
	{
		GameRecord game = new GameRecord(1, BackgroundSelector.RoomType.ROOM_DAY, DateTime.Now);

		List<GameRecord> games = new List<GameRecord> ();
		games.Add(game);

		GameRecordMessage single = new GameRecordMessage();
		single.Game = game;

		GameRecordListMessage msg = new GameRecordListMessage();
		msg.Factory = factoryMock;
		msg.Games = games;

		factoryMock.GameRecord(game).Returns(single);

		writer.Write(msg);

		clientMock.RegisterHandler(
			Arg.Any<short>(),
			Arg.InvokeDelegate<NetworkMessageDelegate>(message)
		);

		subject.RegisterOnGameRecordList(delegate(List<GameRecord> list) {
			Assert.True(list[0].Seed == 1);
		});
	}

	[Test]
	public void TestItSendsAPlayerProfileCorrectly()
	{
		PlayerProfileMessage msg = Substitute.For<PlayerProfileMessage>();
		PlayerProfile profile = new PlayerProfile("jayden", Arg.Any<Color>());

		factoryMock.PlayerProfile(profile).Returns(msg);

		subject.SendPlayerProfile(profile);

		factoryMock.Received().PlayerProfile(profile);
		
		clientMock.Received().Send(
			PlayerProfileMessage.Type,
			msg
		);
	}

	[Test]
	public void TestItSendsAGameRecordCorrectly()
	{
		GameRecordMessage msg = Substitute.For<GameRecordMessage>();
		GameRecord game = new GameRecord(1, BackgroundSelector.RoomType.ROOM_DAY, DateTime.Now);

		factoryMock.GameRecord(game).Returns(msg);

		subject.SendGameRecord(game);

		factoryMock.Received().GameRecord(game);
		
		clientMock.Received().Send(
			GameRecordMessage.Type,
			msg
		);
	}
}
