using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class ReplayManager : NetworkManager {

	private int frame;
	private int eventHead;
	private bool playing;

	private GameRecord game;
	private PlayerProfile perspective;
	private Dictionary<PlayerProfile, GameObject> playerObjects;

	private IEventEmitter eventEmitter;

	// Use this for initialization
	void Start () {
		this.StartHost();
		this.playing = false;

		EventEmitterContainer eventEmitterContainer =
			gameObject.GetComponent<EventEmitterContainer> ();

		eventEmitterContainer.EnterPlaybackMode();
		eventEmitter = eventEmitterContainer.GetEventEmitter();
	}
	
	// Update is called once per frame
	void Update () {
		if (this.playing) {
			this.PlayAtCurrentFrame();
			this.frame++;
		}
	}

	public void StartGame(GameRecord game, PlayerProfile perspective) {
		this.game = game;
		this.perspective = perspective;
		this.playerObjects = new Dictionary<PlayerProfile, GameObject> ();
		this.frame = 0;
		this.eventHead = 0;
		this.playing = true;
		this.ServerChangeScene("GameScene");
	}

	public override void OnClientSceneChanged(NetworkConnection connection) {
		Scene scene = SceneManager.GetActiveScene();

		switch (scene.name) {
			case "GameScene":
				this.OnGameScene();
				break;
			case "GameEndScene":
				this.OnGameEndScene();
				break;
		}
	}

	public override void OnServerAddPlayer(NetworkConnection conn, short id) {
		NetworkServer.AddPlayerForConnection(
			conn, this.SpawnPlayer(this.game.Players[id]), id
		);
	}

	private void OnGameScene() {
		RegisterPlayers();
		FixPerspective();

		GameObject
			.FindObjectOfType<BackgroundSelector> ()
			.SetNewRoom(game.Background);

		GameObject
			.FindObjectOfType<TalkButtonController> ()
			.gameObject
			.SetActive(false);

		eventEmitter.EmitGameStartedEvent(
			new List<GameObject>(playerObjects.Values),
			game.Seed,
			game.Background
		);
	}

	private void OnGameEndScene() {
		Debug.Log("Replay ended");
	}

	private GameObject SpawnPlayer(PlayerProfile profile) {
		this.playerObjects[profile] =
			(GameObject) Instantiate(
				this.playerPrefab, new Vector3(0, 0, 1), Quaternion.identity
			);

		this.playerObjects[profile].GetComponent<PlayerProfileContainer> ()
			.SetPlayerProfile(profile);

		return this.playerObjects[profile];
	}

	private void RegisterPlayers() {
		for (short id = 0; id < this.game.Players.Count; id++) {
			ClientScene.AddPlayer(this.client.connection, id);
		}
	}

	private void FixPerspective() {
		GameObject.FindObjectOfType<PlayerFollower> ().player =
			this.playerObjects[this.perspective];
	}

	private void ExecuteEvent(PlayerEvent playerEvent) {
		switch (playerEvent.GetType().Name) {
			case "PlayerMovementEvent":
				ExecuteEvent(playerEvent as PlayerMovementEvent);
				break;
			case "PlayerDroppedItemEvent":
				ExecuteEvent(playerEvent as PlayerDroppedItemEvent);
				break;
			case "PlayerDashStartedEvent":
				ExecuteEvent(playerEvent as PlayerDashStartedEvent);
				break;
			case "PlayerDashStoppedEvent":
				ExecuteEvent(playerEvent as PlayerDashStoppedEvent);
				break;
			case "PlayerQuitEvent":
				ExecuteEvent(playerEvent as PlayerQuitEvent);
				break;
		}
	}

	private void ExecuteEvent(PlayerMovementEvent playerMovementEvent) {
		eventEmitter.EmitPlayerMovedEvent(
			playerObjects[playerMovementEvent.Profile],
			playerMovementEvent.Position
		);
	}

	private void ExecuteEvent(PlayerDroppedItemEvent playerDroppedItemEvent) {
		eventEmitter.EmitPlayerDroppedItemEvent(
			playerObjects[playerDroppedItemEvent.Profile]
		);
	}

	private void ExecuteEvent(PlayerDashStartedEvent playerDashStartedEvent) {
		eventEmitter.EmitPlayerDashStartedEvent(
			playerObjects[playerDashStartedEvent.Profile]
		);
	}

	private void ExecuteEvent(PlayerDashStoppedEvent playerDashStoppedEvent) {
		eventEmitter.EmitPlayerDroppedItemEvent(
			playerObjects[playerDashStoppedEvent.Profile]
		);
	}

	private void ExecuteEvent(PlayerQuitEvent playerQuitEvent) {
		eventEmitter.EmitPlayerQuitEvent(
			playerObjects[playerQuitEvent.Profile]
		);
	}

	private void PlayAtCurrentFrame() {
		for (; this.NextEvent() != null; this.eventHead++) {
			this.ExecuteEvent(this.NextEvent());
		}
	}

	private PlayerEvent NextEvent() {
		if (this.game.Events.Count <= this.eventHead)
			return null;

		if (this.game.Events[this.eventHead].Frame != this.frame)
			return null;

		return this.game.Events[this.eventHead];
	}

}
