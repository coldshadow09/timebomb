using System;
using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;


public class HeadlessServer : MonoBehaviour {

	public int serverPort;

	// Use this for initialization
	void Start () {
		if (SystemInfo.graphicsDeviceType == GraphicsDeviceType.Null) {
			switch (Environment.GetEnvironmentVariable("SERVER_TYPE")) {
				case "GAME":
				default:
					GameServer();
					break;
				case "MASTER":
					MasterServer();
					break;
			}
		}
	}

	void GameServer() {
		NetworkManager manager = gameObject.GetComponent<NetworkManager> ();

		SceneManager.LoadScene ("LobbyScene");
		manager.networkPort = serverPort;
		manager.StartServer ();

		Debug.Log ("Entered headless game mode");
	}

	void MasterServer() {
		SceneManager.LoadScene ("MasterServerScene");
		Debug.Log ("Entered headless master mode");
	}

}
