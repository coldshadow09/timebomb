using UnityEngine;
using System.Collections.Generic;

/**
 * The interface for the game event handler. This will be notified of various
 * game and player events that occur during the game.
 */
public interface IEventHandler {
	/**
	 * Called when the game starts. Passed a list of the players in the game,
	 * the random seed used for RNG and the background of the room.
	 */
	void OnGameStarted(List<GameObject> players, int seed, BackgroundSelector.RoomType background);
	/**
	 * Called when the game has ended. Passed a boolean flag that indicates whether the
	 * reason the game ended was the bomb exploding.
	 */
	void OnGameEnded(bool bombWasDetonated);
	/**
	 * Called when the given player GameObject moves
	 */
	void OnPlayerMoved(GameObject player, Vector3 position);
	/**
	 * Called when the given player GameObject drops the item in their stash
	 */
	void OnPlayerDroppedItem(GameObject player);
	/**
	 * Called when the given player GameObject starts dashing
	 */
	void OnPlayerDashStarted(GameObject player);
	/**
	 * Called when the given player GameObject stops dashing
	 */
	void OnPlayerDashStopped(GameObject player);
	/**
	 * Called when the given player GameObject quits
	 */
	void OnPlayerQuit(GameObject player);
}