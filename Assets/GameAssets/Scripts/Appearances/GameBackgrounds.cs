using System.Collections.Generic;

class GameBackgrounds
{

    public static Dictionary<string, string> Types;

    static GameBackgrounds()
    {
		Types = new Dictionary<string, string> ();
		
        Types.Add("Day", "");
        Types.Add("Night", "");
    }


}