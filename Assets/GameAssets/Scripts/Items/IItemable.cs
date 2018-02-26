using UnityEngine;
using System.Collections;

public interface IItemable {
	void ApplyEffect (PlayerController player);
	void OnPickUp ();
	void OnDrop (Vector3 position);
}
