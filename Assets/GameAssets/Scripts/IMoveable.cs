using UnityEngine;

public interface IMoveable {
	// Interface for moving player, needed for event system
	void Move (Vector3 position);
	// Indication of where player moved.
}
