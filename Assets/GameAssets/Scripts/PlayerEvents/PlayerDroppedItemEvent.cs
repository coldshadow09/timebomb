using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class PlayerDroppedItemEvent : PlayerEvent {

	public PlayerDroppedItemEvent(
		PlayerProfile profile,
		int frame
	) : base(profile, frame) {}

}
