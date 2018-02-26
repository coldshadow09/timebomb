using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class PanelAnimationController : MonoBehaviour {
	public Button menuButton;
	public GameObject sidePanel;
	private Animator anim;
	private bool displayPanel = false;
	public InGameSettingsMenu settings;

	// Use this for initialization
	void Start() {
		settings = GameObject.FindObjectOfType<InGameSettingsMenu>();
		anim = sidePanel.GetComponent<Animator>();
		// Don't play the animation by default
		anim.enabled = false;
		if (!displayPanel) {
			menuButton.onClick.AddListener(DisplayPanel);
        }
	}

	public void DisplayPanel() {
		// Enable animator component
		anim.enabled = true;
		// Play animation
		anim.Play("MenuSlideIn");
		displayPanel = true;
		menuButton.onClick.AddListener(HidePanel);
	}

	public void HidePanel() {
		anim.Play("MenuSlideOut");
		displayPanel = false;
		settings.StopMicrophone();
	}
}
