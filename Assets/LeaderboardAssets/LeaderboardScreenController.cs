using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class LeaderboardScreenController : MonoBehaviour, IPointerClickHandler {
	public void OnPointerClick(PointerEventData eventData) {
		if (NetworkManager.singleton != null) {
			NetworkManager.singleton.offlineScene = "";
			Destroy(NetworkManager.singleton.gameObject);
			NetworkManager.Shutdown();
		}

		SceneManager.LoadScene ("LeaderboardScene");
	}
}
