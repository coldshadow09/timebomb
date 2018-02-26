using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.EventSystems;

public class PlayerMovement : NetworkBehaviour, IDashable, IMoveable {
	private const float normalSpeed = 5.0f;
	private const float dashSpeed = 2 * normalSpeed;

	private float speed;
	public Rigidbody2D rigidBody;

	private Vector3 preCollisionPosition;
	private Vector3 newPosition;

	private IEventEmitter _eventEmitter;

	private Animator animator;

	// Use this for initialization
	void Start () {
		speed = normalSpeed;
		newPosition = transform.position;

		_eventEmitter = GameObject
			.FindObjectOfType<EventEmitterContainer> ()
			.GetEventEmitter(true);

		animator = gameObject.GetComponent<Animator> ();
	}

	// Update is called once per fram
	void Update () {
		if (!isLocalPlayer)
			return;

		Vector3? position = ExtractPosition();

		if (position == null)
			return;

		_eventEmitter.EmitPlayerMovedEvent(this.gameObject, position.Value);
	}

	private Vector3? ExtractPosition() {
		Vector3 worldPosition;
		Vector3? screenPosition;

		if (!Input.touchSupported) {
			screenPosition = ExtractMousePosition();
		} else {
			screenPosition = ExtractTouchPosition();
		}

		if (screenPosition == null)
			return null;

		worldPosition = Camera.main.ScreenToWorldPoint(screenPosition.Value);
		worldPosition.z = 0;

		return (Vector3?) worldPosition;
	}

	private Vector3? ExtractMousePosition() {
		if (
			Input.GetMouseButtonDown (0) &&
			!EventSystem.current.IsPointerOverGameObject ()
		) {
			return Input.mousePosition;
		}

		return null;
	}

	private Vector3? ExtractTouchPosition() {
		foreach (Touch touch in Input.touches) {
			int pointerID = touch.fingerId;

			if (touch.phase == TouchPhase.Ended) {
				if (!EventSystem.current.IsPointerOverGameObject (pointerID)) {
					return (Vector3) touch.position;
				}
			}
		}

		return null;
	}

	public void FixedUpdate () {
		if (Vector3.Distance(rigidBody.transform.position, newPosition) > 0) {
			animator.StopPlayback();
		} else {
			animator.StartPlayback();
		}

		if (!isLocalPlayer)
			return;

		preCollisionPosition = transform.position;
		ContinueMoving ();
	}

	private void ContinueMoving () {
		rigidBody.transform.position = Vector3.MoveTowards(
			rigidBody.transform.position,
			newPosition,
			speed * Time.deltaTime
		);
	}

	public void ResetPosition() {
		Debug.Log ("reset position");
		rigidBody.transform.position = new Vector3(0,0,0);
	}

	public override void OnStartLocalPlayer () {
		if (isLocalPlayer) {
			GameObject.FindObjectOfType <PlayerFollower> ().player = this.gameObject;
			GameObject.FindObjectOfType <DashButtonController> ().player = this.gameObject;
		}
	}

	public void OnTriggerEnter2D (Collider2D other) {
		// Collides "Background" from the inside.
		if (other.CompareTag ("bomb") || other.CompareTag ("Background")) {
			rigidBody.transform.position = preCollisionPosition;
			newPosition = preCollisionPosition;
		}
	}

	public void Move(Vector3 position) {
		newPosition = position;
	}

	public void DashStart() {
		speed = dashSpeed;
	}

	public bool isDashing() {
		if(speed == dashSpeed)
			return true;
		return false;
	}

	public void DashStop() {
		speed = normalSpeed;
	}
}
