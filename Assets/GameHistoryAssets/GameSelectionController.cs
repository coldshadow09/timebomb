using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSelectionController : MonoBehaviour, IPointerClickHandler {

	private ReplayManager replayManager;
	private GameRecord _game;

	public PlayerProfile player;

	public GameRecord game {
		get {
			return _game;
		}

		set {
			gameObject.GetComponentsInChildren<Text> ()[0].text =
				value.Time.ToString();
			_game = value;
		}
	}

	public void Start() {
		replayManager = GameObject.FindObjectOfType<ReplayManager> ();
	}

	public void OnPointerClick(PointerEventData eventData) {
		replayManager.StartGame(game, player);
	}

}
