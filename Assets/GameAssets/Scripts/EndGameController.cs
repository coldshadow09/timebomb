using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class EndGameController : NetworkBehaviour {

	public float displaySeconds;
	private NetworkManager manager;

	void Start () {
		manager = GameObject.FindObjectOfType<NetworkManager> ();

		if (isServer)
			Invoke ("SwitchToOfflineScene", displaySeconds);
	}
	
	void SwitchToOfflineScene () {
		manager.ServerChangeScene (manager.offlineScene);
	}
}
