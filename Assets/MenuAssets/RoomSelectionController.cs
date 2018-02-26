using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class RoomSelectionController : MonoBehaviour, IPointerClickHandler {
	public string serverHostname;
	public int serverPort;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnPointerClick(PointerEventData eventData) {
		NetworkManager manager = GameObject.FindObjectOfType<NetworkManager> ();
		manager.networkAddress = serverHostname;
		manager.networkPort = serverPort;

		SceneManager.LoadScene ("LobbyScene");
		GameObject.FindObjectOfType<NetworkManager> ().StartClient();
	}
}
