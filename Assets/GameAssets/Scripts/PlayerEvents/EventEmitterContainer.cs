using UnityEngine;
using System.Collections;

/**
 * This class is responsible for determining which event emitter to provide
 * depending on the context of the gameplay mode (replay OR normal).
 *
 * This is effectivley so players can't provide input
 * while a replay of a game is occuring.
 */
public class EventEmitterContainer : MonoBehaviour {

	private bool playback;

	void Awake () {
		EnterNormalMode();
	}
	
	public virtual void EnterPlaybackMode() {
		playback = true;
	}

	public virtual void EnterNormalMode() {
		playback = false;
	}

	public virtual IEventEmitter GetEventEmitter(bool userInput = false) {
		// If we are in playback mode and the requesting
		// object emits events via
		// user input: return the null / no-op emitter.
		if (playback && userInput) {
			return gameObject.GetComponent<NullEventEmitter> ();
		}

		return gameObject.GetComponent<StandardEventEmitter> ();
	}
}
