using UnityEngine;
using UnityEditor;
using NUnit.Framework;
using NSubstitute;
using UnityEngine.Networking;
using System.Collections.Generic;
using System;

public class NullEventEmitterTest {

	private NullEventEmitter subject;
	private IEventHandler handlerMock;

	[SetUp]
	public void Init()
	{
		GameObject holder = new GameObject();
		subject = holder.AddComponent<NullEventEmitter> ();
		handlerMock = Substitute.For<IEventHandler> ();
	}

	[Test]
	public void TestItDoesNotEmitAGameStartedEvent() {
		List<GameObject> players = new List<GameObject> ();

		subject.RegisterHandler(handlerMock);
		subject.EmitGameStartedEvent(players, 0, BackgroundSelector.RoomType.ROOM_DAY);
		handlerMock.DidNotReceive().OnGameStarted(players, 0, BackgroundSelector.RoomType.ROOM_DAY);
	}

	[Test]
	public void TestItDoesNotEmitAGameEndedEvent() {
		bool bombDetonated = true;

		subject.RegisterHandler(handlerMock);
		subject.EmitGameEndedEvent(bombDetonated);
		handlerMock.DidNotReceive().OnGameEnded(bombDetonated);
	}

	[Test]
	public void TestItDoesNotEmitAPlayerMovedEvent() {
		GameObject player = new GameObject();
		Vector3 position = Vector3.zero;

		subject.RegisterHandler(handlerMock);
		subject.EmitPlayerMovedEvent(player, position);
		handlerMock.DidNotReceive().OnPlayerMoved(player, position);
	}

	[Test]
	public void TestItDoesNotEmitAPlayerDroppedItemEvent() {
		GameObject player = new GameObject();

		subject.RegisterHandler(handlerMock);
		subject.EmitPlayerDroppedItemEvent(player);
		handlerMock.DidNotReceive().OnPlayerDroppedItem(player);
	}

	[Test]
	public void TestItDoesNotEmitAPlayerDashStartedEvent() {
		GameObject player = new GameObject();

		subject.RegisterHandler(handlerMock);
		subject.EmitPlayerDashStartedEvent(player);
		handlerMock.DidNotReceive().OnPlayerDashStarted(player);
	}

	[Test]
	public void TestItDoesNotEmitAPlayerDashStoppedEvent() {
		GameObject player = new GameObject();

		subject.RegisterHandler(handlerMock);
		subject.EmitPlayerDashStoppedEvent(player);
		handlerMock.DidNotReceive().OnPlayerDashStopped(player);
	}

	[Test]
	public void TestItDoesNotEmitPlayerQuitEvent() {
		GameObject player = new GameObject();

		subject.RegisterHandler(handlerMock);
		subject.EmitPlayerQuitEvent(player);
		handlerMock.DidNotReceive().OnPlayerQuit(player);
	}
	
}
