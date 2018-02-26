using UnityEngine;
using System.Collections.Generic;

/**
 * This class is responsible for emitting events to the registered
 * event handlers
 */
public interface IEventEmitter {

	/**
	 * Registers an event handler that will listen for game events.
	 */
	void RegisterHandler(IEventHandler handler);

	/**
	 * Emits a game started event to all registered listeners
	 */
	void EmitGameStartedEvent(
		List<GameObject> players, int seed, BackgroundSelector.RoomType background
	);

	/**
	 * Emits a game ended event to all registered listeners
	 */
	void EmitGameEndedEvent(bool bombWasDetonated);

	/**
	 * Emits a player moved event to all registered listeners
	 */
	void EmitPlayerMovedEvent(GameObject player, Vector3 position);

	/**
	 * Emits a player dropped item event to all registered listeners
	 */
	void EmitPlayerDroppedItemEvent(GameObject player);

	/**
	 * Emits a player dash started to all registered listeners
	 */
	void EmitPlayerDashStartedEvent(GameObject player);

	/**
	 * Emits a player dash stopped to all registered listeners
	 */
	void EmitPlayerDashStoppedEvent(GameObject player);

	/**
	 * Emits a player quit event to all registered listeners
	 */
	void EmitPlayerQuitEvent(GameObject player);

}
