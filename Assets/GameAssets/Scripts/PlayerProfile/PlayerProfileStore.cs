using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class PlayerProfileStore {

	private const string PROFILE_NAME_KEY = "PROFILE_NAME";
	private const string PROFILE_HAT_COLOR_R_KEY = "PROFILE_HAT_COLOR_R";
	private const string PROFILE_HAT_COLOR_G_KEY = "PROFILE_HAT_COLOR_G";
	private const string PROFILE_HAT_COLOR_B_KEY = "PROFILE_HAT_COLOR_B";

	private const float PROFILE_HAT_COLOR_R_DEFAULT = 1.0f;
	private const float PROFILE_HAT_COLOR_G_DEFAULT = 1.0f;
	private const float PROFILE_HAT_COLOR_B_DEFAULT = 1.0f;
	private const float PROFILE_HAT_COLOR_A_DEFAULT = 1.0f;

	private const string PROFILE_NAME_DEFAULT_PREFIX = "Player_";
	private const int PROFILE_NAME_DEFAULT_SUFFIX_LENGTH = 4;

	private const string RANDOM_CHAR_SUBSET =
		"abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

	private static PlayerProfileStore _instance;

	public static PlayerProfileStore GetInstance() {
		if (_instance == null) {
			_instance = new PlayerProfileStore();
		}

		return _instance;
	}

	public PlayerProfile LoadProfile() {

		string name = PlayerPrefs.GetString(
			PROFILE_NAME_KEY,
			(
				PROFILE_NAME_DEFAULT_PREFIX +
				RandomSuffix(PROFILE_NAME_DEFAULT_SUFFIX_LENGTH)
			)
		);

		float r = PlayerPrefs.GetFloat(
			PROFILE_HAT_COLOR_R_KEY,
			PROFILE_HAT_COLOR_R_DEFAULT
		);

		float g = PlayerPrefs.GetFloat(
			PROFILE_HAT_COLOR_G_KEY,
			PROFILE_HAT_COLOR_G_DEFAULT
		);

		float b = PlayerPrefs.GetFloat(
			PROFILE_HAT_COLOR_B_KEY,
			PROFILE_HAT_COLOR_B_DEFAULT
		);

		PlayerProfile profile = CreateProfile(
			name, new Color(r, g, b, PROFILE_HAT_COLOR_A_DEFAULT)
		);

		if (!PlayerPrefs.HasKey(PROFILE_NAME_KEY)) {
			SaveProfile(profile);
		}

		return profile;
	}

	public void SaveProfile(PlayerProfile profile) {
		SaveProfile(profile.Name, profile.HatColor);
	}

	public void SaveProfile(string name, Color hatColor) {
		PlayerPrefs.SetString(PROFILE_NAME_KEY, name);
		PlayerPrefs.SetFloat(PROFILE_HAT_COLOR_R_KEY, hatColor.r);
		PlayerPrefs.SetFloat(PROFILE_HAT_COLOR_G_KEY, hatColor.g);
		PlayerPrefs.SetFloat(PROFILE_HAT_COLOR_B_KEY, hatColor.b);
	}

	public PlayerProfile CreateProfile(string name, Color hatColor) {
		return new PlayerProfile(name, hatColor);
	}

	private string RandomSuffix(int length) {
	    return new string(Enumerable.Repeat(RANDOM_CHAR_SUBSET, length)
	      .Select(s => s[(int) (Random.value * s.Length)]).ToArray());
	}

}
