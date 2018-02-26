using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Networking;

public class TalkButtonController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {
	public Text buttonText;
	public ITalkable talkable;
	// https://www.youtube.com/watch?v=fRCe9XdHO64
	// Use this for initialization

	void Start () {
		buttonText.text = "start works";
	}
	
	// Update is called once per frame
	void Update () {
		if (talkable != null && talkable.ChatInProgress()) {
			buttonText.text = "Wait";
		}
		else {
			buttonText.text = "Talk";
		}
	}

	public void OnPointerDown (PointerEventData eventData) {
		talkable.TalkStart ();

	}
		
	public void OnPointerUp (PointerEventData eventData) {
		talkable.TalkStop ();
	}
}

