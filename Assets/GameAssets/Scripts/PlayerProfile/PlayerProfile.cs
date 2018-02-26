using UnityEngine;
using System.Collections;

[System.Serializable]
public class PlayerProfile : System.Object
{

	public Color HatColor;
	public string Name;

	// This is needed because of the way serializable classes are synced across the network.
	// A parameterless constructor is needed and properties have to be injected
	// after the fact.
	public PlayerProfile () {}

	public PlayerProfile (string name, Color hatColor)
	{
		this.Name = name;
		this.HatColor = hatColor;
	}

	public override bool Equals(object other)
	{
		PlayerProfile profile = (PlayerProfile) other;

		if (profile == null) {
			return false;
		}

		return Name.Equals(profile.Name);
	}

	public override int GetHashCode()
	{
		return Name.GetHashCode();
	}

}
