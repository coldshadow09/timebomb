using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class TestItem : MonoBehaviour, IItemable {
	public int points;
	public void ApplyEffect(PlayerController player) {
		GameObject.Destroy (gameObject);
		//GameObject.FindGameObjectWithTag ("Timer").GetComponent<UpdateTime>().CmdIncreaseTime(1000);
		GameObject.FindGameObjectWithTag ("Score").GetComponent<PlayerScore>().IncreaseScore(8);
	}

	public void OnPickUp() {

	}

	public void OnDrop (Vector3 position) {

	}
}
