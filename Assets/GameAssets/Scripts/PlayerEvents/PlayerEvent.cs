using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class PlayerEvent {

	private PlayerProfile _profile;

	public PlayerProfile Profile {
		get {
			return _profile;
		}
	}

	private int _frame;

	public int Frame {
		get {
			return _frame;
		}
	}

	protected PlayerEvent(PlayerProfile profile, int frame) {
		_profile = profile;
		_frame = frame;
	}
}
