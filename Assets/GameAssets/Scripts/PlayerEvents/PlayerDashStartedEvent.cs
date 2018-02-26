using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class PlayerDashStartedEvent : PlayerEvent {

	public PlayerDashStartedEvent(
		PlayerProfile profile,
		int frame
	) : base(profile, frame) {}

}
