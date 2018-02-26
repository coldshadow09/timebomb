using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using System;

public class ExitRoomButton : MonoBehaviour, IPointerClickHandler {
	public void OnPointerClick(PointerEventData eventData) {
		if (NetworkManager.singleton != null) {
			NetworkManager.singleton.offlineScene = "";
			Destroy(NetworkManager.singleton.gameObject);
			NetworkManager.Shutdown();
		}

		SceneManager.LoadScene ("MenuScene");
	}
}
