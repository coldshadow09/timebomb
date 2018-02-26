using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.EventSystems;


public class PlayerController : NetworkBehaviour, IPlayable {

	public PlayerScore playerScore;
	private IItemable justDropped;
	public IItemable itemStash;
	public ITimeable timer;
	private PlayerProfileContainer profileContainer;
	private SpriteRenderer spriteRenderer;

	// Use this for initialization

	void Start () {
		if (isLocalPlayer)
			GameObject.FindObjectOfType <StashButtonController> ().player = this.gameObject;

		justDropped = null;
		timer = GameObject.FindObjectOfType <UpdateTime> ();
		spriteRenderer = gameObject.GetComponent<SpriteRenderer> ();
		profileContainer = gameObject.GetComponent<PlayerProfileContainer> ();
		playerScore = gameObject.GetComponent<PlayerScore> ();
	}
	

	void OnGUI() {
		GUIStyle style = new GUIStyle ("label");
		style.alignment = TextAnchor.UpperCenter;
		//style.fontSize = 32;
		//

		if (profileContainer.profile.HatColor != spriteRenderer.color) {
			spriteRenderer.color = profileContainer.profile.HatColor;
		}

		Vector3 screenPoint = Camera.main.WorldToScreenPoint (transform.position);
		GUI.Label (new Rect (screenPoint.x-50, Screen.height - (screenPoint.y + 80), 100f, 100f), profileContainer.profile.Name, style);
		GUI.Label (new Rect (screenPoint.x-50, Screen.height - (screenPoint.y + 100), 100f, 100f), playerScore.score.ToString(), style);
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (IsItem(other)) {
			AddToStash(GetItem(other));
		}

		if (IsBomb(other)) {
			HandleBomb();
		}
	}

	void OnTriggerExit2D (Collider2D other)
	{
		if (GetItem(other) == justDropped) {
			justDropped = null;
		}
	}

	public void PlayerReset() {
		Debug.Log ("reset player");
		FindObjectOfType<PlayerMovement>().ResetPosition();
		itemStash = null;
	}

	private IItemable GetItem (Collider2D obj) {
		return obj.GetComponent<IItemable>();
	}

	private bool IsItem (Collider2D other) {
		return other.CompareTag("Pick Up") && GetItem(other) != null;
	}

	private bool IsBomb (Collider2D other) {
		return other.CompareTag("bomb");
	}

	void HandleBomb() {
		// Handles logic when player returns to the bomb
		if (itemStash != null) {
			itemStash.ApplyEffect(this);
			itemStash = null;
		} else {
			Debug.Log ("You have no item in your stash");
		}
	}

	public void AddToStash(IItemable item) {
		if (justDropped == item) {
			Debug.Log ("you just dropped this item");
			return;
		}
		if (itemStash == null) {
			item.OnPickUp();
			itemStash = item;
			Debug.Log ("You picked up an item");
		} else {
			Debug.Log ("You already have an item in your stash");
		}
	}

	public void EmptyStash() {
		if (itemStash == null) {
			Debug.Log("No item in stash to drop.");
			return;
		}

		justDropped = itemStash;
		itemStash.OnDrop (transform.position);
		itemStash = null;

		Debug.Log("Dropped item");
	}

	public void QuitGame() {

	}


	[Command]
	public void CmdIncreaseTime(float value) {
		// Tells the server to increase the time by value
		Debug.Log(" Server sending command to increase time");
		if (timer == null)
			timer = GameObject.FindObjectOfType <UpdateTime> ();
		timer.IncreaseTime(value);
	}
	
}
