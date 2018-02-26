using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerProfileSettings : MonoBehaviour {
	public Slider colourSlider;
	public Text text;
	public InputField input;

	PlayerProfileStore profileStore = new PlayerProfileStore ();
	PlayerProfile profile;


	void Start() {
		profile = profileStore.LoadProfile ();
		input.text = profile.Name;
	}

	// Update is called once per frame
	void Update () {
		Color hatColour;
		float r,g,b,i;

		i = colourSlider.value * 2.0f * Mathf.PI;


		r = 0.5f + Mathf.Clamp(Mathf.Sin (i+0.0f*Mathf.PI/3.0f), -0.5f, 0.5f);
		g = 0.5f + Mathf.Clamp(Mathf.Sin (i+2.0f*Mathf.PI/3.0f), -0.5f, 0.5f);
		b = 0.5f + Mathf.Clamp(Mathf.Sin (i+4.0f*Mathf.PI/3.0f), -0.5f, 0.5f);
	
		hatColour = new Color (r, g, b);

		text.color = hatColour;

		profile.Name = input.text;
		profile.HatColor = hatColour;
	}

	public void SaveChanges() {
		profileStore.SaveProfile (profile);
	}
}
