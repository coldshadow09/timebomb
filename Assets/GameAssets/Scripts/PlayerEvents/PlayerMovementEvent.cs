using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerMovementEvent : PlayerEvent {

	private Vector3 _position;

	public Vector3 Position {
		get {
			return _position;
		}
	}

	public PlayerMovementEvent(
		PlayerProfile profile,
		int frame,
		Vector3 position
	) : base(profile, frame) {
		_position = position;
	}

}
