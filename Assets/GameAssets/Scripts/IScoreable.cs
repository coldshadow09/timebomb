using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Networking;

public interface IScoreable {
	int score { get; }
	void ResetScore();
	void IncreaseScore(int increase);
}
