using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class PlayerProfileContainer : NetworkBehaviour {

	[SyncVar]
	public PlayerProfile profile;

	public void SetPlayerProfile(PlayerProfile profile) {
		this.profile = profile;
	}

}
