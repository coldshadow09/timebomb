using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using System;

public class PlayerProfileMessage : MessageBase {

	public const short Type = 3052;

	public PlayerProfile Profile;

	public override void Serialize(NetworkWriter writer)
	{
		writer.Write(Profile.Name);
		writer.Write(Profile.HatColor);
	}

	public override void Deserialize(NetworkReader reader)
	{
		String name = reader.ReadString();
		Color hatColor = reader.ReadColor();

		Profile = new PlayerProfile(name, hatColor);
	}

}