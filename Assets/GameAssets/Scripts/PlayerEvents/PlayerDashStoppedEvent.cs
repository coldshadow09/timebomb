using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class PlayerDashStoppedEvent : PlayerEvent {

	public PlayerDashStoppedEvent(
		PlayerProfile profile,
		int frame
	) : base(profile, frame) {}

}
