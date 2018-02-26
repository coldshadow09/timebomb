using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class PlayerQuitEvent : PlayerEvent {

	public PlayerQuitEvent(
		PlayerProfile profile,
		int frame
	) : base(profile, frame) {}

}
