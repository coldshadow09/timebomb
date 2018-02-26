using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class DashButtonController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {
	const float maxDashTime = 3.0f;
	const float dashRechargeRate = 0.5f;
	const float dashDepletionRate = 1.0f;

	private bool isDashing;
	private float dashTimeLeft; // amount of time spent dashing

	public GameObject player;
	public Text buttonText;

	private IEventEmitter eventEmitter;

	// Use this for initialization
	void Start () {
		isDashing = false;
		dashTimeLeft = maxDashTime;

		eventEmitter = GameObject
			.FindObjectOfType<EventEmitterContainer> ()
			.GetEventEmitter(true);
	}

	// Update is called once per frame
	void Update () {
		if (isDashing) {
			dashTimeLeft -= Time.deltaTime * dashDepletionRate;
			dashTimeLeft = Mathf.Max (dashTimeLeft, 0.0f);

			if (dashTimeLeft == 0) {
				isDashing = false;
				NotifyDashState ();
			}
		} else {
			dashTimeLeft += Time.deltaTime * dashRechargeRate;
			dashTimeLeft = Mathf.Min (dashTimeLeft, 3.0f);
		}

		buttonText.text = dashTimeLeft.ToString ();
	}

	public void NotifyDashState () {
		if (player != null) {
			if (isDashing) {
				eventEmitter.EmitPlayerDashStartedEvent (player);
			} else {
				eventEmitter.EmitPlayerDashStoppedEvent (player);
			}
		}
	}

	public void OnPointerDown (PointerEventData eventData) {
		if (dashTimeLeft > 0.0 && !isDashing) {
			isDashing = true;
			NotifyDashState ();
		}
	}

	public void OnPointerUp (PointerEventData eventData) {
		if (isDashing) {
			isDashing = false;
			NotifyDashState ();
		}
	}

	public void setIsDashing(bool value) {
		// Function for manually setting dash value, useful for testing
		isDashing = value;
	}
}
