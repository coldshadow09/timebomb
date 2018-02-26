using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class InGameSettingsMenu : MonoBehaviour {
	private static float musicValue = 0.5f, soundValue = 0.5f; // Set default
	public Slider musicSlider;
	private AudioSource musicSource;
	public Slider soundSlider;
	private AudioSource soundSource;
	private string microphone;
	public Button applyButton;
	public Button cancelButton;
	public PanelAnimationController panelAnimation;

	void Start() {
		try {
			// Get previously applied settings
			musicSlider.value = PlayerPrefs.GetFloat("MusicVolume");
			soundSlider.value = PlayerPrefs.GetFloat("SoundVolume");
			ChangeAudioSettings(musicSlider.value, soundSlider.value);
		} catch {
			// Set default values
			musicSlider.value = musicValue;
			soundSlider.value = soundValue;
			ChangeAudioSettings(musicSlider.value, soundSlider.value);
		}
		musicSlider.onValueChanged.AddListener(delegate { OnMusicChange(); });
		soundSlider.onValueChanged.AddListener(delegate { OnSoundChange(); });
		applyButton.onClick.AddListener(delegate { SaveSettings(); });
		cancelButton.onClick.AddListener(delegate {
			// Hide the panel
			panelAnimation.HidePanel();
		});
	}

	public void OnMusicChange() {
		musicSource = musicSlider.GetComponent<AudioSource>();
		musicValue = musicSlider.value;
		musicSource.volume = musicValue;
		musicSource.Play();
	}

	public void OnSoundChange() {
		soundSource = soundSlider.GetComponent<AudioSource>();
		// Get available microphones
		foreach (string device in Microphone.devices) {
			if (microphone == null) {
				// Set first mic as default
				microphone = device;
			}
		}
		InitMicrophone();
		soundSource.volume = soundSlider.value;
	}

	public void InitMicrophone() {
		// Test own mic
		soundSource.clip = Microphone.Start(microphone, true, 10, 44100);
        soundSource.loop = true;
		if (Microphone.IsRecording(microphone)) {
			// Wait for microphone to start recording
			while (!(Microphone.GetPosition(microphone) > 0)) { }
			// Plays audio source
			soundSource.Play();
		}
	}

	public void StopMicrophone() {
		soundSource = soundSlider.GetComponent<AudioSource>();
		soundSource.Stop();
	}

	public void ChangeAudioSettings(float musicValue, float soundValue) {
		// Change volume of the music
		GameObject music = GameObject.FindGameObjectWithTag("MainCamera");
		music.GetComponent<AudioSource>().volume = musicSlider.value;
		// Change mic volume of the players in the game
		GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
		foreach (GameObject player in players) {
			player.GetComponent<AudioSource>().volume = soundSlider.value;
		}
	}

	public void SaveSettings() {
		PlayerPrefs.SetFloat("MusicVolume", musicValue);
		PlayerPrefs.SetFloat("SoundVolume", soundValue);
		ChangeAudioSettings(musicSlider.value, soundSlider.value);
		// Hide the panel
		panelAnimation.HidePanel();
	}
}