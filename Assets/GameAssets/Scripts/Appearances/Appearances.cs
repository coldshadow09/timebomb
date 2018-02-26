using UnityEngine;
using System.Collections.Generic;

public class Appearances {

	public static Dictionary<string, Color> Hats;

	static Appearances()
	{
		Hats = new Dictionary<string, Color>();

		Hats.Add("Red", new Color(0, 0, 0));
		Hats.Add("Blue", new Color(0, 0, 0));
		Hats.Add("Green", new Color(0, 0, 0));
	}

}
