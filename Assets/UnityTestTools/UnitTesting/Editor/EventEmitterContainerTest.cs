using UnityEngine;
using UnityEditor;
using NUnit.Framework;
using NSubstitute;
using UnityEngine.Networking;
using System.Collections.Generic;
using System;

public class EventEmitterContainerTest {

	private EventEmitterContainer subject;

	[SetUp]
	public void Init()
	{
		GameObject holder = new GameObject();
		subject = holder.AddComponent<EventEmitterContainer> ();
		holder.AddComponent<StandardEventEmitter> ();
		holder.AddComponent<NullEventEmitter> ();
	}

	[Test]
	public void TestItEntersNormalModeByDefault()
	{
		Assert.IsInstanceOf<StandardEventEmitter>(subject.GetEventEmitter());
	}

	[Test]
	public void TestItReturnsTheCorrectEventEmitterInNormalMode()
	{
		subject.EnterNormalMode();
		Assert.IsInstanceOf<StandardEventEmitter>(subject.GetEventEmitter());
		Assert.IsInstanceOf<StandardEventEmitter>(subject.GetEventEmitter(true));
	}

	[Test]
	public void TestItReturnsTheCorrectEventEmitterInPlaybackMode()
	{
		subject.EnterPlaybackMode();
		Assert.IsInstanceOf<StandardEventEmitter>(subject.GetEventEmitter());
		Assert.IsInstanceOf<NullEventEmitter>(subject.GetEventEmitter(true));
	}
}
