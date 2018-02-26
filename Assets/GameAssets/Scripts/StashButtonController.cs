using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.EventSystems;

public class StashButtonController : MonoBehaviour, IPointerUpHandler {

	public GameObject player;
	public Button stashButton;
	private Sprite stashSprite;
	private Sprite defaultSprite;
	private IEventEmitter eventEmitter;

    // Use this for initialization
	void Start() {
		eventEmitter = GameObject
			.FindObjectOfType<EventEmitterContainer> ()
			.GetEventEmitter(true);

		defaultSprite = stashButton.GetComponent<Image>().sprite;
	}

	public void OnPointerUp(PointerEventData eventData) {
		// Call PlayerController function
		Debug.Log("calling empty stash");
		eventEmitter.EmitPlayerDroppedItemEvent(player);
    }

    public void ItemSprite(GameObject item) {
        stashSprite = item.GetComponent<SpriteRenderer>().sprite;
        stashButton.GetComponent<Image>().sprite = stashSprite;
	}

	public void StashSprite() {
		stashButton.GetComponent<Image>().sprite = defaultSprite;
		Debug.Log("Stash sprite.");
    }

}
