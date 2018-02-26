using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Networking;
using UnityEngine.UI;

[System.Serializable]
public class LobbyPlayerInfo : NetworkLobbyPlayer {

	[SyncVar]
	public PlayerProfile profile;

	private PlayerProfileStore profileStore;

	void Start() {

		DontDestroyOnLoad(gameObject);

		if (!isLocalPlayer)
			return;

		profileStore = new PlayerProfileStore();

		CmdSetProfile(profileStore.LoadProfile());
	}

	// This is here because trying to set the profile locally
	// won't work (even with syncvar)
	[Command]
	private void CmdSetProfile(PlayerProfile profile) {
		this.profile = profile;
	}
}
