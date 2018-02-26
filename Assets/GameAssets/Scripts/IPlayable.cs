public interface IPlayable {
	// Interface for player scripts that deal primarily with time left in game, stash management and in game events
	void CmdIncreaseTime(float time);
	// Send message to server to increase time left for bomb
	void AddToStash (IItemable item);
	// Inform player to add item to the player's stash
	void EmptyStash ();
	// Informs player to proceed with the empty stash method and dorp all items
	void QuitGame();
	// Informs system that player has chosen to end game
}
