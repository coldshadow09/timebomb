using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class ItemBehaviour : MonoBehaviour, IItemable {
	public int pointIncrease;
	public float timeIncrease;
	public SpriteRenderer itemSprite;
	public GameObject item;
	public StashButtonController stashController;

	const int minPointIncrease = 1;
	const int maxPointIncrease = 100;

	void Start() {
		float percent;

		stashController = GameObject.FindObjectOfType<StashButtonController> ();
		if(pointIncrease == 0 || timeIncrease ==0) {
			Random.InitState(0);
			pointIncrease = Random.Range (minPointIncrease, maxPointIncrease);
			timeIncrease = 70 - pointIncrease;
		}
		percent = ((float)pointIncrease - (float)minPointIncrease) / 
					((float)maxPointIncrease - (float)minPointIncrease);
		itemSprite.color = BlendColourPercentage (percent);
	}

	Color BlendColourPercentage(float percent) {
		return new Color (1.0f - percent, percent, 0.0f);
	}

	public void ApplyEffect(PlayerController player) {
		GameObject.Destroy (gameObject);
		GameObject.FindObjectOfType<UpdateTime>().UpdateTimeServer(timeIncrease);
		player.gameObject.GetComponent<PlayerScore>().IncreaseScore(pointIncrease);
		stashController.StashSprite();
	}

	public void OnPickUp() {
		item.SetActive (false);
		stashController.ItemSprite(item);
	}

	public void OnDrop (Vector3 position) {
		transform.position = position;
		item.SetActive (true);
		stashController.StashSprite();
	}
	
}
