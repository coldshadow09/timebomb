using UnityEngine;
using System.Collections.Generic;

/**
 * This event emitter exists so that  during replay, no actual input events
 * will be routed.
 */
public class NullEventEmitter : MonoBehaviour, IEventEmitter {

	public void RegisterHandler(IEventHandler handler) {}
	
	public void EmitGameStartedEvent(
		List<GameObject> players, int seed, BackgroundSelector.RoomType background
	) {}
	
	public void EmitGameEndedEvent(bool bombWasDetonated) {}
	
	public void EmitPlayerMovedEvent(GameObject player, Vector3 position) {}
	
	public void EmitPlayerDroppedItemEvent(GameObject player) {}
	
	public void EmitPlayerDashStartedEvent(GameObject player) {}
	
	public void EmitPlayerDashStoppedEvent(GameObject player) {}

	public void EmitPlayerQuitEvent(GameObject player) {}

}
