using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.EventSystems;

public class SettingsMenuController : MonoBehaviour, IPointerUpHandler {
	public void OnPointerUp(PointerEventData eventData) {
		SceneManager.LoadScene("SettingsScene", LoadSceneMode.Single);
	}
}
