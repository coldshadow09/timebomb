using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class NetworkServerContainer {

	private bool listening;

	public NetworkServerContainer() {
		listening = false;
	}

	public virtual void Listen(int port) {
		if (!listening) {
			NetworkServer.Listen(port);
			listening = true;
		}
	}

	public virtual void RegisterHandler(short msgType, NetworkMessageDelegate handler) {
		NetworkServer.RegisterHandler(msgType, handler);
	}

}
