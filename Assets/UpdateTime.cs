using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using ProgressBar.Utils;
public class UpdateTime :  NetworkBehaviour, ITimeable  {
	[SyncVar]
	public float timeLeft;
	
	public int endTime;
	
	public Text timerText;
	
	public IIncrementable progressable;
	
	public IPlayable playable;

	private IEventEmitter eventEmitter;

	private bool finished;

	// Use this for initialization
	void Start () {
		finished = false;

		eventEmitter = GameObject
			.FindObjectOfType <EventEmitterContainer> ()
			.GetEventEmitter();

		progressable = GameObject
			.FindObjectOfType <ProgressBar.ProgressBarBehaviour> ();
	}

	// Update is called once per frame
	void Update () {
		progressable.SetValue (CalculatePercentage());
		timeLeft -= Time.deltaTime;

		if (!isServer)
			return;

		if((timeLeft < 0 || timeLeft > endTime) && !finished) {
			finished = true;
			eventEmitter.EmitGameEndedEvent(timeLeft < 0);
		}
	}

	public float CalculatePercentage() {
		return 100*timeLeft/endTime;
	}
	public void OnTimeLeftChange(float time) {
		timerText.text = "Time remaining: " + time.ToString("f1");
	}

	public void IncreaseTime(float time) {
		// Updates the time of the clock on the server
		Debug.Log("increased time on server");
		if (!isServer) {
			return;
		}
		timeLeft = timeLeft + time;
	}
		
	public void UpdateTimeServer(float time) {
		// inform server to increase time by 'time' seconds

		Debug.Log ("sending command to player controller for time");
		GameObject.FindObjectOfType <PlayerController>().CmdIncreaseTime(time);


	}


}
