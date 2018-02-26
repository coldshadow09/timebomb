using UnityEngine;
using System.Collections.Generic;

public class StandardEventEmitter : MonoBehaviour, IEventEmitter {

	public List<IEventHandler> handlers;

	public void Start () {
		handlers = new List<IEventHandler> ();
	}

	public void RegisterHandler(IEventHandler handler) {
		handlers.Add(handler);
	}

	public void EmitGameStartedEvent(
		List<GameObject> players, int seed, BackgroundSelector.RoomType background
	) {
		foreach (IEventHandler handler in handlers) {
			handler.OnGameStarted(players, seed, background);
		}
	}

	public void EmitGameEndedEvent(bool bombWasDetonated) {
		foreach (IEventHandler handler in handlers) {
			handler.OnGameEnded(bombWasDetonated);
		}
	}

	public void EmitPlayerMovedEvent(GameObject player, Vector3 position) {
		foreach (IEventHandler handler in handlers) {
			handler.OnPlayerMoved(player, position);
		}
	}

	public void EmitPlayerDroppedItemEvent(GameObject player) {
		foreach (IEventHandler handler in handlers) {
			handler.OnPlayerDroppedItem(player);
		}
	}

	public void EmitPlayerDashStartedEvent(GameObject player) {
		foreach (IEventHandler handler in handlers) {
			handler.OnPlayerDashStarted(player);
		}
	}

	public void EmitPlayerDashStoppedEvent(GameObject player) {
		foreach (IEventHandler handler in handlers) {
			handler.OnPlayerDashStopped(player);
		}
	}

	public void EmitPlayerQuitEvent(GameObject player) {
		foreach (IEventHandler handler in handlers) {
			handler.OnPlayerQuit(player);
		}
	}

}
