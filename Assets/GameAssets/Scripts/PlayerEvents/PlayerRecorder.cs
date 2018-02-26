using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Networking;

/**
 * This class is necessary because in order to send commands
 * using unity across the networkmanager, you need player authority. Only
 * scripts attached to your player object may have player authority so we have
 * to route messages that are emitted on the local machine through this class
 * using Unity's Commands.
 */
public class PlayerRecorder : NetworkBehaviour {

	private RecordContainer recordContainer;
	private PlayerProfileContainer profileContainer;

	void Start () {
		// if not a local player but still exists in the game instance (and not the server)
		// we want to return and not retrieve the record.
		if (!isLocalPlayer && !isServer)
			return;

		recordContainer = GameObject.FindObjectOfType<RecordContainer> ();
		profileContainer = gameObject.GetComponent<PlayerProfileContainer> ();
	}

	public void SendPlayerMovedEvent(Vector3 position) {		
		if (!isServer) {
			CmdSendPlayerMovedEvent(position);
			return;
		}

		recordContainer.AddPlayerMovedEvent(
			profileContainer.profile, position
		);
	}

	public void SendPlayerDroppedItemEvent() {
		if (!isServer) {
			CmdSendPlayerDroppedItemEvent();
			return;
		}

		recordContainer.AddPlayerDroppedItemEvent(profileContainer.profile);
	}

	public void SendPlayerDashStartedEvent() {
		if (!isServer) {
			CmdSendPlayerDashStartedEvent();
			return;
		}

		recordContainer.AddPlayerDashStartedEvent(profileContainer.profile);
	}

	public void SendPlayerDashStoppedEvent() {
		if (!isServer) {
			CmdSendPlayerDashStoppedEvent();
			return;
		}

		recordContainer.AddPlayerDashStartedEvent(profileContainer.profile);
	}

	public void SendPlayerQuitEvent() {
		if (!isServer) {
			CmdSendPlayerQuitEvent();
			return;
		}

		recordContainer.AddPlayerDashStartedEvent(profileContainer.profile);
	}

	[Command]
	private void CmdSendPlayerMovedEvent(Vector3 position) {
		SendPlayerMovedEvent(position);
	}

	[Command]
	private void CmdSendPlayerDroppedItemEvent() {
		SendPlayerDroppedItemEvent();
	}

	[Command]
	private void CmdSendPlayerDashStartedEvent() {
		SendPlayerDashStartedEvent();
	}

	[Command]
	private void CmdSendPlayerDashStoppedEvent() {
		SendPlayerDashStoppedEvent();
	}

	[Command]
	private void CmdSendPlayerQuitEvent() {
		SendPlayerQuitEvent();
	}

}
