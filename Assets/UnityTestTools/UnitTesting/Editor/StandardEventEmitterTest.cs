using UnityEngine;
using UnityEditor;
using NUnit.Framework;
using NSubstitute;
using UnityEngine.Networking;
using System.Collections.Generic;
using System;

public class StandardEventEmitterTest {

	private StandardEventEmitter subject;
	private IEventHandler handlerMock;

	[SetUp]
	public void Init()
	{
		GameObject holder = new GameObject();
		subject = holder.AddComponent<StandardEventEmitter> ();
		subject.Start();
		handlerMock = Substitute.For<IEventHandler> ();
	}

	[Test]
	public void TestItRegistersAListenerCorrectly()
	{
		subject.RegisterHandler(handlerMock);
		Assert.True(subject.handlers[0] == handlerMock);
	}

	[Test]
	public void TestItEmitsAGameStartedEvent() {
		List<GameObject> players = new List<GameObject> ();

		subject.RegisterHandler(handlerMock);
		subject.EmitGameStartedEvent(players, 0, BackgroundSelector.RoomType.ROOM_DAY);
		handlerMock.Received().OnGameStarted(players, 0, BackgroundSelector.RoomType.ROOM_DAY);
	}

	[Test]
	public void TestItEmitsAGameEndedEvent() {
		bool bombDetonated = true;

		subject.RegisterHandler(handlerMock);
		subject.EmitGameEndedEvent(bombDetonated);
		handlerMock.Received().OnGameEnded(bombDetonated);
	}

	[Test]
	public void TestItEmitsAPlayerMovedEvent() {
		GameObject player = new GameObject();
		Vector3 position = Vector3.zero;

		subject.RegisterHandler(handlerMock);
		subject.EmitPlayerMovedEvent(player, position);
		handlerMock.Received().OnPlayerMoved(player, position);
	}

	[Test]
	public void TestItEmitsAPlayerDroppedItemEvent() {
		GameObject player = new GameObject();

		subject.RegisterHandler(handlerMock);
		subject.EmitPlayerDroppedItemEvent(player);
		handlerMock.Received().OnPlayerDroppedItem(player);
	}

	[Test]
	public void TestItEmitsAPlayerDashStartedEvent() {
		GameObject player = new GameObject();

		subject.RegisterHandler(handlerMock);
		subject.EmitPlayerDashStartedEvent(player);
		handlerMock.Received().OnPlayerDashStarted(player);
	}

	[Test]
	public void TestItEmitsAPlayerDashStoppedEvent() {
		GameObject player = new GameObject();

		subject.RegisterHandler(handlerMock);
		subject.EmitPlayerDashStoppedEvent(player);
		handlerMock.Received().OnPlayerDashStopped(player);
	}

	[Test]
	public void TestItEmitsPlayerQuitEvent() {
		GameObject player = new GameObject();

		subject.RegisterHandler(handlerMock);
		subject.EmitPlayerQuitEvent(player);
		handlerMock.Received().OnPlayerQuit(player);
	}
	
}
