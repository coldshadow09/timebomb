public interface ITalkable {
	// Interface for informing system when players are talking.
	void TalkStart ();
	// Sends message that a player is recording a message
	void TalkStop ();
	// Sends message that the player has ceased to record a message
	bool ChatInProgress();
	// Returns a boolean value indicating whether a player is currently talking
}