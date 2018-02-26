using UnityEngine;
using UnityEditor;
using NUnit.Framework;
using NSubstitute;
using UnityEngine.Networking;
using System.Collections.Generic;
using System;

public class GameHistoryServerMessengerTest {

	private NetworkServerContainer serverMock;
	private MessageFactory factoryMock;
	private GameHistoryServerMessenger subject;

	[SetUp]
	public void Init()
	{
		serverMock = Substitute.For<NetworkServerContainer> ();
		factoryMock = Substitute.For<MessageFactory> ();
		subject = new GameHistoryServerMessenger(serverMock, factoryMock);
	}	

	[Test]
	public void TestItRegistersAOnAllGamesHandlerCorrectly()
	{
		subject.RegisterOnAllGames(delegate() {
			return Arg.Any<List<GameRecord>> ();
		});

		serverMock.Received().RegisterHandler(
			Arg.Any<short>(),
			Arg.Any<NetworkMessageDelegate>()
		);
	}

	[Test]
	public void TestItRegisterAnOnGameRecordHandlerCorrectly()
	{
		subject.RegisterOnGameRecord(delegate(GameRecord game) {
			return Arg.Any<bool> ();
		});

		serverMock.Received().RegisterHandler(
			Arg.Any<short>(),
			Arg.Any<NetworkMessageDelegate>()
		);
	}

	[Test]
	public void TestItRegisterAnOnPlayerProfileHandlerCorrectly()
	{
		subject.RegisterOnPlayerProfile(delegate(PlayerProfile profile) {
			return Arg.Any<List<GameRecord>> ();
		});

		serverMock.Received().RegisterHandler(
			Arg.Any<short>(),
			Arg.Any<NetworkMessageDelegate>()
		);
	}
}
