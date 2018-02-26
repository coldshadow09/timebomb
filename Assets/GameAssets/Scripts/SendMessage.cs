using UnityEngine;
using System.Collections;
using UnityEngine.Networking;


public class SendMessage : NetworkBehaviour, ITalkable {
	public AudioSource aud;
	public int maxTalkTime;
	public int frequencyValue;
	public bool messageReceived = false;
	float[] tmpFirst;
	float[] tmpLast;
	[SyncVar]
	public bool messageRecording = false;

	public override void OnStartLocalPlayer ()
	{
		if (isLocalPlayer) {
			GameObject.FindObjectOfType <TalkButtonController> ().talkable = this;
		}
	}

	public bool ChatInProgress() {
		return messageRecording;
	}

	public IEnumerator PlayFromArray() {
		while (!messageReceived) {
			Debug.Log ("waiting");
			yield return new WaitForSeconds(1f);

		}

		float[] samples = new float[tmpFirst.Length * 2 ];
		for (int i = 0; i < tmpFirst.Length; i++) {
			samples [i] = tmpFirst [i];
		}
		for (int i = 0; i < tmpLast.Length; i++) {
			samples [i + tmpLast.Length] = tmpLast [i];
		}
		aud.clip = AudioClip.Create ("test", frequencyValue*2, 1, frequencyValue, false); 
		aud.clip.SetData (samples, 0);
		aud.Play ();
		messageReceived = false;
	}

	public void TalkStart() {
		if (!messageRecording) {
			CmdSetIsRecording (true);
			//messageRecording = true;
			aud.clip = Microphone.Start ("Built-in Microphone", false, maxTalkTime, frequencyValue);
			StartCoroutine(sendVoice (aud.clip));
		}
	}

	public void TalkStop() {
		if (messageRecording) {
			CmdPlayAudio ();
			CmdSetIsRecording (false);

		}
	}



	public IEnumerator sendVoice(AudioClip c) {
		yield return new WaitForSeconds (maxTalkTime);
		float[] samples = new float[c.samples * c.channels];
		c.GetData(samples, 0);
		float[] first_half = new float[samples.Length/2];
		first_half = return_half (samples, 0);
		float[] second_half = new float[samples.Length/2];
		second_half = return_half (samples, samples.Length/2);
		CmdTransferAudio(first_half, false);
		CmdTransferAudio(second_half, true);

	}


	public float [] return_half(float [] message, int index) {
		float[] result = new float[message.Length/2];
		Debug.Log(PlayerPrefs.GetFloat("SoundVolume"));
		for (int i = index; i < index + ( message.Length / 2 ); i++) {
			result [i-index] = message [i]*50*PlayerPrefs.GetFloat("SoundVolume");
		}
		return result;
	}
		

	[Command]
	void CmdTransferAudio(float[] message, bool isFinished) {
		// Send a voice message to the server
		// Message. A float array with values to indicate voice segments
		// isFinished. Indicates that the voice stream has finished and no further messages
		RpcTransferAudio (message, isFinished);
	}

	[ClientRpc]
	void RpcTransferAudio(float[] MessageBase, bool isFinished) {
		// Receive a voise message and save it in
		// Message. A float array with values to indicate voice segments
		// isFinished. Indicates that the voice stream has finished and no further messages
		if (!isFinished) {
			tmpFirst = MessageBase;
			messageReceived = false;
		}

		if (isFinished) {
			tmpLast = MessageBase;
			Debug.Log ("sent second message");
			messageReceived = true;
		}
	}

	[Command]
	void CmdPlayAudio() {
		// Informs all clients to play the audio
		RpcPlayAudio();
	}

	[ClientRpc]
	void RpcPlayAudio() {
		StartCoroutine(PlayFromArray ());
	}

	[Command]
	void CmdSetIsRecording(bool value) {
		//messageRecording = value;
		RpcSetIsRecording(value);
		Debug.Log ("Just set value as " + value);
	}

	[ClientRpc]
	void RpcSetIsRecording(bool value) {
		messageRecording = value;
		Debug.Log ("Messsage recording is " + messageRecording);
	}

}
